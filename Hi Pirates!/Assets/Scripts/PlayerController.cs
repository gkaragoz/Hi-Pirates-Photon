using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Joystick _joystick;
    [SerializeField]
    private GameObject _HUD;

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
            Destroy(_HUD.gameObject);
        } else {
            //_shipController.SetOwner(_photonView.Owner);
            Camera.main.GetComponent<CameraController>().SetTarget(this.transform);
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

        if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
            FireRight();
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
            FireLeft();
        }
    }

    public void Move() {
        _shipController.MoveToInput(CurrentInput);
    }

    public void FireRight() {
        _shipController.FireRight(1f);
    }
    
    public void FireLeft() {
        _shipController.FireLeft(1f);
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }

}
