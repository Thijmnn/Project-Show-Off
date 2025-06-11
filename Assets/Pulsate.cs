using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsate : MonoBehaviour
{
    float maxSize = 1;
    float minSize = 0.7f;
    bool max;
    Vector3 Scale;
    float size;


    private void Update()
    {

        gameObject.transform.localScale = Scale;
        Scale = new Vector3(size,size,size);    
        if (!max)
        {
            size += 0.5f * Time.deltaTime;
        }
        else if (max)
        {
            size -= 0.5f * Time.deltaTime;
        }


        if(size < minSize)
        {
            max = false;
        }
        else if (size > maxSize)
        {
            max = true;
        }
    }



}
