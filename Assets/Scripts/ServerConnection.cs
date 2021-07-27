using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ServerConnection : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Image loadingScreen;
    [SerializeField]
    private Button btn;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        loadingScreen.gameObject.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        loadingScreen.gameObject.SetActive(false);
    }

}
