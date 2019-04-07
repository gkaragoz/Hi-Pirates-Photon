using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(ShipStats), typeof(Rigidbody))]
public class ShipMotor : MonoBehaviour {

    public int MyProperty { get; set; }

    private Vector3 _nextPosition;
    private Quaternion _nextRotation;

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
            _rb.position = Vector3.MoveTowards(_rb.position, _nextPosition, Time.fixedDeltaTime);
            _rb.rotation = Quaternion.RotateTowards(_rb.rotation, _nextRotation, Time.fixedDeltaTime * 100f);
        }
    }

    public Vector3 GetCurrentPosition() {
        return new Vector3(_rb.position.x, 0f, _rb.position.z);
    }

    public Quaternion GetCurrentRotation() {
        return _rb.rotation;
    }

    public Vector3 GetCurrentVelocity() {
        return _rb.velocity;
    }

    public void SetNextPosition(Vector3 position) {
        _nextPosition = position;
    }

    public void SetNextRotation(Quaternion rotation) {
        _nextRotation = rotation;
    }

    public void SetVelocity(Vector3 velocity) {
        _rb.velocity = velocity;
    }

    public void MoveToInput(Vector2 input) {
        _rb.velocity = transform.forward * _shipStats.GetMovementSpeed() * input.magnitude;
        _rb.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(input.x, 0, input.y)), Time.deltaTime * _shipStats.GetRotationSpeed()));
    }

}
