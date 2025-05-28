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
        GiveMagnet.AddListener(IncreaseSpeed);
        if (canInteract) { GiveMagnet?.Invoke(); canInteract = false; }
        else
        {
            return;
        }

    }

    private void IncreaseSpeed()
    {
        StartCoroutine(SpeedIncrease(boostDuration));
    }

    private IEnumerator SpeedIncrease(float boostDur)
    {
        magnetic = true;
        

        yield return new WaitForSeconds(boostDur);

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
