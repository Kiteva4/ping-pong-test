using System;
using System.Collections.Generic;
using Photon.Realtime;

public interface ILobby
{
    void Connect();
    void ChangeNick(string nickName);
    
    void JoinLobby();
    event Action JoinedLobby;
    
    void CreateRoom(string roomName);
    event Action<List<(string, bool)>> RoomListUpdate;
    
    void JoinRoom(string roomName);
    event Action<string, Player[]> JoinedRoom;
    event Action<string> CreateRoomFailed;
    
    event Action Disconnect;
    void DisconnectFromServer();
    
    void StartGame();
    
    void LeaveFromRoom();
    event Action LeftRoom;
    
    event Action<Player> PlayerEnteredRoom;
    event Action PlayerLeftRoom;
}
