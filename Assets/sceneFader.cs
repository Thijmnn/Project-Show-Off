using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneFader : MonoBehaviour
{
    public Animator fadeAnimator;
    public bool playAnimation;
    public void TriggerFadeOut()
    {
        fadeAnimator.SetTrigger("FadeOutTrigger");
    }

    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
