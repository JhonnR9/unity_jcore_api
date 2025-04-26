
public class MoveState : IState
{
    public ICommandScheduler CommandScheduler {get; set;}
    public Model Model {get; set;}

    public void OnEnter(ICommandScheduler commandScheduler, Model model){
        CommandScheduler = commandScheduler;
        Model = model;
    }

    public void OnExit()
    {
        
    }

    public void OnProcess()
    {
        Command commnad = CommandScheduler.CreateCommand(() => new PrintCommand( "test"));
        CommandScheduler.ScheduleCommand(commnad);
  
    }
}