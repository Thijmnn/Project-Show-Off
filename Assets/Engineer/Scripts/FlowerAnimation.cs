using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlowerAnimation : MonoBehaviour
{
    bool openFlower;   
    Animator animator;

    GameObject[] flowers;

    public void Start()
    {
        flowers = FindObjectsOfType<GameObject>();
        animator = GetComponentInChildren<Animator>();
    }
    public void UpdateFlower()
    {
        openFlower = true;
    }


    private void Update()
    {
        if (openFlower)
        {
            flowers[0].SetActive(false);
            flowers[1].SetActive(true);
            animator.Play("Flower1_Inside_Animation");
        }
        else if(this.animator.GetCurrentAnimatorStateInfo(0).IsName("Flower1_Inside_Animation"))
        {
            flowers[1].SetActive(false);
            flowers[2].SetActive(true);
        }
    }
}
