
using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// This class acts as a bridge between Unity and the JCore API,
/// offering methods to allow other classes to manipulate Unity objects.
/// All classes in the Unity scene should inherit from this class.
/// </summary>
public partial class Controller : MonoBehaviour
{
    protected partial class Data
    {
        public HeapQueue<Command> commands { get; set; }
        public Coroutine commandRoutine;
    }
    protected Data data;

    protected virtual void Start()
    {
        if (UsePhysics)
        {
            StartPhysics();
        }
        StartVisual();
        data.commands = new HeapQueue<Command>();
    }



    public void ScheduleCommand(Command command)
    {
        //Print($"Agendando comando: {command.GetType().Name}, Prioridade: {command.Priority}");
        data.commands.Push(command);

        if (data.commandRoutine == null)
        {
            data.commandRoutine = StartCoroutine(ProcessCommands());
        }
    }


    public void Print(String message)
    {
        Debug.Log(message);
    }

    IEnumerator ProcessCommands()
    {
        const float maxMillisecondsPerFrame = 5f;

        while (!data.commands.IsEmpty)
        {
            float startTime = Time.realtimeSinceStartup * 1000f;

            while (!data.commands.IsEmpty)
            {
                data.commands.First.Execute();
                data.commands.Pop();

                float elapsed = (Time.realtimeSinceStartup * 1000f) - startTime;

                if (elapsed >= maxMillisecondsPerFrame)
                    break;
            }

            yield return null;
        }

        data.commandRoutine = null;
    }

    protected virtual void Update(){}
    protected void Awake()
    {
        data = new Data();
        GetComponents();
    }

    protected virtual void GetComponents()
    {

        GetPhysicsComponents();
        GetVisualComponents();
    }

    void ShowMissingComponentError(string componentName)
    {
        Debug.LogError($"{gameObject.name} is missing a {componentName} component.");
    }


}

