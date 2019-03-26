using UnityEngine;

public class CannonProjectile : MonoBehaviour {

    [SerializeField]
    private GameObject _waterSplashFX;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Water") {
            Instantiate(_waterSplashFX, new Vector3(transform.position.x, 0f, transform.position.z), Quaternion.identity);
        }    
    }

}
