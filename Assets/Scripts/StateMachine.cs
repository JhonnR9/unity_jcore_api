using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : ICommandScheduler
{
    private readonly Dictionary<string, IState> states = new();
    private IState currentState;

    private HeapQueue<Command> commands { get; set; }
    private Coroutine commandRoutine;
    private readonly Controller controller;

    public StateMachine(Controller controller)
    {
        this.controller = controller;
        commands = new HeapQueue<Command>();
    }

    public void AddState(string key, IState state)
    {
        if (!states.ContainsKey(key))
        {
            states.Add(key, state);
        }
    }

    public void ChangeState(string key)
    {
        if (states.TryGetValue(key, out var newState))
        {
            currentState?.OnExit();
            currentState = newState;
            currentState?.OnEnter(this, controller.GetModel());
        }
        else
        {
            Debug.LogWarning($"State '{key}' not found.");
        }
    }

    public void ScheduleCommand(Command command)
    {
        commands.Push(command);

        if (commandRoutine == null)
        {
            commandRoutine = controller.StartCoroutine(ProcessCommands());
        }
    }

    IEnumerator ProcessCommands()
    {
        const float maxMillisecondsPerFrame = 5f;

        while (!commands.IsEmpty)
        {
            float startTime = Time.realtimeSinceStartup * 1000f;

            while (!commands.IsEmpty)
            {
                commands.First.Execute();
                commands.Pop();

                float elapsed = (Time.realtimeSinceStartup * 1000f) - startTime;

                if (elapsed >= maxMillisecondsPerFrame)
                    break;
            }

            yield return null;
        }

        commandRoutine = null;
    }

    public void UpdateMachine(){
        currentState?.OnProcess();
    }

    public T CreateCommand<T>(Func<T> commandFactory) where T : Command
    {
        if (commandFactory == null)
        {
            Debug.LogError($"{controller.name}: The factory function cannot be null.");
            return null;
        }
        T commnadInstance = commandFactory();
        commnadInstance.Controller = controller;

        return commnadInstance;
    }
}
