using UnityEngine;
using Mirror;
using VContainer;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody _rb;
    private PlayerInput _input;

    [Inject]
    public void Construct(PlayerInput input)
    {
        _input = input;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        
        var scope = GetComponent<PlayerScope>();
        
        if (!scope) return;
        
        scope.Build();
        scope.Container.Inject(this);
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        _input.Update();

        if (_input.SendMessagePressed)
        {
            // send message
        }
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        if (_input.RotateInput == 0f && _input.MoveInput == 0f)
        {
            _input.ResetOneFrameFlags();
            return;
        }

        transform.Rotate(Vector3.up * (_input.RotateInput * 120f * Time.fixedDeltaTime));
        _rb.velocity = transform.forward * (_input.MoveInput * moveSpeed);

        _input.ResetOneFrameFlags();
    }
}