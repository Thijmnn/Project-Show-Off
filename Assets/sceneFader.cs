using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneFader : MonoBehaviour
{
    public Animator fadeAnimator;
    public bool playAnimation;

    int sceneIndex;

    private void Start()
    {
        int currentscene = SceneManager.GetActiveScene().buildIndex;
        if (currentscene == SceneManager.sceneCount) { sceneIndex = 1; }
        else { sceneIndex = SceneManager.GetActiveScene().buildIndex + 1; }
    }
    public void TriggerFadeOut()
    {
        fadeAnimator.SetTrigger("FadeOutTrigger");
    }

    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(sceneIndex);
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
