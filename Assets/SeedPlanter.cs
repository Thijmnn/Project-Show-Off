using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class SeedPlanter : MonoBehaviour
{
    PlayerInput _playerinput;


    public GameObject stageOnePlant;
    public GameObject stageTwoPlant;
    public GameObject FinalPlant;

    public GameObject PlantStageOnePopUp;
    public GameObject WaterStageOnePopUp;
    public GameObject DoneStageOnePopUp;
    public GameObject TimerStageOnePopUp;

    bool CountDown;
    int timer = 120;
    bool plantGrown;
    bool once = true;
    int progress;

    bool clicked;
    int cooldown;
    private void Start()
    {
        _playerinput = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        if(timer >= 0 && CountDown)
        {
            timer--;
        }
        else if (once && CountDown)
        {
            DoneStageOnePopUp.SetActive(true);
            TimerStageOnePopUp.SetActive(false);
            CountDown = false;
            plantGrown = true;
            once = false;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (_playerinput.actions["Interact"].inProgress)
        {
            cooldown = 50;
            clicked = true;
            if (clicked)
            {
                cooldown--;
                if(cooldown <= 0)
                {
                    clicked = false;
                }
            }

            if (!clicked)
            {
                switch (progress)
                {
                    case 0:
                        PlantBud();
                        break;
                    case 1:
                        WaterBud();
                        break;
                    case 2:
                        if (plantGrown)
                        {
                            GrowPlant();
                        }
                        break;
                    case 3:
                        WaterPlant();
                        break;
                    case 4:
                        if (plantGrown)
                        {
                            GrowFinalPlant();
                        }
                        break;
                    case 5:
                        HarvestPlant();
                        break;
                }
            }
        }
    }


    private void PlantBud()
    {
        stageOnePlant.SetActive(true);
        WaterStageOnePopUp.SetActive(true);
        progress++;
    }

    private void WaterBud()
    {
        WaterStageOnePopUp.SetActive(false);
        TimerStageOnePopUp.SetActive(true);
        CountDown = true;
        progress++;
    }

    private void GrowPlant()
    {
        DoneStageOnePopUp.SetActive(false);
        stageOnePlant.SetActive(false);
        WaterStageOnePopUp.SetActive(true);
        stageTwoPlant.SetActive(true);
        progress++;
        plantGrown = false;
    }

    private void WaterPlant()
    {
        WaterStageOnePopUp.SetActive(false);
        TimerStageOnePopUp.SetActive(true);
        CountDown = true;
        timer = 120;
        once = true;
        progress++;
    }

    private void GrowFinalPlant()
    {
        DoneStageOnePopUp.SetActive(false);
        stageTwoPlant.SetActive(false);
        FinalPlant.SetActive(true);
        progress++;
    }

    private void HarvestPlant()
    {
        FinalPlant.SetActive(false);
        print("you got a seed");
    }
}