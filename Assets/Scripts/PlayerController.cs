using UnityEngine;
using UnityEngine.InputSystem;

class PlayerController : Controller
{
    InputAction moveAction;
    Vector2 InputCache;
    protected override void Start()
    {
        UsePhysics = true;
        base.Start();
        SetColor(Color.green);
        moveAction = InputSystem.actions.FindAction("Move");

        GetStateMachine().AddState("move",new MoveState());
        GetStateMachine().ChangeState("move");
     
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
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
            //Command commnad = GetStateMachine().CreateCommand(() => new PrintCommand("test"));
            //Command moveCommnad = GetStateMachine().CreateCommand(() => new MoveCommand(moveDirection));

           // GetStateMachine().ScheduleCommand(commnad);
            //Debug.Log($"MoveCommand issued: {moveDirection}");

            InputCache = moveDirection;

        }
    }

    private bool IsInRange(float v, float from, float to)
    {
        return v > from && v < to;
    }



}