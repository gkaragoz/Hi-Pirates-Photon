using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPunObservable {

    [SerializeField]
    private Joystick _joystick;

    public Vector2 CurrentInput { get; set; }
    public PhotonView PhotonView { get; private set; }

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
        PhotonView = GetComponent<PhotonView>();
        _shipController = GetComponent<ShipController>();

        if (PhotonView.IsMine) {
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
        if (!PhotonView.IsMine) {
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(GetCurrentPosition());
            stream.SendNext(GetCurrentRotation());
            stream.SendNext(GetCurrentVelocity());
        } else {
            Vector3 nextPosition = (Vector3)stream.ReceiveNext();
            Quaternion nextRotation = (Quaternion)stream.ReceiveNext();
            SetVelocity((Vector3)stream.ReceiveNext());

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            nextPosition += GetCurrentVelocity() * lag;

            SetNextPosition(nextPosition);
            SetNextRotation(nextRotation);
        }
    }

    public Vector3 GetCurrentPosition() {
        return _shipController.GetCurrentPosition();
    }

    public Quaternion GetCurrentRotation() {
        return _shipController.GetCurrentRotation();
    }

    public Vector3 GetCurrentVelocity() {
        return _shipController.GetCurrentVelocity();
    }

    public void SetNextPosition(Vector3 position) {
        _shipController.SetNextPosition(position);
    }

    public void SetNextRotation(Quaternion rotation) {
        _shipController.SetNextRotation(rotation);
    }

    public void SetVelocity(Vector3 velocity) {
        _shipController.SetVelocity(velocity);
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

    public Collider GetShipCollider() {
        return _shipController.GetShipCollider();
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }

}
