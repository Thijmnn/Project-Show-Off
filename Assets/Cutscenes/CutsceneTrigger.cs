using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public static CutsceneTrigger Instance { get; private set; }
    public PlayableDirector director;  // Assign in Inspector
    public bool bubblepopCutscene = false;

    private void Awake()
    {
        Instance = this;
    }
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
