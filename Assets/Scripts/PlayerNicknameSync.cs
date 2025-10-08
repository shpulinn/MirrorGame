using Mirror;
using TMPro;
using UnityEngine;

public class PlayerNicknameSync : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnNicknameChanged))]
    public string nickname = "Player";

    [SerializeField] private TextMeshProUGUI nameTextLabel;

    private void OnNicknameChanged(string oldNickname, string newNickname)
    {
        Debug.Log($"Никнейм изменен: {oldNickname} -> {newNickname}");
        UpdateNicknameDisplay();
    }

    private void UpdateNicknameDisplay()
    {
        if (nameTextLabel != null)
        {
            nameTextLabel.text = nickname;
        }
    }
}