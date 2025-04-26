
using System;

public abstract class Command : IComparable<Command>
{
    public int Priority { get; set; } = 0;
    public Controller Controller {get;set;}
    public int CompareTo(Command other)
    {
        return other.Priority.CompareTo(Priority);

    }
    public abstract void Execute();

}



