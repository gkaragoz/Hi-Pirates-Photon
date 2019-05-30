using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks {

    #region Singleton

    public static GameManager instance;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion
    
    public PlayerController GetPlayer(Player player) {
        var playerList = GameObject.FindObjectsOfType<PlayerNetwork>();
        for (int ii = 0; ii < playerList.Length; ii++) {
            if (player == playerList[ii].photonView.Owner) {
                return playerList[ii].GetComponent<PlayerController>();
            }
        }
        return null;
    }

    public override void OnJoinedRoom() {
        Debug.Log("OnJoinedRoom");
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);
    }

    public override void OnLeftRoom() {
        Debug.Log("OnLeftRoom");
        PhotonNetwork.Disconnect();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        Debug.Log("OnPlayerEnteredRoom: (" + newPlayer.UserId + ")" + newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        Debug.Log("OnPlayerLeftRoom: (" + otherPlayer.UserId + ")" + otherPlayer.NickName);
    }

    public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps) {
        Debug.Log("OnPlayerPropertiesUpdate: (" + target.UserId + ")");

        if (changedProps.ContainsKey(GameVariables.PLAYER_HEALTH_FIELD)) {
            //check if owned ship has been died 
            return;
        }
    }

    public override void OnDisconnected(DisconnectCause cause) {
        Debug.Log(cause);
    }

}
