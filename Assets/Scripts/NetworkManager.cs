using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Collections;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("Login")]
    public GameObject loginPn;
    public InputField playerName;
    string tempPlayerName;

    [Header("Lobby")]
    public GameObject lobbyPn;
    public InputField roomName;
    string tempRoomName;

    [Header("Player")]
    public GameObject playerPun;

    [Header("LoadingScreen")]
    public Text LoadingText;
    public int numeroDaCena = 2;

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        loginPn.gameObject.SetActive(true);
        tempPlayerName = "Player" + Random.Range(8, 99);
        playerName.text = tempPlayerName;
        tempRoomName = "Room" + Random.Range(8, 99);
        LoadingText.text = "Procurando Inimigos Proximos...";
    }

    public void Login()
    {
        if (playerName.text != "")
        {
            PhotonNetwork.NickName = playerName.text;
        }
        else
        {
            PhotonNetwork.NickName = tempPlayerName;
        }

        loginPn.gameObject.SetActive(false);
    }
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Quicksearch()
    {
        PhotonNetwork.JoinLobby();
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom(roomName.text, roomOptions, TypedLobby.Default);
    }

    public override void OnConnected()
    {
        Debug.LogWarning("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        LoadingText.text = "Conectado...";
        Debug.LogWarning("OnConnectedToMaster");
        Debug.LogWarning("Server: " + PhotonNetwork.CloudRegion);
        Debug.LogWarning("Ping: " + PhotonNetwork.GetPing());
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.LogWarning("OnJoinedLobby");
        LoadingText.text = "Oponente encontrado...";
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(tempRoomName);
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnJoinedRoom()
    {
        Debug.LogWarning("OnJoinedRoom");
        Debug.LogWarning("Nome da Sala: " + PhotonNetwork.CurrentRoom.Name);
        Debug.LogWarning("Nome do Player: " + PhotonNetwork.NickName);
        Debug.LogWarning("Players Conectados: " + PhotonNetwork.CurrentRoom.PlayerCount);

        loginPn.gameObject.SetActive(false);
        StartCoroutine(LoadMap(numeroDaCena));//Troca a cena
    }
    IEnumerator LoadMap(int value)
    {
        yield return new WaitForSeconds(3f);
        PhotonNetwork.LoadLevel(value);
    }
}
