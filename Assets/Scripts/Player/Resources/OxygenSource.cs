using UnityEngine;
using UnityEngine.Events;

public class OxygenSource : MonoBehaviour {
    public float refillRate = 1f;

    public UnityEvent<Vector3> enteredOxygen;
    public UnityEvent<Vector3> exitedOxygen;

    private void OnTriggerEnter (Collider other) {
        var tank = other.GetComponent<OxygenTank>();
        if (tank != null) {
            tank.AddSource(this);
            var point = other.ClosestPointOnBounds(transform.position);
            enteredOxygen?.Invoke(point);
        }
    }

    private void OnTriggerExit (Collider other) {
        var tank = other.GetComponent<OxygenTank>();
        if (tank != null) {
            tank.LeaveSource(this);
            var point = other.ClosestPointOnBounds(transform.position);
            exitedOxygen?.Invoke(point);
        }
    }
}