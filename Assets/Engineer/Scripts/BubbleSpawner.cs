using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public Wave[] waves;
    [SerializeField] Collider bounds;
    bool once = true;
    int currentWaveIndex;
    int enemiesLeft;
    private void Start()
    {
        enemiesLeft = waves[currentWaveIndex].bubbles.Length;
    }
    void Update()
    {
        WaveSpawner();
    }

    private void WaveSpawner()
    {
        if (once) { SpawnNextWave(); once = false; }
        
    }
    private void SpawnNextWave()
    {
      foreach(GameObject bubble in waves[currentWaveIndex].bubbles)
      {
            BubbleBehaviour _bubble = bubble.GetComponent<BubbleBehaviour>();
            _bubble.popThreshhold = waves[currentWaveIndex].popThreshold;
            _bubble.overlap = waves[currentWaveIndex].overlap;

            Bounds _bounds = bounds.bounds;
            float offsetX = Random.Range(-_bounds.extents.x, _bounds.extents.x);
            float offsetZ = Random.Range(-_bounds.extents.z, _bounds.extents.z);

            Instantiate(bubble, new Vector3(offsetX, 2.5f, offsetZ), Quaternion.identity); 
      }  
    }

    [System.Serializable]
    public class Wave
    {
        public GameObject[] bubbles;
        [Header("Hover for more information")]
        [Tooltip("Size of the bubble needed to pop" +
            "should always be less then half of the amount of bubbles spawned")]
       
        public int popThreshold;
        [Tooltip("percentage overlapped")]
        public int overlap;
    }
}
