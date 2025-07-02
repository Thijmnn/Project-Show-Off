using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagnetBoost : Wander
{

    [HideInInspector] public UnityEvent GiveMagnet;
    bool canInteract = true;

    public float boostDuration;
    public float pullVelocity;

    public BubbleBehaviour[] bubbles;

    public float magnetRange;

    public bool magnetic;



    public override void GiveBoost()
    {
        if (!canInteract || BoostGiven) return;

        if (GiveMagnet.GetPersistentEventCount() == 0)
            GiveMagnet.AddListener(MagnetGive);

        GiveMagnet?.Invoke();
        canInteract = false;
    }

    private void MagnetGive()
    {
        StartCoroutine(MagnetActive(boostDuration));
    }

    private IEnumerator MagnetActive(float boostDur)
    {
        if (BoostGiven) yield break;

        BoostGiven = true;

        try
        {
            magnetic = true;
            //notifications
            NotificationsAppear.Instance.ShowNoteFrog();



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

            magnetic = false;

            Material[] revertMats = _playerRenderer.materials;
            if (revertMats.Length > 1)
            {
                Material[] trimmed = new Material[revertMats.Length - 1];
                for (int i = 0; i < trimmed.Length; i++)
                    trimmed[i] = revertMats[i];
                _playerRenderer.materials = trimmed;
            }

        }
        finally
        {
            BoostGiven = false;
        }

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
