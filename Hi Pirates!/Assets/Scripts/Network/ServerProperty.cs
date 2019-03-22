using Photon.Pun;
using TMPro;
using UnityEngine;

public class ServerProperty : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _txtServerInfo;

    private int _cache = -1;

    private void Update() {
        if (PhotonNetwork.IsConnectedAndReady) {
            _txtServerInfo.text = "(" + PhotonNetwork.CloudRegion + ")" + PhotonNetwork.ServerAddress;
        } else {
            _txtServerInfo.text = "n/a";
        }
    }

}
