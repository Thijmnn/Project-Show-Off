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

    public Collider[] playerColliders;
    public Collider[] collObjs;
    public List <Collider> ignoreColliders;

    LayerMask layer = 6;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
    }
    public void Update()
    {
        _rb.mass = transform.localScale.x;

        if(transform.localScale.x >= 20)
        {
            Destroy(gameObject);
            print("bubblePopped!");
        }

        IgnoreColisions();
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
        Vector3 distance = transform.position - other.transform.position;
        float length = distance.magnitude;
        float halfLength = (transform.localScale.x + other.transform.localScale.x) / 2;
        Rigidbody rig = other.GetComponent<Rigidbody>();

        if (length <= (halfLength / 2))
        {
            if (other.transform.localScale.x > transform.localScale.x)
            {
                Destroy(gameObject);
                other.transform.localScale = other.transform.localScale + (transform.localScale / 2);
                
                rig.AddForce(_rb.velocity);
            }
            else if (transform.localScale.x > other.transform.localScale.x)
            {
                Destroy(other);
                transform.localScale = transform.localScale + (other.transform.localScale / 2);
            }
            else if (other.transform.localScale.x == transform.localScale.x)
            {
                if (DestroySelf == _bubbleBehaviour.DestroySelf)
                {
                    RollRandom();
                }
                else if (DestroySelf && !_bubbleBehaviour.DestroySelf)
                {
                    Destroy(gameObject);
                    other.transform.localScale = other.transform.localScale + (transform.localScale / 2);
                    rig.AddForce(_rb.velocity);
                }
                else if (_bubbleBehaviour.DestroySelf && !DestroySelf)
                {
                    Destroy(other);
                    transform.localScale = transform.localScale + (other.transform.localScale / 2);
                }
            }
        }
    }




    void Awake()
    {

        playerColliders = GetComponents<Collider>();
        
        collObjs = FindObjectsOfType<Collider>();

        foreach (Collider go in collObjs)
        {
            if (go.gameObject.layer == layer)
            {
                ignoreColliders.Add(go);
            }
        }
    }

    void IgnoreColisions()
    {

        
        foreach (Collider playerColl in playerColliders)
        {
            
            if (playerColl != null && playerColl.isTrigger == false)
            {
                
                foreach (Collider ignoreColl in ignoreColliders)
                {
                    
                    if (ignoreColl != null && ignoreColl.isTrigger == false)
                    {
                        
                        Physics.IgnoreCollision(playerColl, ignoreColl, false);
                    }
                }
            }
        }
    }
}
