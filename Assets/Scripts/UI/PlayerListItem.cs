using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] 
    private TMP_Text playerNickName;
    private Player _player;
    
    public void SetUp(Player player)
    {
        _player = player;
        playerNickName.text = _player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (Equals(_player, otherPlayer))
        {
            Destroy(gameObject);
        }
    }
    
    public override void OnLeftRoom() => Destroy(gameObject);
}
