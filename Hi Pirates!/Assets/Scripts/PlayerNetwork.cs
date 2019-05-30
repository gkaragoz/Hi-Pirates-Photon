using Photon.Pun;
using UnityEngine;

public class PlayerNetwork : MonoBehaviourPun, IPunObservable {

    private PlayerController _playerController;
    private Vector3 _remotePlayerPosition;
    private Quaternion _remotePlayerQuaternion;

    private void Awake() {
        _playerController = GetComponent<PlayerController>();

        //destroy the controller if the player is not controlled by me
        if (!photonView.IsMine && _playerController != null)
            _playerController.IsRemotePlayer = true;
    }

    private void Update() {
        if (photonView.IsMine) {
            return;
        }

        Vector3 LagDistance = _remotePlayerPosition - transform.position;

        //High distance => sync is to much off => send to position
        if (LagDistance.magnitude > 5f) {
            transform.position = _remotePlayerPosition;
            LagDistance = Vector3.zero;
        }

        //ignore the y distance
        LagDistance.y = 0;

        Vector2 remoteInput = Vector2.zero;

        if (LagDistance.magnitude < 0.11f) {
            //Player is nearly at the point
            remoteInput = Vector2.zero;
        } else {
            //Player has to go to the point
            remoteInput = new Vector2(LagDistance.normalized.x, LagDistance.normalized.z);
        }

        _playerController.SetRemoteInput(remoteInput);
        _playerController.SetRemoteRotation(_remotePlayerQuaternion);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(_playerController.GetCurrentPosition());
            stream.SendNext(_playerController.GetCurrentRotation());
        } else {
            _remotePlayerPosition = (Vector3)stream.ReceiveNext();
            _remotePlayerQuaternion = (Quaternion)stream.ReceiveNext();
        }
    }

}
