using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class SecondPlayerInitializer : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    private PhotonView _photonView;
    private Platform _platform;
    
    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _platform = GetComponent<Platform>();
    }

    private void Start() => SetupSecondPlayer();
    public override void OnPlayerEnteredRoom(Player newPlayer) => SetupSecondPlayer();

    public override void OnPlayerLeftRoom(Player oldPlayer)
    {
        if (PhotonNetwork.PlayerListOthers.Length >= 1)
            SetupSecondPlayer();
        else
        {
            _photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
            _platform.OnColorChanged(Color.blue);
        }
    }
    
    private void SetupSecondPlayer()
    {
        if (PhotonNetwork.PlayerListOthers.Length >= 1)
        {
            var p = PhotonNetwork.PlayerListOthers.FirstOrDefault();
            _photonView.RequestOwnership();
            _platform.OnColorChanged(Color.red);
        }
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (!Equals(requestingPlayer, _photonView.Owner))
        {
            _photonView.TransferOwnership(requestingPlayer);
        }
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        Debug.Log($" {targetView} changed owner from {previousOwner} to {targetView.Owner}");
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        Debug.Log($"Ownership transfer fail {targetView} with sender request {senderOfFailedRequest}");
    }
}
