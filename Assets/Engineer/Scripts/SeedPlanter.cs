using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
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
    int timer = 1000;
    bool plantGrown;
    bool once = true;
    int progress = 0;


    bool insideCollider;
    private void Start()
    {
        _playerinput = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {

        if(CountDown) { CountdownPlant(); }

        if(insideCollider) { CheckInput(); }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        PlantStageOnePopUp.SetActive(true);
        insideCollider = true;
    }

    private void OnTriggerExit(Collider other)
    {
        insideCollider = false;
    }

    private void PlantBud()
    {
        PlantStageOnePopUp.SetActive(false);
        stageOnePlant.SetActive(true);
        WaterStageOnePopUp.SetActive(true);
        progress = 1;
    }

    private void WaterBud()
    {
        WaterStageOnePopUp.SetActive(false);
        TimerStageOnePopUp.SetActive(true);
        CountDown = true;
        timer = 1000;
        once = true;
        progress = 2;
    }

    private void GrowPlant()
    {
        DoneStageOnePopUp.SetActive(false);
        stageOnePlant.SetActive(false);
        WaterStageOnePopUp.SetActive(true);
        stageTwoPlant.SetActive(true);
        progress = 3;
        plantGrown = false;
    }

    private void WaterPlant()
    {
        WaterStageOnePopUp.SetActive(false);
        TimerStageOnePopUp.SetActive(true);
        CountDown = true;
        timer = 1000;
        once = true;
        progress = 4;
    }

    private void GrowFinalPlant()
    {
        DoneStageOnePopUp.SetActive(true);
        stageTwoPlant.SetActive(false);
        FinalPlant.SetActive(true);
        progress = 5;
    }

    private void HarvestPlant()
    {
        FinalPlant.SetActive(false);
        DoneStageOnePopUp.SetActive(false);
        print("you got a seed");
        progress = 0;
    }

    private void CountdownPlant()
    {
        if (timer >= 0)
        {
            timer--;
        }
        else if (once)
        {
            DoneStageOnePopUp.SetActive(true);
            TimerStageOnePopUp.SetActive(false);
            CountDown = false;
            plantGrown = true;
            once = false;
        }
    }


    private void CheckInput()
    {
        if (_playerinput.actions["Interact"].triggered)
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