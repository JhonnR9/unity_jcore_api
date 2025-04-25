public  interface IState{
    public abstract void OnEnter(ICommandScheduler commandScheduler);
    public abstract void OnProcess();
    public abstract void OnExit();
}