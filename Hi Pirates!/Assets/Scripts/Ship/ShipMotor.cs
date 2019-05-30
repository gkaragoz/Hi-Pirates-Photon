using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(ShipStats), typeof(Rigidbody))]
public class ShipMotor : MonoBehaviour {

    public int MyProperty { get; set; }

    private Vector2 _remoteInput;
    private Quaternion _remoteRotation;
    private float _remoteRotationSpeed = 4f;

    private ShipStats _shipStats;
    private Rigidbody _rb;
    private PhotonView _photonView;

    private void Awake() {
        _shipStats = GetComponent<ShipStats>();
        _rb = GetComponentInChildren<Rigidbody>();
        _photonView = GetComponentInChildren<PhotonView>();
    }

    private void FixedUpdate() {
        if (!_photonView.IsMine) {
            ProcessRemoteInput();
            ProcessRemoteRotation();
        }
    }

    private void ProcessRemoteInput() {
        _rb.velocity = new Vector3(_remoteInput.x * _shipStats.GetMovementSpeed(), 0f, _remoteInput.y * _shipStats.GetMovementSpeed());
    }

    private void ProcessRemoteRotation() {
        if (_remoteRotation.eulerAngles.magnitude <= 0) {
            return;
        }

        _rb.MoveRotation(Quaternion.Lerp(transform.rotation, _remoteRotation, Time.fixedDeltaTime * _remoteRotationSpeed));
    }

    public Vector3 GetCurrentPosition() {
        return new Vector3(_rb.position.x, 0f, _rb.position.z);
    }

    public Quaternion GetCurrentRotation() {
        return transform.rotation;
    }

    public void SetRemoteInput(Vector2 input) {
        _remoteInput = input;
    }

    public void SetRemoteRotation(Quaternion rotation) {
        _remoteRotation = rotation;
    }

    public void MoveToLocalInput(Vector2 input) {
        _rb.velocity = transform.forward * _shipStats.GetMovementSpeed() * input.magnitude;
    }

    public void RotateToLocalInput(Vector2 input) {
        if (input.magnitude <= 0) {
            return;
        }

        _rb.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(input.x, 0, input.y)), Time.fixedDeltaTime * _shipStats.GetRotationSpeed()));
    }

}
