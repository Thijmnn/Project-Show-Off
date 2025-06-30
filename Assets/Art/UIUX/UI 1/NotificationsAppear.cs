using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NotificationsAppear : MonoBehaviour
{
    public static NotificationsAppear Instance { get; private set; }
    public GameObject noteRabbit, noteHedgehog, noteFrog;
    public GameObject noteFrame;

   

    [SerializeField]
    int rabbitLength = 5;
    [SerializeField]
    int hedgehogLength = 5;
    [SerializeField]
    int frogLength = 5;

    private void Awake()
    {
        Instance = this;
    }


    public void ShowNoteRabbit()
    {
        StartCoroutine(RabbitPower());
    }
    public void ShowNoteHedgehog()
    {
        StartCoroutine (HedgehogPower());
    }
    public void ShowNoteFrog()
    {
        StartCoroutine(FrogPowers());
    }

    IEnumerator RabbitPower()
    {
        int rabbitCount = rabbitLength;
        var noteRabbitInstance = Instantiate(noteRabbit,noteFrame.transform);
        TMP_Text rabbitTimer = noteRabbitInstance.GetComponentInChildren<TMP_Text>();
        for (var i = 0; i < rabbitLength; ++i)
        {
            rabbitCount -= 1;
            rabbitTimer.text = rabbitCount.ToString();

            yield return new WaitForSeconds(1);
        }

        
        Destroy(noteRabbitInstance);
        Debug.Log("Speed boost done");
    }
    IEnumerator HedgehogPower()
    {
        int hedgeCount = hedgehogLength;
        var noteHedgehogInstance = Instantiate(noteHedgehog,noteFrame.transform);
        TMP_Text hedgeTimer = noteHedgehogInstance.GetComponentInChildren<TMP_Text>();
        for (var i = 0; i < hedgehogLength; ++i)
        {
            hedgeCount -= 1;
            hedgeTimer.text = hedgeCount.ToString();

            yield return new WaitForSeconds(1);
        }


        Destroy(noteHedgehogInstance);
        Debug.Log("fan boost done");
    }
    IEnumerator FrogPowers()
    {
        int frogCount = frogLength;
        var noteFrogInstance = Instantiate(noteFrog,noteFrame.transform);
        TMP_Text frogTimer = noteFrogInstance.GetComponentInChildren<TMP_Text>();

        for (var i = 0;  i < frogLength; ++i)
        {
            frogCount -= 1;
            frogTimer.text = frogCount.ToString();

            yield return new WaitForSeconds(1);
        }
        Destroy(noteFrogInstance);
        Debug.Log("magnet done");
    }

}
