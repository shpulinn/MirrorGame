using UnityEngine;
using Mirror;

public class PlayerAnimatorControllerSync : NetworkBehaviour, IPlayerAnimationService
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    [SerializeField] private Animator animator;
    private float _lastSpeed;

    [Command] private void CmdSetSpeed(float speed) => RpcSetSpeed(speed);
    [ClientRpc] private void RpcSetSpeed(float speed)
    {
        if (isLocalPlayer) return;
        animator.SetFloat(Speed, speed);
    }

    public void SetSpeed(float value)
    {
        if (Mathf.Approximately(_lastSpeed, value)) return;
        _lastSpeed = value;
        animator.SetFloat(Speed, value);
        CmdSetSpeed(value);
    }
}