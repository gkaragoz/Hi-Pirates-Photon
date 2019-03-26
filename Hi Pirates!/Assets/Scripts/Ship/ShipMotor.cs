using UnityEngine;

[RequireComponent(typeof(ShipStats), typeof(Rigidbody))]
public class ShipMotor : MonoBehaviour {

    private ShipStats _shipStats;
    private Rigidbody _rb;

    private void Awake() {
        _shipStats = GetComponent<ShipStats>();
        _rb = GetComponentInChildren<Rigidbody>();
    }

    public void MoveToInput(Vector2 input) {
        _rb.velocity = transform.forward * _shipStats.GetMovementSpeed() * input.magnitude;
        _rb.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(input.x, 0, input.y)), Time.deltaTime * _shipStats.GetRotationSpeed()));
    }

}
