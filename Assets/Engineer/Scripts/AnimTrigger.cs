using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    public Animator animator;
    public bool CanMove;
    public void IntroAnimDone()
    {
        animator.enabled = false;
    }

    public void OutroAnimStart()
    {
        animator.enabled = true;
    }
}
