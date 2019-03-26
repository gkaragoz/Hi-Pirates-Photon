using Photon.Realtime;
using UnityEngine;

public class CannonProjectile : MonoBehaviour {

    [SerializeField]
    private GameObject _waterSplashFX;

    public Player Owner { get; private set; }
    public void InitializeBullet(Player owner)
    {
        Owner = owner;
    }
    public void Start()
    {
        Destroy(gameObject, 3.0f);
    }
    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Water") {
            Instantiate(_waterSplashFX, new Vector3(transform.position.x, 0f, transform.position.z), Quaternion.identity);
            Destroy(this.gameObject);
        }    
    }

}
