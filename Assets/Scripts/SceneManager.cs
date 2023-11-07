using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : NetworkBehaviour
{
    [SerializeField] private NetworkObject _playerPrefab;

    public override void OnNetworkSpawn()
    {
        if (IsServer) NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += StartGame;
    }
    
    private void StartGame(string scenename, LoadSceneMode loadscenemode, List<ulong> clientscompleted, List<ulong> clientstimedout)
    {
        foreach (ulong id in NetworkManager.Singleton.ConnectedClientsIds)
        {
            NetworkObject spawned = Instantiate(_playerPrefab);
            spawned.SpawnAsPlayerObject(id);
        }
    }
}