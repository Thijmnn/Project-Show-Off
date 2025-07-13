using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    public Animator animator;
    public bool CanMove;
    public GameObject UI;
    private SpawnManager PlayerSpawner;
    public void IntroAnimDone()
    {
        animator.enabled = false;
    }

    private void Start()
    {
        PlayerSpawner = FindObjectOfType<SpawnManager>();
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
            wander.transform.gameObject.SetActive(false);
        }
        UI.SetActive(false);
        PlayerSpawner.gameObject.SetActive(false);
        Camera.main.gameObject.SetActive(false);
    }
}
