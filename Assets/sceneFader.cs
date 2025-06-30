using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneFader : MonoBehaviour
{
    public Animator fadeAnimator;
    public bool playAnimation;

    int sceneIndex;

    public void TriggerFadeOut()
    {
        fadeAnimator.SetTrigger("FadeOutTrigger");
    }

    public void OnFadeOutComplete()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        int nextScene;

        if (currentScene == totalScenes - 1)
        {
            nextScene = 0; // back to the first scene
        }
        else
        {
            nextScene = currentScene + 1;
        }

        SceneManager.LoadScene(nextScene);
    }

    private void Update()
    {
        if (playAnimation)
        {
            TriggerFadeOut();
            playAnimation = false;
        }

        
    }
}
