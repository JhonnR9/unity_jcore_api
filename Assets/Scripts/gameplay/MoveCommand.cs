
using UnityEngine;

public class MoveCommand : Command
{
    private Vector2 direction;

    public MoveCommand(Vector2 direction, Controller controller = null, int priority = 0)
    {
        this.Controller = controller;
        this.Priority = priority;
        this.direction = direction;
    }
    public override void Execute()
    {
        Controller.AddMoveInputVector(direction);
    }
}