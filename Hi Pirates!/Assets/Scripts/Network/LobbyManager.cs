using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviourPunCallbacks {
    private Dictionary<int, GameObject> playerListEntries;

    public string username;

    private void Start() {
        username = "Player " + Random.Range(1000, 10000);

        PhotonNetwork.LocalPlayer.NickName = username;
        PhotonNetwork.ConnectUsingSettings();

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    #region PUN CALLBACKS

    public override void OnConnectedToMaster() {
        Debug.Log("connected to master");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message) {
        Debug.Log("OnJoinRoomFailed: " + message);
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.Log("OnCreateRoomFailed: " + message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        Debug.Log("OnJoinRandomFailed: " + message);
        Debug.Log("Creating Room because no random room found");

        string roomName = "Room " + Random.Range(1000, 10000);

        RoomOptions options = new RoomOptions { MaxPlayers = 8 };

        PhotonNetwork.CreateRoom(roomName, options, null);
    }

    public override void OnDisconnected(DisconnectCause cause) {
        base.OnDisconnected(cause);
        Debug.Log(cause);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {

    }

    public override void OnLeftLobby() {

    }

    public override void OnJoinedRoom() {
        if (playerListEntries == null) {
            playerListEntries = new Dictionary<int, GameObject>();
        }

        Debug.Log("joined room");
    }

    public override void OnLeftRoom() {
        Debug.Log("room left");

        PhotonNetwork.Disconnect();
    }

    public override void OnMasterClientSwitched(Player newMasterClient) {

    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) {

    }

    #endregion

}
