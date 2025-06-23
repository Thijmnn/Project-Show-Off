using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangeBoost : Wander
{

    public SkinnedMeshRenderer matRenderer2;
    [HideInInspector] public UnityEvent GiveRange;
    bool canInteract = true;

    Vector3 originalScale;

    public float boostDuration;
    public float rangeIncrease;


    public override void GiveBoost()
    {
        if (!canInteract || BoostGiven) return;

        if (GiveRange.GetPersistentEventCount() == 0)
            GiveRange.AddListener(IncreaseRange);

        GiveRange?.Invoke();
        canInteract = false;
    }

    private void IncreaseRange()
    {
        StartCoroutine(RangeIncrease(boostDuration, rangeIncrease));
    }

    private IEnumerator RangeIncrease(float boostDur, float RangeInc)
    {
        if (BoostGiven) yield break;
        BoostGiven = true;
        try
        {
            originalScale = BlowrangeColl.transform.localScale;
            BlowrangeColl.transform.localScale = new Vector3(BlowrangeColl.transform.localScale.x * RangeInc, BlowrangeColl.transform.localScale.y, BlowrangeColl.transform.localScale.z * RangeInc);

            _blowScript.blowMulti *= 2;

            NotificationsAppear.Instance.ShowNoteHedgehog();

            Material outlineMat = matRenderer.materials.Length > 1 ? matRenderer.materials[1] : null;

            if (outlineMat != null)
            {
                Material[] playerMats = _playerRenderer.materials;
                Material[] updated = new Material[playerMats.Length + 1];

                for (int i = 0; i < playerMats.Length; i++)
                    updated[i] = playerMats[i];

                updated[playerMats.Length] = outlineMat;
                _playerRenderer.materials = updated;
            }

            trail.SetActive(false);
            matRenderer.materials = new[] { matRenderer.materials[0] };
            matRenderer2.materials = new[] { matRenderer2.materials[0] };

            yield return new WaitForSeconds(boostDur);

            Material[] revertMats = _playerRenderer.materials;
            if (revertMats.Length > 1)
            {
                Material[] trimmed = new Material[revertMats.Length - 1];
                for (int i = 0; i < trimmed.Length; i++)
                    trimmed[i] = revertMats[i];
                _playerRenderer.materials = trimmed;
            }

       
            _blowScript.blowMulti *= 0.5f;
            BlowrangeColl.transform.localScale = originalScale;
        }
        finally
        {
            BoostGiven = false;
        }

    }
}
