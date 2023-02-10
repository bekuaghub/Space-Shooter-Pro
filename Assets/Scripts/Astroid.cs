using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField] private float _rotSpeed = 0.1f;
    [SerializeField] private GameObject _explosion;
    private Spawn_Manager _spawnManager;
    void Start()
    {
        _spawnManager = GameObject.FindWithTag("SpawnManager").GetComponent<Spawn_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotSpeed);
    }
    private void OnTriggerEnter2D(Collider2D other) //Collision with player or laser
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject);
        }
    }
}
