using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedBoost1 : Wander
{
    public UnityEvent GiveSpeed;
    bool canInteract = true;

    float originalSpeed;
    public float boostDuration;
    public float moveSpeedIncrease;
    public override void GiveBoost()
    {
        GiveSpeed.AddListener(IncreaseSpeed);
        if (canInteract) { GiveSpeed?.Invoke(); canInteract = false; }
        else
        {
            return;
        }
        
    }

    private void IncreaseSpeed()
    {
        StartCoroutine(SpeedIncrease(boostDuration, moveSpeedIncrease));
    }

    private IEnumerator SpeedIncrease(float boostDur, float speedInc)
    {
        originalSpeed = _playerMovement.originalSpeed;
        _playerMovement.originalSpeed *= speedInc;

        yield return new WaitForSeconds(boostDur);

        _playerMovement.originalSpeed = originalSpeed;

    }
}
