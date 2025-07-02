using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference _footsteps;
    [SerializeField] private EventReference fan1Ref;
    [SerializeField] private EventReference fan2Ref;
    [SerializeField] private Animator animator;

    private FMOD.Studio.EventInstance footsteps;
    private FMOD.Studio.EventInstance fan1;
    private FMOD.Studio.EventInstance fan2;

    private Coroutine fadeOutFan1;
    private Coroutine fadeOutFan2;

    private bool isFanPlaying = false;


    private void Awake()
    {
        if (!_footsteps.IsNull)
        {
            footsteps = FMODUnity.RuntimeManager.CreateInstance(_footsteps);
        }

    }
    private void Start()
    {
        if (!fan1Ref.IsNull)
            fan1 = RuntimeManager.CreateInstance(fan1Ref);

        if (!fan2Ref.IsNull)
            fan2 = RuntimeManager.CreateInstance(fan2Ref);
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Replace "Blow" and "BlowWalk" with your actual animation state names
        bool shouldPlayFan = stateInfo.IsName("Armature|Blow") || stateInfo.IsName("Armature|Blow&Walk");

        if (shouldPlayFan && !isFanPlaying)
        {
            PlayFanSounds();
        }
        else if (!shouldPlayFan && isFanPlaying)
        {
            StopFanSounds();
        }
    }

    private void PlayFanSounds()
    {
        // Fan 1
        if (fadeOutFan1 != null)
        {
            StopCoroutine(fadeOutFan1);
            fadeOutFan1 = null;

            fan1.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            fan1.release();
            fan1 = RuntimeManager.CreateInstance(fan1Ref);
        }

        if (fan1.isValid())
        {
            fan1.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
            fan1.start();
        }

        // Fan 2
        if (fadeOutFan2 != null)
        {
            StopCoroutine(fadeOutFan2);
            fadeOutFan2 = null;

            fan2.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            fan2.release();
            fan2 = RuntimeManager.CreateInstance(fan2Ref);
        }

        if (fan2.isValid())
        {
            fan2.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
            fan2.start();
        }

        isFanPlaying = true;
    }

    private class FMODInstanceWrapper
    {
        public FMOD.Studio.EventInstance instance;
        public EventReference eventRef;

        public FMODInstanceWrapper(FMOD.Studio.EventInstance inst, EventReference evRef)
        {
            instance = inst;
            eventRef = evRef;
        }
    }

    private void StopFanSounds()
    {
        if (fan1.isValid())
            fadeOutFan1 = StartCoroutine(FadeOutAndStop(fan1, fan1Ref, newInstance => fan1 = newInstance));

        if (fan2.isValid())
            fadeOutFan2 = StartCoroutine(FadeOutAndStop(fan2, fan2Ref, newInstance => fan2 = newInstance));

        isFanPlaying = false;
    

}

    private IEnumerator FadeOutAndStop(FMOD.Studio.EventInstance instance, EventReference eventRef, System.Action<FMOD.Studio.EventInstance> onRestart)
    {
        float currentVolume;
        instance.getVolume(out currentVolume);
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newVolume = Mathf.Lerp(currentVolume, 0f, elapsed / duration);
            instance.setVolume(newVolume);
            yield return null;
        }

        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instance.release();

        var newInstance = RuntimeManager.CreateInstance(eventRef);
        onRestart(newInstance);
    }

    public void PlayFootsteps()
    {
        if (footsteps.isValid())
        {
            footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

            GroundSwitch();
            footsteps.start();

        }

    
    }
    private void GroundSwitch()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, -Vector3.up);
        Material surfaceMaterial;

        if (Physics.Raycast(ray, out hit, 1.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            Renderer surfaceRenderer = hit.collider.GetComponentInChildren<Renderer>();
            if (surfaceRenderer)
            {
               
                Debug.Log(surfaceRenderer.material.name);
                if (surfaceRenderer.material.name.Contains("grass"))
                {
                    
                    footsteps.setParameterByName("Footsteps", 1);
                }
                else
                {
                    footsteps.setParameterByName("Footsteps", 0);
                }
            }
        }
    }

}
