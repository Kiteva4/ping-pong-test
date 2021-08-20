using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text roomNameText;
    public RoomListContainer parent;
    private string _roomName;
    public string RoomName
    {
        set
        {
            _roomName = value;
            roomNameText.text = value;
        }
    }

    public void OnJoinRoom() => parent.OnPressJoinRoom(_roomName);
}
