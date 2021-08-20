using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuProvider : MonoBehaviour
{
    [SerializeField] private Lobby lobby;
    [SerializeField] private MenuSwitcher menuSwitcher;
    [SerializeField] private RoomListContainer roomListContainer;
    [SerializeField] private PlayersListContainer playersListContainer;
    
    [Header("Buttons")] 
    [SerializeField] private Button startSoloGameButton;
    [SerializeField] private Button createRoomButton;
    [SerializeField] private Button enterPlayerNameButton;
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button leaveRoomButton;
    [SerializeField] private Button findRoomButton;
    [SerializeField] private Button disconnectButton;
    [SerializeField] private Button quitGameButton;
    
    [Header("Input fields")] 
    [SerializeField] private TMP_InputField roomNameInputField;
    [SerializeField] private TMP_InputField playerNameInputField;

    [Header("Text fields")]
    [SerializeField] private TMP_Text roomNameText;
    [SerializeField] private TMP_Text errorText;
    
    private void OnEnable()
    {
        lobby.JoinedLobby += () => menuSwitcher.OpenMenu(MenuType.Multiplayer);
        
        lobby.LeftRoom += () => menuSwitcher.OpenMenu(MenuType.Multiplayer);
        lobby.CreateRoomFailed += (error) =>
        {
            errorText.text = error;
            menuSwitcher.OpenMenu(MenuType.Error);
        };
        
        lobby.PlayerEnteredRoom += OnPlayerEnteredRoom;
        lobby.JoinedRoom += OnJoinedRoom;
        lobby.RoomListUpdate += OnRoomListUpdate;
        lobby.Disconnect += OnDisconnected;
        roomListContainer.JoinRoom += JoinRoom;
        createRoomButton.onClick.AddListener(OnCreateRoomButtonClicked);
        enterPlayerNameButton.onClick.AddListener(OnPlayerNameEntered);
        leaveRoomButton.onClick.AddListener(OnLeaveButtonClicked);
        startGameButton.onClick.AddListener(OnStartGameButtonClicked);
        disconnectButton.onClick.AddListener(OnDisconnectButtonClicked);
        quitGameButton.onClick.AddListener(QuitGameButtonClicked);
        startSoloGameButton.onClick.AddListener(OnStartSoloGameButtonClicked);
    }



    private void OnDisable()
    {
        lobby.JoinedRoom -= OnJoinedRoom;
        lobby.RoomListUpdate -= OnRoomListUpdate;
        lobby.Disconnect -= OnDisconnected;
        lobby.PlayerEnteredRoom -= OnPlayerEnteredRoom;
        roomListContainer.JoinRoom -= JoinRoom;
        createRoomButton.onClick.RemoveListener(OnCreateRoomButtonClicked);
        enterPlayerNameButton.onClick.RemoveListener(OnPlayerNameEntered);
        leaveRoomButton.onClick.RemoveListener(OnLeaveButtonClicked);
        startGameButton.onClick.RemoveListener(OnStartGameButtonClicked);
        disconnectButton.onClick.RemoveListener(OnDisconnectButtonClicked);
        quitGameButton.onClick.RemoveListener(QuitGameButtonClicked);
        startSoloGameButton.onClick.RemoveListener(OnStartSoloGameButtonClicked);

    }
    
    private void OnStartSoloGameButtonClicked() => SceneManager.LoadScene(1);
    private void OnPlayerEnteredRoom(Player player) => playersListContainer.AddPlayer(player);
    private void OnLeaveButtonClicked() => lobby.LeaveFromRoom();
    private void OnCreateRoomButtonClicked()
    {
        if(string.IsNullOrEmpty(roomNameInputField.text))
            return;
        
        menuSwitcher.OpenMenu(MenuType.Loading);
        lobby.CreateRoom(roomNameInputField.text);
        roomNameInputField.text = "";
    }
    private void OnStartGameButtonClicked() => lobby.StartGame();
    private void OnDisconnectButtonClicked()
    {
        lobby.DisconnectFromServer();
        menuSwitcher.OpenMenu(MenuType.Loading);
    }
    private void QuitGameButtonClicked() => Application.Quit();
    private void OnPlayerNameEntered()
    {
        if(string.IsNullOrEmpty(playerNameInputField.text))
            return;
        
        menuSwitcher.OpenMenu(MenuType.Loading);
        lobby.Connect();
        lobby.ChangeNick(playerNameInputField.text);
        playerNameInputField.text = "";
    }
    private void OnJoinedRoom(string roomName, Player[] players)
    {
        menuSwitcher.OpenMenu(MenuType.Room);
        roomNameText.text = roomName;
        playersListContainer.UpdatePlayersList(players);
    }
    private void JoinRoom(string roomName) => lobby.JoinRoom(roomName);
    private void OnDisconnected()
    {
        Debug.Log("Disconnect");
        menuSwitcher.OpenMenu(MenuType.Main);
    }
    private void OnRoomListUpdate(List<(string, bool)> rooms) => roomListContainer.UpdateRoomsList(rooms);
}
