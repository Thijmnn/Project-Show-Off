using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class FMODUISelectionSound : MonoBehaviour, ISelectHandler
{
    public EventReference selectionSound;

    private static bool hasFirstSelectionPlayed = false;

    public void OnSelect(BaseEventData eventData)
    {
        if (!hasFirstSelectionPlayed)
        {
            hasFirstSelectionPlayed = true;
            return; // Skip first hover sound only once per scene
        }

        if (!selectionSound.IsNull)
        {
            RuntimeManager.PlayOneShot(selectionSound);
        }
    }

    private void OnDisable()
    {
        // Optional: reset flag if scene reloads or UI is dynamically unloaded
        hasFirstSelectionPlayed = false;
    }
}
