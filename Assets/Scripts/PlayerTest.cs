using UnityEngine;

class PlayerTest : Controller{

    protected override void Start()
    {
        UsePhysics = true;
        base.Start();
        SetColor(Color.green);

    }
    void Update()
    {
        AddInputVector(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }
}