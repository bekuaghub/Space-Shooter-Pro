using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 8f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        gameObject.transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (this.transform.position.y > 10f)
            Destroy(this.gameObject);
    }
}
