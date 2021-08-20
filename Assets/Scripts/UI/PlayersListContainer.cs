using Photon.Realtime;
using UnityEngine;

public class PlayersListContainer : MonoBehaviour
{
    [SerializeField] 
    private PlayerListItem _playerItemPrefab;

    public void UpdatePlayersList(Player[] players)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var player in players)
        {
            Instantiate(_playerItemPrefab, transform).GetComponent<PlayerListItem>().SetUp(player);
        }
    }

    public void AddPlayer(Player player)
    {
        Instantiate(_playerItemPrefab, transform).GetComponent<PlayerListItem>().SetUp(player);
    }
}
