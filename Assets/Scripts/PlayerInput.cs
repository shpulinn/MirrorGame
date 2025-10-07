using UnityEngine;

public class PlayerInput
{
    public float MoveInput { get; private set; }
    public float RotateInput { get; private set; }
    public bool SendMessagePressed { get; private set; }

    public void Update()
    {
        MoveInput = Input.GetAxis("Vertical");
        RotateInput = Input.GetAxis("Horizontal");
        SendMessagePressed = Input.GetKeyDown(KeyCode.Space);
    }

    public void ResetOneFrameFlags()
    {
        SendMessagePressed = false;
    }
}