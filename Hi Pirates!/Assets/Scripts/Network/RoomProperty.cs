using UnityEngine;
using Photon.Pun;
using TMPro;

public class RoomProperty : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _txtCountOfRooms;
    [SerializeField]
    private TextMeshProUGUI _txtCountOfPlayers;

    private int _cache = -1;

    private void Update() {
        if (PhotonNetwork.IsConnectedAndReady) {
            _txtCountOfRooms.text = "Active Rooms:" + PhotonNetwork.CountOfRooms;
            _txtCountOfPlayers.text = "Total Players: " + PhotonNetwork.CountOfPlayersOnMaster;
        } else {
            _txtCountOfRooms.text = "n/a";
            _txtCountOfPlayers.text = "n/a";
        }
    }

}
