using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(ShipStats))]
public class ShipAttack : MonoBehaviour
{

    [SerializeField]
    private CannonProjectile _cannonProjectile;
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

    private void Awake()
    {
        _shipStats = GetComponent<ShipStats>();
    }

    private void Update()
    {
        if (!_isFireRightCharging && _fireRightChargeTime > 0)
        {
            _fireRightChargeTime -= Time.deltaTime;

            if (_fireRightChargeTime < 0)
            {
                _fireRightChargeTime = 0;
            }
        }

        if (!_isFireLeftCharging && _fireLeftChargeTime > 0)
        {
            _fireLeftChargeTime -= Time.deltaTime;

            if (_fireLeftChargeTime < 0)
            {
                _fireLeftChargeTime = 0;
            }
        }
    }

    public void ChargeFireRight()
    {
        // Charging...
        _isFireRightCharging = true;
        _fireRightChargeTime += Time.deltaTime;

        if (_fireRightChargeTime >= _shipStats.GetAttackSpeed())
        {
            _fireRightChargeTime = _shipStats.GetAttackSpeed();
        }

        // Calculate charge amount based on charged time.
        _fireRightChargeAmount = Mathf.Clamp(_fireRightChargeTime, _chargeThreshold, 1f);
        // Draw preposition of launch arc.
        RenderArc(_shipStats.GetAttackRange() * _fireRightChargeAmount, LaunchArcRenderer.Direction.Right);
    }

    public void ChargeFireLeft()
    {
        // Charging...
        _isFireLeftCharging = true;
        _fireLeftChargeTime += Time.deltaTime;

        if (_fireLeftChargeTime >= _shipStats.GetAttackSpeed())
        {
            _fireLeftChargeTime = _shipStats.GetAttackSpeed();
        }

        // Calculate charge amount based on charged time.
        _fireLeftChargeAmount = Mathf.Clamp(_fireLeftChargeTime, _chargeThreshold, 1f);
        // Draw preposition of launch arc.
        RenderArc(_shipStats.GetAttackRange() * _fireLeftChargeAmount, LaunchArcRenderer.Direction.Left);
    }

    public void ReleaseFireRight(PhotonView photonview, Quaternion rot, float chargeAmount = 0)
    {
        // Charge released.
        _isFireRightCharging = false;

        // Fire!
        FireRight(photonview, rot, chargeAmount);
    }

    public void ReleaseFireLeft(PhotonView photonview, Quaternion rot, float chargeAmount = 0)
    {
        // Charge released.
        _isFireLeftCharging = false;

        // Fire!
        FireLeft(photonview, rot, chargeAmount);
    }

    private void FireRight(PhotonView photonview, Quaternion rot, float chargeAmount = 0)
    {
        // Instantiate projectile and add force based on launchArcRenderer.
        _cannonProjectile.InitializeBullet(_shipStats.Owner);
        Rigidbody projectile = Instantiate(_cannonProjectile, _launchArcRenderer.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

        Debug.Log("eulerAngles: " + transform.rotation.eulerAngles.y);
        if (chargeAmount > 0)
            _fireRightChargeAmount = chargeAmount;

        if (photonview.IsMine)
        {
            photonview.RPC("FireRight", RpcTarget.AllViaServer, _fireRightChargeAmount, transform.rotation);
            projectile.AddForce(_launchArcRenderer.GetForceVector(transform.rotation) * _shipStats.GetAttackRange() * _fireRightChargeAmount, ForceMode.VelocityChange);

        }
        else
        {
            projectile.AddForce(_launchArcRenderer.GetForceVector(rot) * _shipStats.GetAttackRange() * _fireRightChargeAmount, ForceMode.VelocityChange);
        }


        //projectile.AddForce(Vector3.one * _shipStats.GetAttackRange() * _fireRightChargeAmount, ForceMode.VelocityChange);

        // Instantiate cannon fire FX.
        Instantiate(_cannonFireFX, _rightCannon.transform.position, Quaternion.AngleAxis(transform.rotation.eulerAngles.y + 90, Vector3.up));
      
        // Set invisible of launchArcRenderer.
        _launchArcRenderer.SetVisibility(false);
    }

    private void FireLeft(PhotonView photonview, Quaternion rot, float chargeAmount = 0)
    {
        // Instantiate projectile and add force based on launchArcRenderer.
        _cannonProjectile.InitializeBullet(_shipStats.Owner);
        Rigidbody projectile = Instantiate(_cannonProjectile, _launchArcRenderer.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        Debug.Log("total : " + _launchArcRenderer.GetForceVector(transform.rotation) * _shipStats.GetAttackRange() * chargeAmount);
        Debug.Log("charge: " + chargeAmount.ToString());
        Debug.Log("GetAttackRange(): " + _shipStats.GetAttackRange().ToString());
        Debug.Log("get force vector :" + _launchArcRenderer.GetForceVector(transform.rotation));
        if (chargeAmount > 0)
            _fireLeftChargeAmount = chargeAmount;

        if (photonview.IsMine)
        {
            photonview.RPC("FireLeft", RpcTarget.AllViaServer, _fireLeftChargeAmount, transform.rotation);
            projectile.AddForce(_launchArcRenderer.GetForceVector(transform.rotation) * _shipStats.GetAttackRange() * _fireLeftChargeAmount, ForceMode.VelocityChange);
        }
        else
        {
            projectile.AddForce(_launchArcRenderer.GetForceVector(rot) * _shipStats.GetAttackRange() * _fireLeftChargeAmount, ForceMode.VelocityChange);
        }

        // Instantiate cannon fire FX.
        //projectile.AddForce(Vector3.one * _shipStats.GetAttackRange() * _fireLeftChargeAmount, ForceMode.VelocityChange);

        Instantiate(_cannonFireFX, _leftCannon.transform.position, Quaternion.AngleAxis(transform.rotation.eulerAngles.y - 90, Vector3.up));
     
        // Set invisible of launchArcRenderer.
        _launchArcRenderer.SetVisibility(false);
    }

    private void RenderArc(float velocity, LaunchArcRenderer.Direction direction)
    {
        // Set visible of launchArcRenderer.
        _launchArcRenderer.SetVisibility(true);

        // Keep calculate and render the launchArc.
        _launchArcRenderer.RenderArc(velocity, direction);
    }

}
