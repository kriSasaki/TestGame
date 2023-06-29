using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DestroyableObject : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _shadow;    

    private AudioSource _audio;

    private void Start()
    {
        _audio= GetComponent<AudioSource>();
    }

    public void BreakObject()
    {
        _audio.Play();
        _particleSystem.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        _shadow.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 2f);
    }
}
