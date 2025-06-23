using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedBoost1 : Wander
{
    [HideInInspector] public UnityEvent GiveSpeed;
    bool canInteract = true;

    float originalSpeed;
    float originalSprintSpeed;
    public float boostDuration;
    public float moveSpeedIncrease;


    public override void GiveBoost()
    {
        if (!canInteract || BoostGiven) return;

        if (GiveSpeed.GetPersistentEventCount() == 0)
            GiveSpeed.AddListener(IncreaseSpeed);

        GiveSpeed?.Invoke();
        canInteract = false;
    }

    private void IncreaseSpeed()
    {
        StartCoroutine(SpeedIncrease(boostDuration, moveSpeedIncrease));
    }

    private IEnumerator SpeedIncrease(float boostDur, float speedInc)
    {
        if (BoostGiven) yield break;
        BoostGiven = true;
        try
        {
            originalSprintSpeed = _playerMovement.newSpeed;
            originalSpeed = _playerMovement.originalSpeed;
            _playerMovement.originalSpeed *= speedInc;
            _playerMovement.newSpeed *= speedInc;

            //notifications
            NotificationsAppear.Instance.ShowNoteRabbit();



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

            yield return new WaitForSeconds(boostDur);

            Material[] revertMats = _playerRenderer.materials;
            if (revertMats.Length > 1)
            {
                Material[] trimmed = new Material[revertMats.Length - 1];
                for (int i = 0; i < trimmed.Length; i++)
                    trimmed[i] = revertMats[i];
                _playerRenderer.materials = trimmed;
            }

            _playerMovement.newSpeed = originalSprintSpeed;
            _playerMovement.originalSpeed = originalSpeed;
        }
        finally
        {
            BoostGiven = false;
        }

    }
}
