using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    public static GameLoopManager instance;


    public static Action beginCutscenePlay;
    public static Action MapIntroductionMap1Play;
    public static Action MapIntroductionMap2Play;
    public static Action MapIntroductionMap3Play;
    public static Action MapEndMap1Play;
    public static Action MapEndnMap2Play;
    public static Action MapEndMap3Play;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }




}
