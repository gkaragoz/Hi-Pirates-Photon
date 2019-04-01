using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCharger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public Action onPointerDown;
    public Action onPointerUp;
    public Action onCharging;

    [SerializeField]
    [Utils.ReadOnly]
    private bool _isCharging = false;

    private void Update() {
        if (_isCharging) {
            onCharging?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        _isCharging = true;
        onPointerDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData) {
        _isCharging = false;
        onPointerUp?.Invoke();
    }

}
