using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OxygenTank : MonoBehaviour
{
    [SerializeField] bool debug;   
    [SerializeField]
    public float oxygenReal = 100f;

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

    public float OxygenPercent =>  oxygenReal/ maxOxygen;

    [SerializeField]
    public List<OxygenSource> activeSources;


    private void Awake()
    {
        activeSources = new List<OxygenSource>();
    }

    public void AddSource(OxygenSource source)
    {
        //Debug.Log("Entering source");
        if (!activeSources.Contains(source))
        {
            activeSources.Add(source);
        }
    }

    public void LeaveSource(OxygenSource source)
    {
       // Debug.Log("Exiting source");
        if (activeSources.Contains(source))
        {
            activeSources.Remove(source);
        }
    }

    private void Update()
    {
        if (debug) return;
        //Debug.Log("Oxygen: " + oxygenRounded);
        Check();
    }

    public UnityEvent tankDepleted;
    void Die()
    {
        tankDepleted?.Invoke();
        //Debug.LogError("Player has died of oxygen loss");
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
        }
        else
        {
            Drain();
        }
        UpdateScale();
    }

    [SerializeField]
    UnityEvent<int, int> changedOxygen;
    void UpdateScale()
    {//Animate tank here
        changedOxygen?.Invoke(oxygenRounded,Mathf.RoundToInt(maxOxygen));
    }


    [SerializeField]
    UnityEvent tankFilled;

    bool refilling = false;
    [SerializeField]
    UnityEvent startedFilling;
    void Refill()
    {
        if(refilling==false)
        {
            startedFilling?.Invoke();
        }
        refilling = true;
        var rate = drainRate * 5;
        oxygenReal += Time.deltaTime * rate;
        if(oxygenReal>maxOxygen)
        {
            oxygenReal = maxOxygen;
            tankFilled?.Invoke();
        }
    }

    public void FillMax()
    {
        //Debug.Log("Maxing Tank: " + oxygenReal +" " + maxOxygen);
        startedFilling?.Invoke();
        oxygenReal = maxOxygen;
        tankFilled?.Invoke();
        UpdateScale();
    }

    public void UpdateMax(int additive)
    {
        maxOxygen += additive;
       // Debug.Log("New max " + maxOxygen);
        FillMax();
    }
    void Drain()
    {
        refilling = false;
        oxygenReal -= Mathf.Max(Time.deltaTime * drainRate, 0);
    }
}
