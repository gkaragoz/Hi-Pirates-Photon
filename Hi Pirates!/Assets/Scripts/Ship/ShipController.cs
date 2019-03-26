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

    public void FireRight(float chargeAmount) {
        _shipAttack.FireRight(chargeAmount);
    }

    public void FireLeft(float chargeAmount) {
        _shipAttack.FireLeft(chargeAmount);
    }

}
