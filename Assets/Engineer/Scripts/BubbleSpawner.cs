using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public Wave[] waves;
    [SerializeField] List<Collider> spawnAreas;
    int currentWaveIndex;
    public int bubblesLeft;

    BlowingScript[] blowingScripts;
    public static BubbleSpawner Instance { get; private set; }

    public float bubbleHeight;

    public GameObject endSceneObjects;
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

    private void FixedUpdate()
    {
        blowingScripts = FindObjectsOfType<BlowingScript>();
    }

    void Update()
    {
        WaveSpawner();

        if(bubblesLeft == 1)
        {
            endSceneObjects.SetActive(true);
            CutsceneTrigger.Instance.bubblepopCutscene = true;
            BubbleBehaviour lastBubble = FindObjectOfType<BubbleBehaviour>();
            foreach (BlowingScript blower in blowingScripts)
            {
                blower.RemoveBubble(lastBubble.gameObject);
            }
            Destroy(lastBubble.gameObject);
            
            bubblesLeft = 0;
            currentWaveIndex++;
            print("you popped a bubble"); 
        }

        if (Input.GetKeyDown(KeyCode.R))  // for example, press R to respawn
        {
            DebugRespawnFirstWave();
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
        foreach (GameObject bubble in waves[currentWaveIndex].bubbles)
        {
            BubbleBehaviour _bubble = bubble.GetComponent<BubbleBehaviour>();

            Collider area = spawnAreas[Random.Range(0, spawnAreas.Count)];
            Bounds _bounds = area.bounds;

            float offsetX = Random.Range(-_bounds.extents.x, _bounds.extents.x);
            float offsetZ = Random.Range(-_bounds.extents.z, _bounds.extents.z);
            Vector3 spawnPos = GetValidSpawnPosition();

            if (spawnPos != Vector3.zero) { Instantiate(bubble, spawnPos, Quaternion.identity); }
            else { print("Failed to spawn bubble"); }
            
        }  
    }

    private Vector3 GetValidSpawnPosition()
    {
        const int maxAttempts = 15;
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Collider area = spawnAreas[Random.Range(0, spawnAreas.Count)];
            Bounds bounds = area.bounds;

            float offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
            float offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);
            Vector3 spawnPos = new Vector3(offsetX, bubbleHeight, offsetZ) + bounds.center;

            if (IsPositionValid(spawnPos))
            {
                return spawnPos;
            }
        }
        return Vector3.zero; 
    }


    private bool IsPositionValid(Vector3 position)
    {
        float checkRadius = 0.5f;
        int wallLayerMask = LayerMask.GetMask("Walls");
        Collider[] colliders = Physics.OverlapSphere(position, checkRadius, wallLayerMask, QueryTriggerInteraction.Collide);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Wall"))
            {
                return false;
            }
        }
        return true;
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

    public void DebugRespawnFirstWave()
    {
        currentWaveIndex = 0;
        bubblesLeft = waves[currentWaveIndex].bubbles.Length;
        SpawnNextWave();
    }
}
