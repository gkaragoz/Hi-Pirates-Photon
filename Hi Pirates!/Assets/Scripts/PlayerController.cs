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

    [PunRPC]
    public void FireRight(int test, PhotonMessageInfo info)
    {
        //float lag = (float)(PhotonNetwork.Time - info.SentServerTime);

        _shipController.ReleaseFireRight();

    }
    [PunRPC]
    public void FireLeft(int test, PhotonMessageInfo info)
    {
        //float lag = (float)(PhotonNetwork.Time - info.SentServerTime);
 
        _shipController.ReleaseFireLeft();

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
        _photonView.RPC("FireRight", RpcTarget.AllViaServer, 0);
        //_shipController.ReleaseFireRight();
    }
    
    public void ReleaseFireLeft() {
        _photonView.RPC("FireLeft", RpcTarget.AllViaServer, 0);
        //_shipController.ReleaseFireLeft();
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }

}
