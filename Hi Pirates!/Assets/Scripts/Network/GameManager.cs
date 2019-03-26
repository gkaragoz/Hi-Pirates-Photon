using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks {

    [SerializeField]
    private List<Player> _playerList = new List<Player>();

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        Debug.Log("OnPlayerEnteredRoom: (" + newPlayer.UserId + ")" + newPlayer.NickName);

        _playerList.Add(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        Debug.Log("OnPlayerLeftRoom: (" + otherPlayer.UserId + ")" + otherPlayer.NickName);

        _playerList.Add(otherPlayer);
    }

    public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps) {
        Debug.Log("OnPlayerPropertiesUpdate: (" + target.UserId + ")");
    }

}
