using Photon.Realtime;
using UnityEngine;

[RequireComponent(typeof(ShipMotor), typeof(ShipAttack), typeof(ShipStats))]
public class ShipController : MonoBehaviour {

    private ShipMotor _shipMotor;
    private ShipAttack _shipAttack;
    private ShipStats _shipStats;

    private void Awake() {
        _shipMotor = GetComponent<ShipMotor>();
        _shipAttack = GetComponent<ShipAttack>();
        _shipStats = GetComponent<ShipStats>();
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

}
