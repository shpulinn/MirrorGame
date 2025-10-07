using UnityEngine;

public class NicknameService
{
    public string Nickname { get; private set; }

    public void SetNickname(string nickname)
    {
        Nickname = nickname;
        Debug.Log($"Nickname set: {nickname}");
    }
}