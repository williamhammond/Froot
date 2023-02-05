using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Targeter : MonoBehaviour
{
    List<Tile> targeted;
    
    public Tile target;

    [SerializeField]float distanceInfront = 1.5f;
    [SerializeField]float distanceUp = 1f;
    [SerializeField]LayerMask environmentLayer;
    private void Awake()
    {
        targeted = new List<Tile>();
    }

    private void Update()
    {
        if(Physics.Raycast(transform.parent.position + transform.parent.forward * distanceInfront + Vector3.up * distanceUp,Vector3.down,
            out RaycastHit hit, 10f,environmentLayer))
        {
           if(hit.collider.transform.parent.parent.gameObject.TryGetComponent<Tile>(out Tile t))
            {
                if(target != t && t.IsInteractable)
                {
                    if(target != null)
                        target.Deselect();
                    target = t;
                    target.Select();

                    newTarget?.Invoke(target);

                }
                return;

            }
        }

        if(target != null)
        {
            target.Deselect();
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        var rayStart = transform.parent.position + transform.parent.forward * distanceInfront + Vector3.up * distanceUp;

        Gizmos.DrawSphere(rayStart, .2f);
        if (Physics.Raycast(transform.parent.position + transform.parent.forward * distanceInfront + Vector3.up * distanceUp, Vector3.down,
           out RaycastHit hit, 10f, environmentLayer))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hit.point, .2f);
        }

        Gizmos.color = Color.white;

    }



    public UnityEvent lostTarget;
    public UnityEvent<Tile> newTarget;

}
