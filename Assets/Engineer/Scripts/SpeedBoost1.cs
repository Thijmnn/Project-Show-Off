using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedBoost1 : Wander
{
    public UnityEvent GiveSpeed;
    bool canInteract = true;

    float originalSpeed;
    float originalSprintSpeed;
    public float boostDuration;
    public float moveSpeedIncrease;
    public override void GiveBoost()
    {
        base.GiveBoost();
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
        BoostGiven = true;
        originalSprintSpeed = _playerMovement.newSpeed;
        originalSpeed = _playerMovement.originalSpeed;
        _playerMovement.originalSpeed *= speedInc;
        _playerMovement.newSpeed *= speedInc;

        yield return new WaitForSeconds(boostDur);

        BoostGiven = false;
        _playerMovement.newSpeed = originalSprintSpeed;
        _playerMovement.originalSpeed = originalSpeed;

        
    }
}
