
//Controller.Physics.cs

using UnityEngine;

/// <summary>
/// Module for objects that use physics, anything that moves for more details look for the Controller.cs file
/// </summary>
public partial class Controller : MonoBehaviour
{
    [SerializeField] Vector2 MaxMoveSpeed = new Vector2(5f, 5f);
    [SerializeField] float Acceleration = 20f;
    [SerializeField] float Friction = 25f;
    public bool UsePhysics { get; set; }
    public Vector2 Velocity => data.rb ? data.rb.linearVelocity : Vector2.zero;

    protected partial class Data
    {
        public Rigidbody2D rb;
        public Vector2 InputDirection = Vector2.zero;
        public Vector2 MoveVelocity = Vector2.zero;
        public Rigidbody2D.SlideMovement SlideMovement;
        public Rigidbody2D.SlideResults SlideResults;
    }

    private void StartPhysics()
    {
        if (data.rb == null)
        {
            ShowMissingComponentError("Rigidbody2D");
            return;
        }

        data.SlideMovement = new Rigidbody2D.SlideMovement();
        data.rb.bodyType = RigidbodyType2D.Kinematic;
        data.rb.gravityScale = 0;

        data.SlideMovement.gravity = Vector2.zero;
        data.SlideMovement.surfaceAnchor = Vector2.zero;
        data.SlideMovement.useSimulationMove = true;
        data.SlideMovement.surfaceUp=Vector2.zero;

    }
    void GetPhysicsComponents()
    {
        data.rb = GetComponent<Rigidbody2D>();
        
    }

    void UpdateMovement()
    {
        Vector2 targetSpeed = new Vector2(
            data.InputDirection.x * MaxMoveSpeed.x,
            data.InputDirection.y * MaxMoveSpeed.y
        );

        Vector2 accelRate = new Vector2(
            Mathf.Abs(targetSpeed.x) > 0.01f ? Acceleration : Friction,
            Mathf.Abs(targetSpeed.y) > 0.01f ? Acceleration : Friction
        );

        Vector2 movement = new Vector2(
            Mathf.MoveTowards(data.MoveVelocity.x, targetSpeed.x, accelRate.x * Time.fixedDeltaTime),
            Mathf.MoveTowards(data.MoveVelocity.y, targetSpeed.y, accelRate.y * Time.fixedDeltaTime)
        );

        data.MoveVelocity = movement;
    }

    virtual protected void FixedUpdate()
    {
        if (data.rb)
        {
            UpdateMovement();
            ApplyMovement();
        }
    }

    private void ApplyMovement()
    {
        data.SlideResults = data.rb.Slide(data.MoveVelocity, Time.fixedDeltaTime, data.SlideMovement);

        if (data.SlideResults.slideHit)
        {
            data.SlideMovement.surfaceAnchor = data.SlideResults.surfaceHit.normal * 0.1f;

        }

        // Sync with model
        data.model.position = data.rb.position;
        data.model.velocity = data.rb.linearVelocity;

    }

    public void AddMoveInputVector(Vector2 newInput)
    {
        if (data.rb)
        {
            data.InputDirection = newInput.sqrMagnitude > 0.01f ? newInput.normalized : Vector2.zero;
        }
    }


}
