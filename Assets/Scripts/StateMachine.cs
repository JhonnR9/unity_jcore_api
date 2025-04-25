using System;
using System.Collections;
using UnityEngine;
public class StateMachine : ICommandScheduler
{
    private HeapQueue<Command> commands { get; set; }
    private Coroutine commandRoutine;
    private readonly Controller controller;

    public StateMachine(Controller controller)
    {
        commands = new HeapQueue<Command>();
        this.controller = controller;
    }

    public void ScheduleCommand(Command command)
    {
        //Print($"Agendando comando: {command.GetType().Name}, Prioridade: {command.Priority}");
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

    public T CreateCommand<T>(Func<T> commandFactory) where T : Command
    {
        if (commandFactory == null)
        {
            Debug.LogError($"{controller.name}: The factory function cannot be null.");
            return null;
        }

        return commandFactory();
    }
    

}