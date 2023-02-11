using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //[SerializeField] private AudioSource _explosionSound;
    // Start is called before the first frame update
    private AudioSource _explosionSoundSource;
    //private AudioSource _explosionSound;
    void Start()
    {
        //_explosionSound = GameObject.FindWithTag("ExplosionSound").GetComponent<AudioSource>();
        //_explosionSound.Play();
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
