using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    public Animator animator;
    public bool CanMove;
    public GameObject UI;
    public void IntroAnimDone()
    {
        animator.enabled = false;
    }

    public void OutroAnimStart()
    {
        animator.enabled = true;
        BlowingScript[] blows = FindObjectsOfType<BlowingScript>();
        foreach(BlowingScript b in blows)
        {
            b.transform.parent.gameObject.SetActive(false);
        }

        Wander[] wanderers = FindObjectsOfType<Wander>();
        foreach (Wander wander in wanderers)
        {
            wander.transform.gameObject.SetActive(true);
        }

        UI.SetActive(false);

        Camera.main.gameObject.SetActive(false);
    }
}
