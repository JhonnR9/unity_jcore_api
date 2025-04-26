
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveState : IState
{
    public ICommandScheduler CommandScheduler { get; set; }
    public Model Model { get; set; }
    InputAction moveAction;
    Vector2 InputCache;

    public void OnEnter(ICommandScheduler commandScheduler, Model model)
    {
        CommandScheduler = commandScheduler;
        Model = model;
        moveAction = InputSystem.actions.FindAction("Move");
    }

    public void OnExit()
    {

    }

    private bool IsInRange(float v, float from, float to)
    {
        return v > from && v < to;
    }
    public void OnProcess()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector2 moveDirection = Vector2.zero;

        if (!IsInRange(moveValue.x, -0.01f, 0.01f))
        {
            moveDirection.x = Mathf.Sign(moveValue.x);
        }
        if (!IsInRange(moveValue.y, -0.01f, 0.01f))
        {
            moveDirection.y = Mathf.Sign(moveValue.y);
        }

        if (moveDirection.x != InputCache.x || moveDirection.y != InputCache.y)
        {
            Command moveCommnad = CommandScheduler.CreateCommand(() => new MoveCommand(moveDirection));
            InputCache = moveDirection;

            CommandScheduler.ScheduleCommand(moveCommnad);

        }

    }
}