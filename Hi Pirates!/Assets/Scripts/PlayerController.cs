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

    public bool IsRemotePlayer { get; set; }

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private float _xInput, _yInput;
    private ShipController _shipController;

    private ButtonCharger _btnRightFire;
    private ButtonCharger _btnLeftFire;

    private PlayerNetwork _playerNetwork;

    private void Awake() {
        _playerNetwork = GetComponent<PlayerNetwork>();
        _shipController = GetComponent<ShipController>();

        if (!_playerNetwork.photonView.IsMine) {
            return;
        }

        _joystick = FindObjectOfType<Joystick>();

        _btnRightFire = GameObject.Find("BtnRightFire").GetComponent<ButtonCharger>();
        _btnLeftFire = GameObject.Find("BtnLeftFire").GetComponent<ButtonCharger>();

        _btnRightFire.onCharging += ChargeFireRight;
        _btnRightFire.onPointerUp += ReleaseFireRight;
        _btnLeftFire.onCharging += ChargeFireLeft;
        _btnLeftFire.onPointerUp += ReleaseFireLeft;

        Camera.main.GetComponent<CameraController>().SetTarget(this.transform);
    }

    private void Update() {
        if (IsRemotePlayer) {
            return;
        }

        if (_joystick == null) {
            Debug.Log("Joystick is missing!");
            return;
        }

        _xInput = _joystick.Horizontal;
        _yInput = _joystick.Vertical;

        CurrentInput = new Vector2(_xInput, _yInput);

        if (HasInput) {
            MoveToCurrentInput();
            RotateToCurrentInput();
        }

        if (Input.GetKey(KeyCode.KeypadPlus)) {
            ChargeFireRight();
        }

        if (Input.GetKeyUp(KeyCode.KeypadPlus)) {
            ReleaseFireRight();
        }

        if (Input.GetKey(KeyCode.KeypadMinus)) {
            ChargeFireLeft();
        }

        if (Input.GetKeyUp(KeyCode.KeypadMinus)) {
            ReleaseFireLeft();
        }
    }

    public Vector3 GetCurrentPosition() {
        return _shipController.GetCurrentPosition();
    }

    public Quaternion GetCurrentRotation() {
        return _shipController.GetCurrentRotation();
    }

    public void SetRemoteInput(Vector2 input) {
        _shipController.SetRemoteInput(input);
    }

    public void SetRemoteRotation(Quaternion rotation) {
        _shipController.SetRemoteRotation(rotation);
    }

    public void MoveToCurrentInput() {
        _shipController.MoveToLocalInput(CurrentInput);
    }

    public void RotateToCurrentInput() {
        _shipController.RotateToLocalInput(CurrentInput);
    }

    public void ChargeFireRight() {
        _shipController.ChargeFireRight();
    }

    public void ChargeFireLeft() {
        _shipController.ChargeFireLeft();
    }

    public void ReleaseFireRight() {
        _shipController.ReleaseFireRight();
    }

    public void ReleaseFireLeft() {
        _shipController.ReleaseFireLeft();
    }

    public Collider GetShipCollider() {
        return _shipController.GetShipCollider();
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }

}
