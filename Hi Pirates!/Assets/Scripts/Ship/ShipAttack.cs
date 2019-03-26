using UnityEngine;

[RequireComponent(typeof(ShipStats))]
public class ShipAttack : MonoBehaviour {

    [SerializeField]
    private LaunchArcRenderer _launchArcRenderer;

    private ShipStats _shipStats;

    private void Awake() {
        _shipStats = GetComponent<ShipStats>();
    }

    public void FireRight(float chargeAmount) {
        _launchArcRenderer.RenderArc(_shipStats.GetAttackRange() * chargeAmount, LaunchArcRenderer.Direction.Right);
    }

    public void FireLeft(float chargeAmount) {
        _launchArcRenderer.RenderArc(_shipStats.GetAttackRange() * chargeAmount, LaunchArcRenderer.Direction.Left);
    }

}
