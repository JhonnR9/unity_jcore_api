
using UnityEngine;

public class MoveCommand : Command
{
    private Controller controller;
    private Vector2 direction;
    
    public MoveCommand(Controller controller, Vector2 direction , int priority=0){
        this.controller = controller;
        this.Priority = priority;
        this.direction = direction;
    }
    public override void Execute()
    {
        controller.AddMoveInputVector(direction);
    }
}