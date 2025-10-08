using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NicknameInputUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameInput;
    [SerializeField] private Button playButton;
    [SerializeField] private int nameMaxCharsLength = 20;

    private NetworkManagerCustom _networkManager;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayClicked);
    }

    private void Start()
    {
        _networkManager = NetworkManager.singleton as NetworkManagerCustom;
        if (_networkManager)
        {
            _networkManager.GetComponent<NetworkManagerHUD>().enabled = false;
        }
    }

    private void OnPlayClicked()
    {
        string nickname = GetNickname();
        
        if (_networkManager != null)
        {
            _networkManager.playerName = nickname;
            Debug.Log($"Никнейм установлен в NetworkManager: {nickname}");
        }
        
        gameObject.SetActive(false);
        _networkManager.GetComponent<NetworkManagerHUD>().enabled = true;
    }

    private string GetNickname()
    {
        var text = nicknameInput.text.Trim();

        if (text.Length > nameMaxCharsLength)
            text = text.Substring(0, nameMaxCharsLength);
        
        return string.IsNullOrEmpty(text)
            ? $"Player_{Random.Range(1, 99)}"
            : text;
    }
}