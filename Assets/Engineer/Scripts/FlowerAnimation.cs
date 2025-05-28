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
        flowers = GetComponentsInChildren<GameObject>();
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
            flowers[0].SetActive(true);
            flowers[1].SetActive(true);
            animator.Play("Flower1_Inside_Animation");
        }
        else if(openFlower)
        {

        }
    }
}
