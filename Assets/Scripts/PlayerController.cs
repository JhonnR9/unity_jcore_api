using UnityEngine;

class PlayerController : Controller
{
    protected override void Start()
    {
        UsePhysics = true;
        base.Start();

        GetStateMachine().AddState("move",new MoveState());
        GetStateMachine().ChangeState("move");
     
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
      
    }




}