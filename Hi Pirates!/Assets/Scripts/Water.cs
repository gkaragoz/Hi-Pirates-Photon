using UnityEngine;

public class Water : MonoBehaviour {

    [SerializeField]
    private GameObject _waterSplashFX;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Projectile") {
            Instantiate(_waterSplashFX, new Vector3(other.transform.position.x, 0f, other.transform.position.z), Quaternion.identity);
            Destroy(other.gameObject);
        }
    }

}
