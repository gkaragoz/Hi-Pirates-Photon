using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyTimer : MonoBehaviour {

    private void Awake() {
        Destroy(this.gameObject, GetComponent<ParticleSystem>().main.duration);
    }

}
