using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlowerAnimation : MonoBehaviour
{
    bool openFlower;
    public Animator animator;

    public List<GameObject> flowers;

    float normalizedTime;
    private void Start()
    {
        flowers[0].SetActive(true);
        flowers[1].SetActive(false);
        flowers[2].SetActive(false);
    }
    public void UpdateFlower()
    {
        openFlower = true;
    }

    private void Update()
    {
        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        normalizedTime = animStateInfo.normalizedTime;

        if (openFlower)
        {
            flowers[0].SetActive(false);
            flowers[1].SetActive(true);
            openFlower = false;
        }
        else if (normalizedTime > 1.0f && !openFlower)
        {
            flowers[1].SetActive(false);
            flowers[2].SetActive(true);
        }


    }
}
