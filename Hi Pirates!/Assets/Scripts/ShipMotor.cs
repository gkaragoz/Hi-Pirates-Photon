using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMotor : MonoBehaviour {

    public bool goForward;
    public bool rotatingRight, rotatingLeft;
    public float movementSpeed;
    public float rotationSpeed;
    public float slopeSpeed;

    private float _rotationY, _rotationZ;

    private void FixedUpdate() {
        if (goForward)
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        if (rotatingRight) {
            _rotationY += Time.deltaTime * rotationSpeed;
            if (_rotationZ > -15f)
                _rotationZ -= Time.deltaTime * slopeSpeed;
            transform.rotation = Quaternion.Euler(0, _rotationY, _rotationZ);
        } else if (rotatingLeft) {
            _rotationY -= Time.deltaTime * rotationSpeed;
            if (_rotationZ < 15f)
                _rotationZ += Time.deltaTime * slopeSpeed;
            transform.rotation = Quaternion.Euler(0, _rotationY, _rotationZ);
        } else {
            if (_rotationZ > 0) {
                _rotationZ -= Time.deltaTime * slopeSpeed;
            } else if (_rotationZ < 0) {
                _rotationZ += Time.deltaTime * slopeSpeed;
            }
            transform.rotation = Quaternion.Euler(0, _rotationY, _rotationZ);
        }
    }

}
