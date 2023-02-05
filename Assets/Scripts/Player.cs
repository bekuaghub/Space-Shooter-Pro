using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    public float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        if (transform.position.y <= -2.8f) transform.position = new Vector3(transform.position.x, -2.8f, 0);
        else if (transform.position.y >= 8.4f) transform.position = new Vector3(transform.position.x, 8.4f, 0);
        if (transform.position.x <= -10.4f) transform.position = new Vector3(-10.4f, transform.position.y, 0);
        else if (transform.position.x >= 10.4f) transform.position = new Vector3(10.4f, transform.position.y, 0);
    }
}