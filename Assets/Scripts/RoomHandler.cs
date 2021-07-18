using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class RoomHandler : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField createInput, joinInput;

    [SerializeField]
    private Button createBtn, joinBtn;

    private void Awake()
    {
        createBtn.onClick.AddListener(CreateRoom);
        joinBtn.onClick.AddListener(JoinRoom);
    }
    private void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }
    private void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
