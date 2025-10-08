using UnityEngine;
using Mirror;

public class NetworkSpawnManager : NetworkBehaviour
{
    public static NetworkSpawnManager Instance;

    [SerializeField] private GameObject cubePrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [Server]
    public void SpawnCube(Vector3 position, Quaternion rotation)
    {
        var cube = Instantiate(cubePrefab, position, rotation);
        NetworkServer.Spawn(cube);
    }
}