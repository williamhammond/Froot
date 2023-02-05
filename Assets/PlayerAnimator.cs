using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void SetHammerTrigger()
    {
        anim.SetTrigger("Hammer");
    }

    public void SetBuildTrigger() => anim.SetTrigger("Hammer");
}
