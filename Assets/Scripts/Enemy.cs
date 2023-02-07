using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _horizontalRightLimit = 8.2f, _horizontalLeftLimit = -8.2f, _verticalUpLimit = 12f, _verticalDownLimit = -2.0f;
    [SerializeField]
    private float _speed = 4.0f;
    private int _damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(_horizontalLeftLimit, _horizontalRightLimit), _verticalUpLimit, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < _verticalDownLimit)
        {
            Destroy(this.gameObject);
            //Vector3 startPos = new Vector3(Random.Range(-9.2f, 9.2f), 12f, 0);  //Spawn player at a random x position
            //transform.position = startPos;
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
