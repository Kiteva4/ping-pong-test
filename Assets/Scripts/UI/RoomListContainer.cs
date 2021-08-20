using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomListContainer : MonoBehaviour
{
    public event Action<string> JoinRoom;
    [SerializeField] 
    private RoomListItem _roomListItemPrefab;
    public void UpdateRoomsList(List<(string, bool)> rooms)
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }

        foreach (var roomInfo in rooms.Where(roomInfo => !roomInfo.Item2))
        {
            var roomItem = Instantiate(_roomListItemPrefab, transform).GetComponent<RoomListItem>();
            roomItem.RoomName = roomInfo.Item1;
            roomItem.parent = this;
        }
    }

    public void OnPressJoinRoom(string roomName) => JoinRoom?.Invoke(roomName);
}
