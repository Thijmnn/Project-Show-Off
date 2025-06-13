using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationsAppear : MonoBehaviour
{

    public GameObject noteRabbit, noteHedgehog, noteFrog;
    public GameObject noteFrame;

    [SerializeField]
    int rabbitLength = 20;
    [SerializeField]
    int hedgehogLength = 20;
    [SerializeField]
    int frogLength = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showNoteRabbit()
    {
        StartCoroutine(RabbitPower());
    }
    public void showNoteHedgehog()
    {
        StartCoroutine (HedgehogPower());
    }
    public void showNoteFrog()
    {
        StartCoroutine(FrogPowers());
    }

    IEnumerator RabbitPower()
    {
        var noteRabbitInstance = Instantiate(noteRabbit,noteFrame.transform);
        yield return new WaitForSeconds(rabbitLength);
        Destroy(noteRabbitInstance);
    }
    IEnumerator HedgehogPower()
    {
        var noteHedgehogInstance = Instantiate(noteHedgehog,noteFrame.transform);
        yield return new WaitForSeconds(hedgehogLength);
        Destroy(noteHedgehogInstance);
    }
    IEnumerator FrogPowers()
    {
        var noteFrogInstance = Instantiate(noteFrog,noteFrame.transform);
        yield return new WaitForSeconds(frogLength);
        Destroy(noteFrogInstance);
    }

}
