using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField]
    public GameObject target;
    [SerializeField]
    float speed = 1f;
    private void Update() {
         transform.position = Vector3.Lerp(transform.position, target.transform.position , speed * Time.deltaTime);
    }
}
