using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Joystick _joystick;

    public Vector2 CurrentInput { get; set; }

    public bool HasInput {
        get {
            return (CurrentInput != Vector2.zero) ? true : false;
        }
    }

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private float _xInput, _yInput;
    private ShipController _shipController;

    private void Awake() {
        _shipController = GetComponent<ShipController>();
    }

    private void Update() {
        _xInput = _joystick.Horizontal;
        _yInput = _joystick.Vertical;

        CurrentInput = new Vector2(_xInput, _yInput);

        if (HasInput) {
            Move();
        }
    }

    public void Move() {
        _shipController.MoveToInput(CurrentInput);
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }

}
