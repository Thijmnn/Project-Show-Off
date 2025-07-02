using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targets;
    private Transform[] transforms;
    public Vector3 offset;
    [Tooltip("Time taken to smooth out")]
    public float smoothTime = 0.5f;
    private Vector3 velocity;

    public float minZoom = 60f;
    public float maxZoom = 40;
    public float zoomLimiter = 50f;

    private Camera cam;
    public static MultipleTargetCamera Instance { get; private set; }

    public Vector3 minPosition;  // Minimum XYZ values
    public Vector3 maxPosition;  // Maximum XYZ values

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
    private void Start()
    { 
        cam = GetComponent<Camera>();
    }
    public void Update()
    {
        if(targets.Count <= 1)
        {
            transforms = FindObjectsOfType<Transform>();
            foreach(Transform t in transforms)
            {
                if (t.TryGetComponent<BlowingScript>(out BlowingScript _player))
                {
                    if (!targets.Contains(_player.gameObject.transform))
                    {
                        targets.Add(_player.gameObject.transform);
                    }
                }
            }  
        }
        else
        {
            return;
        }
    }
    private void LateUpdate()
    {
        if (targets.Count == 0)
        {
            return;
        }
        if(targets.Count == 1) { Move(); }
        else
        {
            Move();
            Zoom();
        }
        // Clamp the camera's position
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.position.x, minPosition.x, maxPosition.x);
        clampedPosition.y = Mathf.Clamp(transform.position.y, minPosition.y, maxPosition.y);
        clampedPosition.z = Mathf.Clamp(transform.position.z, minPosition.z, maxPosition.z);

        transform.position = clampedPosition;
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newpostion = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newpostion, ref velocity, smoothTime);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() - zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        Vector3 distance = targets[0].position - targets[1].position;
        float length = distance.magnitude;
        return length /2;
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
