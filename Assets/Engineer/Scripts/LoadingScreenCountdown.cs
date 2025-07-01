using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenCountdown : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(NextScene());
    }
    public sceneFader sceneFader;
    public IEnumerator NextScene()
    {
        yield return new WaitForSeconds(5);
        sceneFader.TriggerFadeOut();
        yield return null;
    }
}
