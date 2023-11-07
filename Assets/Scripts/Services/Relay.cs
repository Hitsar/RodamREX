using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Services
{
    public class Relay : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _codeField;
        [SerializeField] private TMP_Text _codeText;
        
        private async void Start()
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        public async void CreateLobby()
        {
            try
            {
                Allocation allocation = await RelayService.Instance.CreateAllocationAsync(4);
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));
                _codeText.text = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
                NetworkManager.Singleton.StartHost();
            }
            catch (RelayServiceException e)
            {
                Debug.Log(e);
            }
        }

        public async void JoinLobby()
        {
            try
            {
                JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(_codeField.text);
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));

                NetworkManager.Singleton.StartClient();
                _codeText.text = "Joined lobby";
            }
            catch (RelayServiceException e)
            {
                Debug.Log(e);
            }
        }

        public void StartGame()
        {
            NetworkManager.Singleton.SceneManager.LoadScene("VSEnemies", LoadSceneMode.Single);
        }
    }
}