using Adobe.Substance.Connector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public Wave[] waves;
    [SerializeField] List<Collider> spawnAreas;
    int currentWaveIndex;
    public int bubblesLeft;

    public static BubbleSpawner Instance { get; private set; }

    public float bubbleHeight;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    void Update()
    {
        WaveSpawner();

        if(bubblesLeft == 1)
        {
            BubbleBehaviour lastBubble = FindObjectOfType<BubbleBehaviour>();
            Destroy(lastBubble.gameObject);
            bubblesLeft = 0;
            currentWaveIndex++;
            print("you popped a bubble"); 
        }
    }

    private void WaveSpawner()
    {
        if (bubblesLeft == 0 && currentWaveIndex < waves.Length) 
        {
            bubblesLeft = waves[currentWaveIndex].bubbles.Length;
            SpawnNextWave();
        }
        else if(bubblesLeft == 0 && currentWaveIndex > waves.Length)
        {
            print("no more waves left");
        }
        
    }
    private void SpawnNextWave()
    {
      foreach(GameObject bubble in waves[currentWaveIndex].bubbles)
      {
            BubbleBehaviour _bubble = bubble.GetComponent<BubbleBehaviour>();
            _bubble.overlap = waves[currentWaveIndex].overlap;

            Collider area = spawnAreas[Random.Range(0, spawnAreas.Count)];
            Bounds _bounds = area.bounds;

            float offsetX = Random.Range(-_bounds.extents.x, _bounds.extents.x);
            float offsetZ = Random.Range(-_bounds.extents.z, _bounds.extents.z);

            Instantiate(bubble, new Vector3(offsetX, bubbleHeight, offsetZ), Quaternion.identity); 
      }  
    }

    [System.Serializable]
    public class Wave
    {
        public GameObject[] bubbles;
        [Header("Hover for more information")]

        [Tooltip("percentage overlapped" +
            "100 = 100% overlap 1 = 1% overlapped" +
            "CANT BE 0 !!!!")]
        public int overlap;
    }
}
