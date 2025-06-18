using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector director;  // Assign in Inspector
    public bool bubblepopCutscene = false;

    void Update()
    {
        if (bubblepopCutscene)
        {
            if (director.state != PlayState.Playing)
            {
                director.Play();
                MultipleTargetCamera ptgouh = FindObjectOfType<MultipleTargetCamera>();
                ptgouh.enabled = false;
            }

            bubblepopCutscene = false; // Optional: reset boolean so it only plays once
        }
    }
}
