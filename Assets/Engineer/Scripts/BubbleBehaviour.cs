using FMODUnity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class BubbleBehaviour : MonoBehaviour
{
    [HideInInspector] public bool DestroySelf;
    private BubbleBehaviour _bubbleBehaviour;

    Rigidbody _rb;


    [HideInInspector] public bool canDestroy;

    public GameObject animalSpawn;

    BlowingScript[] blowingScripts;

    public StudioEventEmitter hoverEmitter;
    public StudioEventEmitter mergeEmitter;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.WakeUp();
    }

    private void FixedUpdate()
    {
        blowingScripts = FindObjectsOfType<BlowingScript>();
    }

    public void Update()
    {
        _rb.drag = _rb.mass;
        _rb.WakeUp();

        if(_rb.velocity.magnitude > 0.1f)
        {
            hoverEmitter.enabled = true;
        }
        else if(_rb.velocity.magnitude < 0.1f)
        {
            hoverEmitter.enabled = false;
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        BubbleBehaviour otherBubble = collision.gameObject.GetComponent<BubbleBehaviour>();
        if (otherBubble == null) return;

        // pick *one* bubble to decide the merge logic
        if (this.GetInstanceID() > otherBubble.GetInstanceID())
        {
            // only the higher-ID bubble decides
            HandleMerge(otherBubble);
        }
    }

    private void HandleMerge(BubbleBehaviour other)
    {
        float mySize = transform.localScale.x;
        float otherSize = other.transform.localScale.x;

        if (otherSize > mySize)
        {
            // other is bigger, I get destroyed
            other.canDestroy = true;
            canDestroy = false;
            SpawnAnimal();
            other.DestroyOtherBubble(gameObject);
        }
        else if (mySize > otherSize)
        {
            // I am bigger, destroy other
            canDestroy = true;
            other.canDestroy = false;
            other.SpawnAnimal();
            DestroyOtherBubble(other.gameObject);
        }
        else
        {
            // equal size, random decision
            int roll = Random.Range(0, 2); // 0 or 1
            if (roll == 0)
            {
                // I survive
                canDestroy = true;
                other.canDestroy = false;
                other.SpawnAnimal();
                SpawnAnimal();
                DestroyOtherBubble(other.gameObject);
            }
            else
            {
                // other survives
                canDestroy = false;
                other.canDestroy = true;
                other.SpawnAnimal();
                SpawnAnimal();
                other.DestroyOtherBubble(gameObject);
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
            blower.RemoveBubble(_other);
        }
        mergeEmitter.Play();

        foreach (var mat in GetComponent<Renderer>().materials)
        {
            if (mat.name.Contains("Nightmare"))
            {
                print(mat.name + gameObject.name + " " + mat.mainTextureScale);
                mat.mainTextureScale = new Vector2(1,1);
                print(mat.name + gameObject.name + " " + mat.mainTextureScale);
            }
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

    
