using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _verticalDownLimit = -2.0f;
    [SerializeField] private float _speed = 4.0f;
    private int _damage = 50;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < _verticalDownLimit)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //Collision with player or laser
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)     
                player.DamageTaken(_damage);

            Destroy(this.gameObject);
        }

        else if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
