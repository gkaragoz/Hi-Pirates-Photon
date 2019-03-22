using UnityEngine;

public class ShipMotor : MonoBehaviour {

    public float movementSpeed;
    public float rotationSpeed;

    private Rigidbody _rb;

    private void Awake() {
        _rb = GetComponentInChildren<Rigidbody>();
    }

    public void MoveToInput(Vector2 input) {
        _rb.velocity = transform.forward * movementSpeed * input.magnitude;
        _rb.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(input.x, 0, input.y)), Time.fixedDeltaTime * rotationSpeed);
    }

    public void Stop() {
        _rb.velocity = Vector3.zero;
    }

}
