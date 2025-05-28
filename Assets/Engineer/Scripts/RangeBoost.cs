using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangeBoost : Wander
{
    

    public UnityEvent GiveRange;
    bool canInteract = true;

    Vector3 originalScale;

    public float boostDuration;
    public float rangeIncrease;
    public override void GiveBoost()
    {
        GiveRange.AddListener(IncreaseRange);
        if (canInteract) { GiveRange?.Invoke(); canInteract = false; }
        else
        {
            return;
        }

    }

    private void IncreaseRange()
    {
        StartCoroutine(RangeIncrease(boostDuration, rangeIncrease));
    }

    private IEnumerator RangeIncrease(float boostDur, float RangeInc)
    {
        originalScale = BlowrangeColl.transform.localScale;
        BlowrangeColl.transform.localScale = new Vector3(BlowrangeColl.transform.localScale.x * RangeInc, BlowrangeColl.transform.localScale.y, BlowrangeColl.transform.localScale.z * RangeInc);

        _blowScript.blowMulti *= 2;

        yield return new WaitForSeconds(boostDur);

        _blowScript.blowMulti *= 0.5f;
        BlowrangeColl.transform.localScale = originalScale;
        gameObject.SetActive(false);

    }
}
