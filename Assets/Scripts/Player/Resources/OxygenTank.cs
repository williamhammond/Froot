using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OxygenTank : MonoBehaviour {
    [SerializeField]
    public float oxygenReal = 100f;
    [SerializeField] private float maxOxygen;
    [SerializeField] private float drainRate = 1f;

    [SerializeField]
    public List<OxygenSource> activeSources;

    [SerializeField] private UnityEvent<string> changedOxygen;

    private UnityEvent tankDepleted;


    private UnityEvent tankFilled;

    [SerializeField]
    public int oxygenRounded => Mathf.RoundToInt(oxygenReal);

    public float OxygenPercent => oxygenReal / maxOxygen;


    private void Awake () {
        activeSources = new List<OxygenSource>();
    }

    private void Update () {
        //Debug.Log("Oxygen: " + oxygenRounded);
        Check();
    }

    public void AddSource (OxygenSource source) {
        //Debug.Log("Entering source");
        if (!activeSources.Contains(source)) {
            activeSources.Add(source);
        }
    }

    public void LeaveSource (OxygenSource source) {
        // Debug.Log("Exiting source");
        if (activeSources.Contains(source)) {
            activeSources.Remove(source);
        }
    }
    private void Die () {
        tankDepleted?.Invoke();
        //Debug.LogError("Player has died of oxygen loss");
    }
    private void Check () {
        if (oxygenRounded <= 0) {
            Die();
            return;
        }
        if (activeSources.Count > 0) {
            Refill();
        } else {
            Drain();
        }
        UpdateScale();
    }
    private void UpdateScale () {//Animate tank here
        changedOxygen?.Invoke(oxygenRounded.ToString());
    }
    private void Refill () {
        var rate = drainRate * 5;
        oxygenReal += Time.deltaTime * rate;
        if (oxygenReal > maxOxygen) {
            oxygenReal = maxOxygen;
            tankFilled?.Invoke();
        }
    }
    private void Drain () {
        oxygenReal -= Mathf.Max(Time.deltaTime * drainRate, 0);
    }
}