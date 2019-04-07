using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

[RequireComponent(typeof(ShipMotor), typeof(ShipAttack), typeof(ShipStats))]
public class ShipController : MonoBehaviour {

    [SerializeField]
    private GameObject _getDamageFX;

    private ShipMotor _shipMotor;
    private ShipAttack _shipAttack;
    private ShipStats _shipStats;
    private BoxCollider _shipCollider;
    private PhotonView _photonView;

    private void Awake() {
        _shipMotor = GetComponent<ShipMotor>();
        _shipAttack = GetComponent<ShipAttack>();
        _shipStats = GetComponent<ShipStats>();
        _photonView = GetComponent<PhotonView>();
        _shipCollider = GetComponent<BoxCollider>();
    }

    public void MoveToInput(Vector2 input) {
        _shipMotor.MoveToInput(input);
    }

    public void SetOwner(Player player) {
        _shipStats.Owner = player;
    }

    public void ChargeFireRight() {
        _shipAttack.ChargeFireRight();
    }

    public void ChargeFireLeft() {
        _shipAttack.ChargeFireLeft();
    }

    public void ReleaseFireRight() {
        _shipAttack.ReleaseFireRight();
    }

    public void ReleaseFireLeft() {
        _shipAttack.ReleaseFireLeft();
    }

    public void FireGetDamageFX(Vector3 hitPoint) {
        Instantiate(_getDamageFX, hitPoint, Quaternion.identity);
    }

    public Collider GetShipCollider() {
        return _shipCollider;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Projectile") {
            Vector3 hitPoint = new Vector3(other.transform.position.x, 0f, other.transform.position.z);
            FireGetDamageFX(hitPoint);

            Destroy(other.gameObject);
        }
    }

}
