using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleBehaviour : MonoBehaviour
{
    [HideInInspector] public bool DestroySelf;
    private BubbleBehaviour _bubbleBehaviour;

    Rigidbody _rb;
    Collider _col;


    [HideInInspector] public bool canDestroy;

    public GameObject animalSpawn;

    BlowingScript[] blowingScripts;

    public StudioEventEmitter hoverEmitter;
    public StudioEventEmitter mergeEmitter;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<BubbleBehaviour>())
        {
            _bubbleBehaviour = other.GetComponent<BubbleBehaviour>();
            CheckOverlap(_bubbleBehaviour, other.gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            _col.isTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            _col.isTrigger = true;
        }
    }

    private void RollRandom()
    {
        
        float random = Random.Range(0f, 1f);
        
        if (random <= 0.5f)
        {
            
            DestroySelf = true;
        }
        else
        {

            DestroySelf = false;
        }
    }



    private void CheckOverlap(BubbleBehaviour _bubbleBehaviour, GameObject other)
    {
        Transform[] children = _other.GetComponentsInChildren<Transform>();
        if (children != null)
        {
            foreach (Transform child in children)
            {
                if (child != transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        _other.transform.localScale = _other.transform.localScale + (transform.localScale / 3);
        _rb.mass = transform.localScale.x /3;
        BubbleSpawner.Instance.bubblesLeft--;
        foreach(BlowingScript blower in blowingScripts)
        {
            blower.RemoveBubble(this.gameObject);
        }

        StudioEventEmitter[] emitter = _other.GetComponents<StudioEventEmitter>();
        
        emitter[1].Play();
        Destroy(gameObject);
    }

    private void DestroyOtherBubble(GameObject _other)
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        if (children != null)
        {
            foreach(Transform child in children)
            {
                if(child != transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        transform.localScale = transform.localScale + (_other.transform.localScale / 2);
        _rb.mass = transform.localScale.x /2;
        BubbleSpawner.Instance.bubblesLeft--;
        foreach (BlowingScript blower in blowingScripts)
        {
            if (other.transform.localScale.x > transform.localScale.x)
            {
                if (transform.localScale.x >= popThreshhold)
                {
                    Destroy(gameObject);
                    Destroy(other);
                    print("bubblePopped!");
                }
                else
                {
                    other.transform.localScale = other.transform.localScale + (transform.localScale / 2);
                    _rb.mass = transform.localScale.x;
                    rig.AddForce(_rb.velocity);
                    Destroy(gameObject);
                }
                
            }
            else if (transform.localScale.x > other.transform.localScale.x)
            {
                if (transform.localScale.x >= popThreshhold)
                {
                    Destroy(gameObject);
                    Destroy(other);
                    print("bubblePopped!");
                }
                else
                { 
                    transform.localScale = transform.localScale + (other.transform.localScale / 2);
                    _rb.mass = transform.localScale.x;
                    Destroy(other);
                }
                
            }
            else if (other.transform.localScale.x == transform.localScale.x)
            {
                if (DestroySelf == _bubbleBehaviour.DestroySelf)
                {
                    RollRandom();
                }
                else if (DestroySelf && !_bubbleBehaviour.DestroySelf)
                {
                    
                    if (transform.localScale.x >= popThreshhold)
                    {
                        Destroy(gameObject);
                        Destroy(other);
                        print("bubblePopped!");
                    }
                    else
                    {
                        rig.AddForce(_rb.velocity);
                        other.transform.localScale = other.transform.localScale + (transform.localScale / 2);
                        _rb.mass = transform.localScale.x;
                        Destroy(gameObject);
                    }
                }
                else if (_bubbleBehaviour.DestroySelf && !DestroySelf)
                {
                    if (transform.localScale.x >= popThreshhold)
                    {
                        Destroy(other);
                        Destroy(gameObject);
                        print("bubblePopped!");
                    }
                    else
                    {
                        Destroy(other);
                        transform.localScale = transform.localScale + (other.transform.localScale / 2);
                        _rb.mass = transform.localScale.x;
                    }
                }
            }
        }
    }

}
