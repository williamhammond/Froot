using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OxygenTank : MonoBehaviour
{
    [SerializeField]
    public float oxygenReal { get; private set; }

    [SerializeField]
    public int oxygenRounded
    {
        get
        {
            return Mathf.RoundToInt(oxygenReal);
        }
    }
    [SerializeField]
    float maxOxygen;
    [SerializeField]
    float drainRate = 1f;

    [SerializeField]
    public Dictionary<OxygenSource, float> activeSources { get; private set; }

    private void Awake()
    {
        activeSources = new Dictionary<OxygenSource, float>();
    }

    public void AddSource(OxygenSource source, float rate)
    {
        if (!activeSources.ContainsKey(source))
        {
            activeSources.Add(source, rate);
        }
    }

    public void LeaveSource(OxygenSource source)
    {
        if (activeSources.ContainsKey(source))
        {
            activeSources.Remove(source);
        }
    }

    private void Update()
    {
        Check();
    }

    UnityEvent tankDepleted;
    void Die()
    {
        tankDepleted?.Invoke();
        Debug.LogError("Player has died of oxygen loss");
    }
    void Check()
    {
        if (oxygenRounded <= 0)
        {
            Die();
            return;
        }
        if (activeSources.Count > 0)
        {
            Refill();
            Drain();
        }
        UpdateScale();
    }

    void UpdateScale()
    {//Animate tank here
    }

    float GetMaxRate()
    {
        float max = 0;
        foreach (float value in activeSources.Values)
        {
            if (value > max)
            {
                max = value;
            }
        }
        return max;

    }

    UnityEvent tankFilled;
    void Refill()
    {
        var rate = GetMaxRate();
        oxygenReal += Time.deltaTime * rate;
        if(oxygenReal>maxOxygen)
        {
            oxygenReal = maxOxygen;
            tankFilled?.Invoke();
        }
    }
    void Drain()
    {
        oxygenReal -= Time.deltaTime * drainRate;
    }
}
