using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Joystick _joystick;

    public Vector2 CurrentInput { get; set; }
    private PhotonView _photonView { get; set; }

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
        _photonView = GetComponent<PhotonView>();
        _shipController = GetComponent<ShipController>();

        if (!_photonView.IsMine) {
            Destroy(_joystick.gameObject);
        }
    }

    private void Update() {
        if (!_photonView.IsMine) {
            return;
        }

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
