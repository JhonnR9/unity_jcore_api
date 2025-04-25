using System;

public interface ICommandScheduler
{
    public void ScheduleCommand(Command command);   
    T CreateCommand<T>(Func<T> commandFactory) where T : Command;
}