using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _health = 100;
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0.0f;

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

        if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire) //Check if fire cooldown is reset
        {
            _nextFire = Time.time + _fireRate;
            Fire();
        }
    }

    void Move()
    { 
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8.8f, 8.8f), Mathf.Clamp(transform.position.y, 0, 9.2f), 0); //Limit player movement in the given boundaries
    }

    void Fire()
    {
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity); //Spawn lasers at given player position with an off-set
    }

    public void DamageTaken(int damage)
    {
        this._health -= damage;

        Debug.Log("Damage taken by: " + damage + "\nHealth is now: " + this._health);

        if(_health <= 0)
            Destroy(this.gameObject);
    }

}