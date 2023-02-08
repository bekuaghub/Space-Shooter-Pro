using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _verticalUpLimit = 12f;
    [SerializeField] private float _speed = 8f;

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        gameObject.transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (this.transform.position.y > _verticalUpLimit)
        {
            if(this.transform.parent != null)
                Destroy(this.transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }
}
