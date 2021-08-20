using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveFromRoom : MonoBehaviour
{
    public void Leave()
    {
        if(PhotonNetwork.NetworkClientState == ClientState.Joined)
            PhotonNetwork.Disconnect();

        SceneManager.LoadScene(0);
    }
}
