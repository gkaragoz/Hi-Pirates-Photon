using UnityEngine;
using Photon.Pun;
using TMPro;

public class ConnectionStatusProperty : MonoBehaviour {

    private readonly string connectionStatusMessage = "Connection Status: ";

    [SerializeField]
    private TextMeshProUGUI _txtConnectionStatus;

    private void Update() {
        _txtConnectionStatus.text = connectionStatusMessage + PhotonNetwork.NetworkClientState;
    }

}
