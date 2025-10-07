using Mirror;
using UnityEngine;

public class NetworkManagerCustom : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        var player = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player);

        Debug.Log($"Player connected: {conn.connectionId}");
    }
}