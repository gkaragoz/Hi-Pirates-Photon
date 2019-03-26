using UnityEngine;

[RequireComponent(typeof(ShipStats))]
public class ShipAttack : MonoBehaviour {

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

    private void Awake() {
        _shipStats = GetComponent<ShipStats>();
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
        _isFireRightCharging = true;

        _fireRightChargeTime += Time.deltaTime;

        if (_fireRightChargeTime >= _shipStats.GetAttackSpeed()) {
            _fireRightChargeTime = _shipStats.GetAttackSpeed();
        }

        _fireRightChargeAmount = Mathf.Clamp(_fireRightChargeTime, _chargeThreshold, 1f);
        RenderArc(_shipStats.GetAttackRange() * _fireRightChargeAmount, LaunchArcRenderer.Direction.Right);
    }

    public void ChargeFireLeft() {
        _isFireLeftCharging = true;

        _fireLeftChargeTime += Time.deltaTime;

        if (_fireLeftChargeTime >= _shipStats.GetAttackSpeed()) {
            _fireLeftChargeTime = _shipStats.GetAttackSpeed();
        }

        _fireLeftChargeAmount = Mathf.Clamp(_fireLeftChargeTime, _chargeThreshold, 1f);
        RenderArc(_shipStats.GetAttackRange() * _fireLeftChargeAmount, LaunchArcRenderer.Direction.Left);
    }

    public void ReleaseFireRight() {
        _isFireRightCharging = false;

        FireRight();
    }

    public void ReleaseFireLeft() {
        _isFireLeftCharging = false;

        FireLeft();
    }

    private void FireRight() {
        Rigidbody projectile = Instantiate(_cannonProjectile, _launchArcRenderer.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        projectile.AddForce(_launchArcRenderer.GetForceVector() * _shipStats.GetAttackRange() * _fireRightChargeAmount, ForceMode.VelocityChange);

        Instantiate(_cannonFireFX, _rightCannon.transform.position, Quaternion.AngleAxis(transform.localRotation.y + 90, Vector3.up));
        _launchArcRenderer.SetVisibility(false);
    }

    private void FireLeft() {
        Rigidbody projectile = Instantiate(_cannonProjectile, _launchArcRenderer.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        projectile.AddForce(_launchArcRenderer.GetForceVector() * _shipStats.GetAttackRange() * _fireLeftChargeAmount, ForceMode.VelocityChange);

        Instantiate(_cannonFireFX, _leftCannon.transform.position, Quaternion.AngleAxis(transform.localRotation.y - 90, Vector3.up));
        _launchArcRenderer.SetVisibility(false);
    }

    private void RenderArc(float velocity, LaunchArcRenderer.Direction direction) {
        _launchArcRenderer.SetVisibility(true);
        _launchArcRenderer.RenderArc(velocity, direction);
    }

}
