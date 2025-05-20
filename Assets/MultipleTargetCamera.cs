using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targets;
    private Transform[] transforms;
    public Vector3 offset;

    public static MultipleTargetCamera Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    public void Update()
    {
        if(targets.Count <= 2)
        {
            transforms = FindObjectsOfType<Transform>();
            foreach(Transform t in transforms)
            {
                if (t.TryGetComponent<PlayerMovement>(out PlayerMovement _player))
                {
                    targets.Add(_player.gameObject.transform);
                }
            }
            
            
        }
    }
    private void LateUpdate()
    {
        if (targets.Count == 0)
        {
            return;
        }
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newpostion = centerPoint + offset;

        transform.position = newpostion;    
    }

    Vector3 GetCenterPoint()
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
