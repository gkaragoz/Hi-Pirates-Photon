using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipMotor))]
public class ShipController : MonoBehaviour {

    private ShipMotor _shipMotor;

    private void Awake() {
        _shipMotor = GetComponent<ShipMotor>();
    }

    public void MoveToInput(Vector2 input) {
        _shipMotor.MoveToInput(input);
    }

}
