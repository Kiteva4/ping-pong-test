using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Lobby : MonoBehaviourPunCallbacks, ILobby
{
    public event Action JoinedLobby;
    public event Action<string, Player[]> JoinedRoom;
    public event Action LeftRoom;
    public event Action<Player> PlayerEnteredRoom;
    public event Action PlayerLeftRoom;
    public event Action<string> CreateRoomFailed;
    public event Action Disconnect;
    public event Action<List<(string, bool)>> RoomListUpdate;

    private string playerName;
    public void ChangeNick(string nickName) => playerName = nickName;
    
    #region Connect
    public void Connect()
    {
        Debug.Log("Connecting... to Master");
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    
    public void JoinLobby() => PhotonNetwork.JoinLobby();
    
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = playerName;
        JoinedLobby?.Invoke();
    }
    #endregion

    #region Room
    public void CreateRoom(string roomName)
    {
        if(string.IsNullOrEmpty(roomName))
            return;

        PhotonNetwork.CreateRoom(roomName);
    }
    public void JoinRoom(string roomName) => PhotonNetwork.JoinRoom(roomName);
    
    public override void OnJoinedRoom()
    {
        Debug.Log($"Joined Room {PhotonNetwork.CurrentRoom.Name}");
        JoinedRoom?.Invoke(PhotonNetwork.CurrentRoom.Name, PhotonNetwork.PlayerList);
    }

    public void LeaveFromRoom() => PhotonNetwork.LeaveRoom();
    public override void OnLeftRoom() => LeftRoom?.Invoke();
    public override void OnCreateRoomFailed(short returnCode, string message) => CreateRoomFailed?.Invoke(message);
    #endregion
    
    public override void OnRoomListUpdate(List<RoomInfo> roomList) => RoomListUpdate?.Invoke(
        roomList.ConvertAll(ri => (ri.Name,ri.RemovedFromList)));

    public void DisconnectFromServer() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause _) => Disconnect?.Invoke();

    public void StartGame() => PhotonNetwork.LoadLevel(1);

    public override void OnPlayerEnteredRoom(Player player) => PlayerEnteredRoom?.Invoke(player);
    public override void OnPlayerLeftRoom(Player otherPlayer) => PlayerLeftRoom?.Invoke();
}
