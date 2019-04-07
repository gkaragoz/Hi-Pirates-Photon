using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipStats))]
public class ShipAttack : MonoBehaviour {

    public enum Direction {
        Right,
        Left
    }

    [SerializeField]
    private GameObject _cannonProjectile;
    [SerializeField]
    private LaunchArcRenderer _launchArcRenderer;
    [SerializeField]
    private GameObject _cannonFireFX;
    [SerializeField]
    private GameObject _rightCannon;
    [SerializeField]
    private GameObject _leftCannon;
    [SerializeField]
    private float _chargeThreshold = 0.2f;

    public Vector3 ProjectileStartPosition { get; private set; }
    public Vector3 RightCannonPosition { get { return _rightCannon.transform.position; } }
    public Vector3 LeftCannonPosition { get { return _leftCannon.transform.position; } }

    private ShipStats _shipStats;

    [SerializeField]
    [Utils.ReadOnly]
    private bool _isFireRightCharging = false;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isFireLeftCharging = false;
    [SerializeField]
    [Utils.ReadOnly]
    private float _fireRightChargeTime = 0;
    [SerializeField]
    [Utils.ReadOnly]
    private float _fireLeftChargeTime = 0;
    [SerializeField]
    [Utils.ReadOnly]
    private float _fireRightChargeAmount = 0;
    [SerializeField]
    [Utils.ReadOnly]
    private float _fireLeftChargeAmount = 0;

    private BoxCollider _shipCollider;
    private PhotonView _photonView;

    private void Awake() {
        _shipStats = GetComponent<ShipStats>();
        _photonView = GetComponent<PhotonView>();
        _shipCollider = GetComponent<BoxCollider>();
    }

    private void Update() {
        if (!_isFireRightCharging && _fireRightChargeTime > 0) {
            _fireRightChargeTime -= Time.deltaTime;

            if (_fireRightChargeTime < 0) {
                _fireRightChargeTime = 0;
            }
        }

        if (!_isFireLeftCharging && _fireLeftChargeTime > 0) {
            _fireLeftChargeTime -= Time.deltaTime;

            if (_fireLeftChargeTime < 0) {
                _fireLeftChargeTime = 0;
            }
        }
    }

    public void ChargeFireRight() {
        // Charging...
        _isFireRightCharging = true;
        _fireRightChargeTime += Time.deltaTime;

        if (_fireRightChargeTime >= _shipStats.GetAttackSpeed()) {
            _fireRightChargeTime = _shipStats.GetAttackSpeed();
        }

        // Calculate charge amount based on charged time.
        _fireRightChargeAmount = Mathf.Clamp(_fireRightChargeTime, _chargeThreshold, 1f);
        // Draw preposition of launch arc.
        RenderArc(_shipStats.GetAttackRange() * _fireRightChargeAmount, LaunchArcRenderer.Direction.Right);
    }

    public void ChargeFireLeft() {
        // Charging...
        _isFireLeftCharging = true;
        _fireLeftChargeTime += Time.deltaTime;

        if (_fireLeftChargeTime >= _shipStats.GetAttackSpeed()) {
            _fireLeftChargeTime = _shipStats.GetAttackSpeed();
        }

        // Calculate charge amount based on charged time.
        _fireLeftChargeAmount = Mathf.Clamp(_fireLeftChargeTime, _chargeThreshold, 1f);
        // Draw preposition of launch arc.
        RenderArc(_shipStats.GetAttackRange() * _fireLeftChargeAmount, LaunchArcRenderer.Direction.Left);
    }

    public void ReleaseFireRight() {
        // Charge released.
        _isFireRightCharging = false;

        // Fire!
        FireRight();
    }

    public void ReleaseFireLeft() {
        // Charge released.
        _isFireLeftCharging = false;

        // Fire!
        FireLeft();
    }

    private void FireRight() {
        // Force for apply to projectile.
        Vector3 force = _launchArcRenderer.GetForceVector(transform.rotation.eulerAngles.y) * _shipStats.GetAttackRange() * _fireRightChargeAmount;

        // Start position about projectile.
        ProjectileStartPosition = _launchArcRenderer.transform.position;

        // Instantiate projectile and add force based on launchArcRenderer.
        Rigidbody projectile = Instantiate(_cannonProjectile, ProjectileStartPosition, Quaternion.identity).GetComponent<Rigidbody>();
        projectile.GetComponent<CannonProjectile>().InitializeBullet(_photonView.Owner);

        if (_photonView.IsMine) {
            projectile.AddForce(force, ForceMode.VelocityChange);

            _photonView.RPC("RPC_Fire", RpcTarget.Others, force, ProjectileStartPosition, RightCannonPosition, Direction.Right);
        }

        // Instantiate cannon fire FX.
        Instantiate(_cannonFireFX, RightCannonPosition, Quaternion.AngleAxis(transform.rotation.eulerAngles.y + 90, Vector3.up));

        // Set invisible of launchArcRenderer.
        _launchArcRenderer.SetVisibility(false);
    }

    private void FireLeft() {
        // Force for apply to projectile.
        Vector3 force = _launchArcRenderer.GetForceVector(transform.rotation.eulerAngles.y) * _shipStats.GetAttackRange() * _fireLeftChargeAmount;

        // Start position about projectile.
        ProjectileStartPosition = _launchArcRenderer.transform.position;

        // Instantiate projectile and add force based on launchArcRenderer.
        Rigidbody projectile = Instantiate(_cannonProjectile, ProjectileStartPosition, Quaternion.identity).GetComponent<Rigidbody>();
        projectile.GetComponent<CannonProjectile>().InitializeBullet(_photonView.Owner);

        if (_photonView.IsMine) {
            projectile.AddForce(force, ForceMode.VelocityChange);

            _photonView.RPC("RPC_Fire", RpcTarget.Others, force, ProjectileStartPosition, LeftCannonPosition, Direction.Left);
        }

        // Instantiate cannon fire FX.
        Instantiate(_cannonFireFX, LeftCannonPosition, Quaternion.AngleAxis(transform.rotation.eulerAngles.y - 90, Vector3.up));

        // Set invisible of launchArcRenderer.
        _launchArcRenderer.SetVisibility(false);
    }

    [PunRPC]
    private void RPC_Fire(Vector3 force, Vector3 startPos, Vector3 cannonPos, Direction direction, PhotonMessageInfo info) {
        Debug.Log("Someone fired: " + info.Sender.UserId);

        // Instantiate projectile and add force based on launchArcRenderer.
        Rigidbody projectile = Instantiate(_cannonProjectile, startPos, Quaternion.identity).GetComponent<Rigidbody>();
        projectile.GetComponent<CannonProjectile>().InitializeBullet(info.Sender);
        projectile.AddForce(force, ForceMode.VelocityChange);

        // Cannon Fire FX rotation.
        Quaternion FXrotation = Quaternion.identity;
        switch (direction) {
            case Direction.Right:
                FXrotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.y + 90, Vector3.up);
                break;
            case Direction.Left:
                FXrotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.y - 90, Vector3.up);
                break;
        }

        // Instantiate cannon fire FX.
        Instantiate(_cannonFireFX, cannonPos, FXrotation);
    }

    private void RenderArc(float velocity, LaunchArcRenderer.Direction direction) {
        // Set visible of launchArcRenderer.
        _launchArcRenderer.SetVisibility(true);

        // Keep calculate and render the launchArc.
        _launchArcRenderer.RenderArc(velocity, direction);
    }

}
