using UnityEngine;

[RequireComponent(typeof(ShipStats))]
public class ShipAttack : MonoBehaviour {

    [SerializeField]
    private LaunchArcRenderer _launchArcRenderer;
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
    }

    public void ChargeFireLeft() {
        _isFireLeftCharging = true;

        _fireLeftChargeTime += Time.deltaTime;

        if (_fireLeftChargeTime >= _shipStats.GetAttackSpeed()) {
            _fireLeftChargeTime = _shipStats.GetAttackSpeed();
        }
    }

    public void ReleaseFireRight() {
        _isFireRightCharging = false;

        FireRight();
    }

    public void ReleaseFireLeft() {
        _isFireLeftCharging = false;

        FireLeft();
    }

    public void FireRight() {
        float chargeAmount = Mathf.Clamp(_fireRightChargeTime, _chargeThreshold, 1f);
        _launchArcRenderer.RenderArc(_shipStats.GetAttackRange() * chargeAmount, LaunchArcRenderer.Direction.Right);
    }

    public void FireLeft() {
        float chargeAmount = Mathf.Clamp(_fireLeftChargeTime, _chargeThreshold, 1f);
        _launchArcRenderer.RenderArc(_shipStats.GetAttackRange() * chargeAmount, LaunchArcRenderer.Direction.Left);
    }

}
