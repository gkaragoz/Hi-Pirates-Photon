using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private Transform _target;

    [Header("Settings")]
    [SerializeField]
    private bool _blockProcess;
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private float minFov = 15f;
    [SerializeField]
    private float maxFov = 60f;
    [SerializeField]
    private float sensitivity = 10f;

    private void LateUpdate() {
        if (_blockProcess) {
            return;
        }
        if (_target == null) {
            return;
        }

        Vector3 desiredPosition = _target.position + _offset;
        transform.position = desiredPosition;

        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }

    public void ApplyRotation(int amount) {
        transform.Rotate(Vector3.right * amount);
    }

    public void SetTarget(Transform target) {
        this._target = target;
    }

}
