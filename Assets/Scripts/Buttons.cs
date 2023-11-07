using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Pool
{
    public class Buttons : MonoBehaviour
    {
        [SerializeField] private Button _client, _host;

        private void Start()
        {
            _client.onClick.AddListener((() => NetworkManager.Singleton.StartClient()));
            _host.onClick.AddListener((() => NetworkManager.Singleton.StartHost()));
        }
    }
}