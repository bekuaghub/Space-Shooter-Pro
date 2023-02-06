using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private int _damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-9.2f, 9.2f), 12f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -2.0f)
        {
            Vector3 startPos = new Vector3(Random.Range(-9.2f, 9.2f), 12f, 0);  //Spawn player at a random x position
            transform.position = startPos;
        }
    }

    private void OnTriggerEnter(Collider other)
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
