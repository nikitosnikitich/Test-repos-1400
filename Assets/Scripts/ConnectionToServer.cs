using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using JetBrains.Annotations;
public class ConnectionToServer : MonoBehaviourPunCallbacks
{
    public static ConnectionToServer Instance;
    [SerializeField] private TMP_Text textCountOfPlayer;
    [SerializeField] private TMP_InputField inputRoomName;
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private Transform transformRoomList;
    [SerializeField] private GameObject roomItemPrefab;

    [SerializeField] private Transform transformPlayerList;
    [SerializeField] private GameObject PlayerListItem;

    [SerializeField] private GameObject startGameButton;

    private void Awake()
    {
        Instance = this;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
        Debug.Log("Your nickname - " + PhotonNetwork.NickName);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textCountOfPlayer.text = "Players: " + PhotonNetwork.CountOfPlayers.ToString();
    }
    public override void OnConnectedToMaster()  // callback при успешном подключении к серверу
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Welcome To Server");
        //
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Connect To Lobby");
        WindowsManager.Layout.OpenLayout("PanelMainMenu");
    }
    public void CreateNewRoom()
    {
        if (string.IsNullOrEmpty(inputRoomName.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(inputRoomName.text);
        Debug.Log("Room created with name: " + inputRoomName.text);
    }
    //
    public override void OnJoinedRoom()
    {
        WindowsManager.Layout.OpenLayout("Panel_GameRoom");
        //
        if(PhotonNetwork.IsMasterClient)
        {
            startGameButton.SetActive(true);
        }
        else
        {
             startGameButton.SetActive(false);
        }
        //
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        //
        Player [] players = PhotonNetwork.PlayerList;
        foreach(Transform trns in transformPlayerList)
        {
            Destroy(trns.gameObject);
        }
        for(int i = 0; i < players.Length; i++)
        {
            Instantiate(PlayerListItem, transformPlayerList).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
    }

    public override void OnPlayerEnteredRoom (Player newPlayer)
    {
        Instantiate(PlayerListItem, transformPlayerList).GetComponent<PlayerListItem>().SetUp(newPlayer);
        Debug.Log("New player join room - " + newPlayer.NickName);
    }
    //
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trns in transformRoomList)
        {
            Destroy(trns.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(roomItemPrefab, transformRoomList).GetComponent<RoomItem>().SetUp(roomList[i]);
        }
    }
    public void JoinRoom(RoomInfo info) 
    {
        PhotonNetwork.JoinRoom(info.Name);
    }

    public void ConnectToRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            startGameButton.SetActive(true);
        }
        else
        {
            startGameButton.SetActive(false);
        }
    }
    public void StartGameLevel(int levelIndex)
    {
        PhotonNetwork.LoadLevel(levelIndex);
    }
}
