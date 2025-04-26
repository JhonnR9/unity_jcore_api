public interface IState
{
    public ICommandScheduler CommandScheduler { get; set; }
    public Model Model { get; set; }
    public void OnEnter(ICommandScheduler commandScheduler, Model model);
    public void OnProcess();
    public void OnExit();
}