using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;


public class MultiplayerMenu : NetworkBehaviour

{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private TMP_InputField joinCodeInput;
    [SerializeField] private TMP_Text joinCodeText;
    [SerializeField] private TMP_Text statusText;

    private const string WebGLConnectionType = "wss";

    [Header("UI Reference")]
    public GameObject menuCanvas;

    public void StartHost()
    {
        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.UseWebSockets = true;
        NetworkManager.Singleton.StartHost();

        if (menuUI != null ) menuUI.SetActive(false);
    }

    public async Task StartClient()
    {
        JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCodeInput.text);
        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.UseWebSockets = true;
        NetworkManager.Singleton.StartHost();
        if (menuUI != null) menuUI.SetActive(false);



        NetworkManager.Singleton.StartClient();
        HideUI();
    }

    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
        HideUI();
    }

    private void HideUI()
    {
        // Disables the Canvas containing the buttons
        if (menuCanvas != null)
        {
            menuCanvas.SetActive(false);
        }
    }
}