using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks {

    public string username;

    private void Start() {
        username = "Player " + Random.Range(1000, 10000);

        PhotonNetwork.LocalPlayer.NickName = username;
        PhotonNetwork.ConnectUsingSettings();

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster() {
        Debug.Log("OnConnectedToMaster");

        Debug.Log("Trying to JoinRandomRoom");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.Log("OnCreateRoomFailed: " + message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        Debug.Log("OnJoinRandomFailed: " + message);
        Debug.Log("Creating a new room.");

        string roomName = "Room " + Random.Range(1000, 10000);

        RoomOptions options = new RoomOptions { MaxPlayers = 8 };

        Debug.Log("roomName: " + roomName + "(" + options.MaxPlayers + ")");
        PhotonNetwork.CreateRoom(roomName, options, null);
    }

}
