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
    }

    public void Move() {
        _shipController.MoveToInput(CurrentInput);
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }

}
