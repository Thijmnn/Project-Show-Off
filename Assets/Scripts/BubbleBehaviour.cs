using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    public bool DestroySelf;
    private BubbleBehaviour _bubbleBehaviour;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<BubbleBehaviour>())
        {
            _bubbleBehaviour = other.GetComponent<BubbleBehaviour>(); 
            Vector3 distance = transform.position - other.transform.position;
            float length = distance.magnitude;
            float halfLength = (transform.localScale.x + other.transform.localScale.x) / 2;

            if(length < (halfLength / 2))
            {
                if(other.transform.localScale.x > transform.localScale.x)
                {
                    Destroy(gameObject);
                    other.transform.localScale = other.transform.localScale + (transform.localScale / 2);
                }
                else if(transform.localScale.x > other.transform.localScale.x)
                {
                    Destroy(other.gameObject);
                    transform.localScale = transform.localScale + (other.transform.localScale / 2);
                }
                else if(other.transform.localScale.x == transform.localScale.x)
                {
                    if (DestroySelf == _bubbleBehaviour.DestroySelf)
                    {
                        RollRandom();
                    }
                    else if (DestroySelf && !_bubbleBehaviour.DestroySelf)
                    {
                        Destroy(gameObject);
                        other.transform.localScale = other.transform.localScale + (transform.localScale / 2);
                    }
                    else if (_bubbleBehaviour.DestroySelf && !DestroySelf)
                    {
                        Destroy(other.gameObject);
                        transform.localScale = transform.localScale + (other.transform.localScale / 2);
                    }
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

    public void Update()
    {
        Rigidbody rig = GetComponent<Rigidbody>();
        rig.mass = transform.localScale.x;
    }
}
