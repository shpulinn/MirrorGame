using UnityEngine;
using Mirror;
using VContainer;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 120f;
    private Rigidbody _rb;
    private PlayerInput _input;
    private PlayerNicknameSync _playerNicknameSync;

    [Inject]
    public void Construct(PlayerInput input)
    {
        _input = input;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerNicknameSync = GetComponent<PlayerNicknameSync>();
    }
    
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        
        var scope = GetComponent<PlayerScope>();
        if (scope != null)
        {
            scope.Build();
            _input = scope.Container.Resolve<PlayerInput>();
        }
        else
        {
            _input = new PlayerInput();
        }
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        _input.Update();

        if (_input.SendMessagePressed)
        {
            CmdSendMessage($"Привет от {_playerNicknameSync.nickname}");
        }
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer || _input == null) return;

        if (_input.RotateInput == 0f && _input.MoveInput == 0f)
        {
            _input.ResetOneFrameFlags();
            return;
        }

        transform.Rotate(Vector3.up * (_input.RotateInput * rotationSpeed * Time.fixedDeltaTime));
        
        Vector3 movement = transform.forward * (_input.MoveInput * moveSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(_rb.position + movement);

        _input.ResetOneFrameFlags();
    }
    
    [Command]
    private void CmdSendMessage(string message)
    {
        RpcShowMessage(message);
    }

    [ClientRpc]
    private void RpcShowMessage(string message)
    {
        Debug.Log(message);
    }
}