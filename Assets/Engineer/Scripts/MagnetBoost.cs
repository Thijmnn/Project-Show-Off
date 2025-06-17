using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagnetBoost : Wander
{

    public UnityEvent GiveMagnet;
    bool canInteract = true;

    public float boostDuration;
    public float pullVelocity;
    public BubbleBehaviour[] bubbles;

    public float magnetRange;

    public bool magnetic;

    
    public override void GiveBoost()
    {
        base.GiveBoost();
        GiveMagnet.AddListener(MagnetGive);
        if (canInteract) { GiveMagnet?.Invoke(); canInteract = false; }
        else
        {
            return;
        }

    }

    private void MagnetGive()
    {
        StartCoroutine(MagnetActive(boostDuration));
    }

    private IEnumerator MagnetActive(float boostDur)
    {
        magnetic = true;
        BoostGiven = true;

        yield return new WaitForSeconds(boostDur);

        BoostGiven = false;
        magnetic = false;
        

    }

    public override void Update()
    {
        base.Update();

        if (magnetic) {
            bubbles = FindObjectsByType<BubbleBehaviour>(FindObjectsSortMode.InstanceID);
            foreach (BubbleBehaviour bubble in bubbles)
            {
                Vector3 dist = _playerMovement.transform.position - bubble.transform.position;
                if (dist.magnitude < magnetRange)
                {
                    bubble.GetComponent<Rigidbody>().velocity += dist.normalized * pullVelocity;
                }
            }
        }
        
    }
}
