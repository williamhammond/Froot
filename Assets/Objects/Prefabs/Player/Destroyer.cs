using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destroyer : MonoBehaviour
{
    [SerializeField] Targeter targeter;
    [SerializeField] OxygenTank oxyTank;
    [SerializeField] float destroyCost = 10f;

    public UnityEvent<Buildable> onDestroyBuildable;

    public void DestroyAThing() => TryDestroy();
    public bool TryDestroy()
    {
        if (targeter.target == null) return false;

        if (targeter.target.occupant == null) return false;

        if (oxyTank.oxygenReal < destroyCost) return false;

        //Add cost here maybe some oxygen?

        onDestroyBuildable?.Invoke(targeter.target.occupant);

        Destroy(targeter.target.occupant.gameObject);
        
        oxyTank.oxygenReal -= destroyCost;

        return true;
    }
}
