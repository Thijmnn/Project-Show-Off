using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBubbles : MonoBehaviour
{
    List<GameObject> bubbles = new List<GameObject>();

    void Update()
    {
        if (bubbles.Count > 0)
        {
            foreach (var bubble in bubbles)
            {
                Rigidbody rb = bubble.GetComponent<Rigidbody>();

                if(rb != null)
                {
                    rb.AddForce(transform.forward);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BubbleBehaviour>())
        {
            bubbles.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (bubbles.Contains(other.gameObject))
        {
            bubbles.Remove(other.gameObject);
        }
    }
}
