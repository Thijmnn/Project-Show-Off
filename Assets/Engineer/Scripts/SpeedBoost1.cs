using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedBoost1 : Wander
{
    public UnityEvent GiveSpeed;
    public override void GiveBoost()
    {
        GiveSpeed.Invoke();
    }

}
