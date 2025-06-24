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
        if (fan1.isValid())
        {
            fan1.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
            fan1.start();
        }

        if (fan2.isValid())
        {
            fan2.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
            fan2.start();
        }

        isFanPlaying = true;
    }

    private void StopFanSounds()
    {
        if (fan1.isValid())
            fan1.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        if (fan2.isValid())
            fan2.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        isFanPlaying = false;
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
