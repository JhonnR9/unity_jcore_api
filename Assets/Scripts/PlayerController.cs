using UnityEngine;

class PlayerController : Controller
{
    protected override void Start()
    {
        UsePhysics = true;
        base.Start();
        SetColor(Color.green);
    }


protected override void FixedUpdate()
{
    // Normalizando os inputs
    float horizontal = Mathf.Sign(Input.GetAxis("Horizontal"));
    float vertical = Mathf.Sign(Input.GetAxis("Vertical"));

    // Se houver movimento, agenda o comando de movimento
    if (horizontal != 0 || vertical != 0)
    {
        Vector2 direction = new Vector2(horizontal, vertical);
        ScheduleCommand(new MoveCommand(this, direction));
    }
    else
    {
        // Quando não há movimento, agende um comando para parar
        // Você pode criar um comando específico de parada, ou apenas não enviar nenhum comando.
        ScheduleCommand(new MoveCommand(this, Vector2.zero));  // Parar o movimento (direção zero)
    }

    base.FixedUpdate();
}


}