using Photon.Realtime;
using UnityEngine;

public class CannonProjectile : MonoBehaviour {

    private Player _owner;

    public void InitializeBullet(Player owner) {
        this._owner = owner;

        Physics.IgnoreCollision(GetComponent<Collider>(), GameManager.instance.GetPlayer(owner).GetShipCollider());
    }

    public bool IsMine(Player owner) {
        return this._owner == owner ? true : false;
    }

}
