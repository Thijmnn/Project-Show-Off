using System.Collections.Generic;
using UnityEngine;

public class PushBubbles : MonoBehaviour
{
    List<GameObject> bubbles = new List<GameObject>();

    void Update()
    {
        // Remove any destroyed (null) GameObjects from the list
        bubbles.RemoveAll(bubble => bubble == null);

        foreach (var bubble in bubbles)
        {
            Rigidbody rb = bubble.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.forward);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BubbleBehaviour>())
        {
            if (!bubbles.Contains(other.gameObject)) // Prevent duplicates
                bubbles.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bubbles.Remove(other.gameObject);
    }
}