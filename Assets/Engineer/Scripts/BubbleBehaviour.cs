using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleBehaviour : MonoBehaviour
{
    [HideInInspector] public bool DestroySelf;
    private BubbleBehaviour _bubbleBehaviour;

    Rigidbody _rb;
    Collider _col;

    public int overlap;

    private int overlapThreshold;

    public bool canDestroy;

    public GameObject animalSpawn;

    private void Start()
    {
        overlapThreshold = 100 / overlap;
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
        Vector3 distance = transform.position - other.transform.position;
        float length = distance.magnitude;
        float halfLength = (transform.localScale.x + other.transform.localScale.x) / 2;

        if (length <= (halfLength / overlapThreshold))
        {
            if (other.transform.localScale.x > transform.localScale.x)
            {
                _bubbleBehaviour.canDestroy = true;
                canDestroy = false;
                if(canDestroy) { DestroyBubble(other); }
                if(animalSpawn != null) { SpawnAnimal(); }
            }
            else if (transform.localScale.x > other.transform.localScale.x)
            {
                _bubbleBehaviour.canDestroy = false;
                canDestroy = true;
                if (canDestroy) { DestroyOtherBubble(other); }
                if (animalSpawn != null) { SpawnAnimal(); }
            }
            else if (other.transform.localScale.x == transform.localScale.x)
            {
                if (DestroySelf == _bubbleBehaviour.DestroySelf)
                {
                    RollRandom();
                }
                else if (DestroySelf && !_bubbleBehaviour.DestroySelf)
                {
                    _bubbleBehaviour.canDestroy = true;
                    canDestroy = false;
                    if (canDestroy) { DestroyBubble(other); }
                    if (animalSpawn != null) { SpawnAnimal(); }
                }
                else if (_bubbleBehaviour.DestroySelf && !DestroySelf)
                {
                    _bubbleBehaviour.canDestroy = false;
                    canDestroy = true;
                    if (canDestroy) { DestroyOtherBubble(other); }
                    if (animalSpawn != null) { SpawnAnimal(); }
                }
            }
        }
    }

    private void DestroyBubble(GameObject _other)
    {
        _other.transform.localScale = _other.transform.localScale + (transform.localScale / 2);
        _rb.mass = transform.localScale.x;
        BubbleSpawner.Instance.bubblesLeft--;
        Destroy(gameObject);
    }

    private void DestroyOtherBubble(GameObject _other)
    {
        transform.localScale = transform.localScale + (_other.transform.localScale / 2);
        _rb.mass = transform.localScale.x;
        BubbleSpawner.Instance.bubblesLeft--;
        Destroy(_other);
    }

    private void SpawnAnimal()
    {
        GameObject _animalSpawn = animalSpawn;

        Instantiate(_animalSpawn.transform, transform.position ,Quaternion.identity);
        animalSpawn = null;
    }
}

    
