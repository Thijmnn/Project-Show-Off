using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeletParticle : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private void OnEnable()
    {
        _particleSystem = GetComponent<ParticleSystem>();

    }
    private void Start()
    {
        _particleSystem.Play();
    }

    private void Update()
    {
        if(_particleSystem != null)
        {
          if(_particleSystem.isPlaying == false)
          {
                Destroy(_particleSystem.gameObject);
          }
        }
    }
}
