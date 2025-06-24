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

    public int overlap;

    private int overlapThreshold;

    [HideInInspector] public bool canDestroy;

    public GameObject animalSpawn;

    BlowingScript[] blowingScripts;
    private void Start()
    {
        overlapThreshold = 100 / overlap;
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        blowingScripts = FindObjectsOfType<BlowingScript>();
    }

    public void Update()
    {
        _rb.drag = _rb.mass;
        _rb.WakeUp();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BubbleBehaviour>())
        {
            _bubbleBehaviour = collision.gameObject.GetComponent<BubbleBehaviour>();
            if (collision.gameObject.transform.localScale.x > transform.localScale.x)
            {
                _bubbleBehaviour.canDestroy = true;
                canDestroy = false;
                SpawnAnimal();
                if (canDestroy) { DestroyBubble(collision.gameObject); }

            }
            else if (transform.localScale.x > collision.gameObject.transform.localScale.x)
            {
                _bubbleBehaviour.canDestroy = false;
                canDestroy = true;
                SpawnAnimal();
                if (canDestroy) { DestroyOtherBubble(collision.gameObject); }

            }
            else if (collision.gameObject.transform.localScale.x == transform.localScale.x)
            {
                if (DestroySelf == _bubbleBehaviour.DestroySelf)
                {

                    RollRandom();

                }
                else if (DestroySelf && !_bubbleBehaviour.DestroySelf)
                {
                    _bubbleBehaviour.canDestroy = true;
                    canDestroy = false;
                    SpawnAnimal();
                    if (canDestroy) { DestroyBubble(collision.gameObject); }

                }
                else if (_bubbleBehaviour.DestroySelf && !DestroySelf)
                {
                    _bubbleBehaviour.canDestroy = false;
                    canDestroy = true;
                    SpawnAnimal();
                    if (canDestroy) { DestroyOtherBubble(collision.gameObject); }

                }
            }
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



    private void DestroyBubble(GameObject _other)
    {
        _other.transform.localScale = _other.transform.localScale + (transform.localScale / 2);
        _rb.mass = transform.localScale.x /2;
        BubbleSpawner.Instance.bubblesLeft--;
        foreach(BlowingScript blower in blowingScripts)
        {
            blower.RemoveBubble(this.gameObject);
        }
        
        
        Destroy(gameObject);
    }

    private void DestroyOtherBubble(GameObject _other)
    {
        transform.localScale = transform.localScale + (_other.transform.localScale / 2);
        _rb.mass = transform.localScale.x /2;
        BubbleSpawner.Instance.bubblesLeft--;
        foreach (BlowingScript blower in blowingScripts)
        {
            blower.RemoveBubble(_other);
        }
        Destroy(_other);
    }

    private void SpawnAnimal()
    {
        if (animalSpawn != null) {
            Instantiate(animalSpawn.transform, transform.position, Quaternion.identity);
            animalSpawn = null;
        }
        
    }
}

    
