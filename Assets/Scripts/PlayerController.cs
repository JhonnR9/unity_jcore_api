using UnityEngine;

class PlayerController : Controller{

    protected override void Start()
    {
        UsePhysics = true;
        base.Start();
        SetColor(Color.green);

    }
    void Update()
    {
        AddMoveInputVector(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }
}