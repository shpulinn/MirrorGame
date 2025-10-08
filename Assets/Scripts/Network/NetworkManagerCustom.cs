using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkManagerCustom : NetworkManager
{
    private readonly Dictionary<NetworkConnectionToClient, string> _nickCache = new();
    
    public string playerName = "Player";
    public System.Action OnClientConnected;
    
    public override void Awake()
    {
        base.Awake();
        autoCreatePlayer = false;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<AddPlayerNameMessage>(OnAddPlayerNameMessage, false);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        
        Debug.Log($"Клиент подключился. Отправляем никнейм: {playerName}");
        
        var message = new AddPlayerNameMessage { Nickname = playerName };
        NetworkClient.Send(message);

        OnClientConnected?.Invoke();
    }

    private void OnAddPlayerNameMessage(NetworkConnectionToClient conn, AddPlayerNameMessage message)
    {
        Debug.Log($"Сервер получил сообщение. Никнейм: {message.Nickname} от {conn.connectionId}");
        
        _nickCache[conn] = message.Nickname;
        SpawnPlayerForConnection(conn);
    }

    private void SpawnPlayerForConnection(NetworkConnectionToClient conn)
    {
        var player = Instantiate(playerPrefab);
        var playerNicknameSync = player.GetComponent<PlayerNicknameSync>();
        if (playerNicknameSync != null && _nickCache.TryGetValue(conn, out var nick))
        {
            playerNicknameSync.nickname = nick;
            Debug.Log($"Игрок создан с никнеймом: {nick}");
        }

        NetworkServer.AddPlayerForConnection(conn, player);
    }
    
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if (_nickCache.ContainsKey(conn))
        {
            _nickCache.Remove(conn);
        }
        
        base.OnServerDisconnect(conn);
    }
}