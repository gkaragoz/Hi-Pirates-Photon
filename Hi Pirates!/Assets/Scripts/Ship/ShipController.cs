using Photon.Realtime;
using UnityEngine;

[RequireComponent(typeof(ShipMotor))]
public class ShipController : MonoBehaviour {

    private ShipMotor _shipMotor;
    private ShipStats _shipStats;
    private void Awake() {
        _shipMotor = GetComponent<ShipMotor>();
        _shipStats = GetComponent<ShipStats>();
    }

    public void MoveToInput(Vector2 input) {
        _shipMotor.MoveToInput(input);
    }

    public void SetOwner(Player player)
    {
        _shipStats.Owner = player;
    }

}
