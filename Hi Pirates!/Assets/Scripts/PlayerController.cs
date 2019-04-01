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

    private ButtonCharger _btnRightFire;
    private ButtonCharger _btnLeftFire;

    private void Awake() {
        _photonView = GetComponent<PhotonView>();
        _shipController = GetComponent<ShipController>();

        if (_photonView.IsMine) {
            _joystick = FindObjectOfType<Joystick>();

            _btnRightFire = GameObject.Find("BtnRightFire").GetComponent<ButtonCharger>();
            _btnLeftFire = GameObject.Find("BtnLeftFire").GetComponent<ButtonCharger>();

            _btnRightFire.onCharging += ChargeFireRight;
            _btnRightFire.onPointerUp += ReleaseFireRight;
            _btnLeftFire.onCharging += ChargeFireLeft;
            _btnLeftFire.onPointerUp += ReleaseFireLeft;

            Camera.main.GetComponent<CameraController>().SetTarget(this.transform);
        }
    }

    private void Update() {
        if (!_photonView.IsMine) {
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
            Move();
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

    public void Move() {
        _shipController.MoveToInput(CurrentInput);
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

    public void Destroy() {
        Destroy(this.gameObject);
    }

}
