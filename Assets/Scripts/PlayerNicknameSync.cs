using Mirror;
using UnityEngine;

public class PlayerNicknameSync : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnNicknameChanged))]
    public string nickname = "Player";

    private void OnNicknameChanged(string oldNickname, string newNickname)
    {
        Debug.Log($"Никнейм изменен: {oldNickname} -> {newNickname}");
        UpdateNicknameDisplay();
    }

    private void UpdateNicknameDisplay()
    {
        var nameText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = nickname;
        }
    }
}