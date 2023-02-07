using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _horizontalRightLimit = 8.8f, _horizontalLeftLimit = -8.8f, _verticalUpLimit = 9.2f, _verticalDownLimit = 0.0f;
    [SerializeField]
    private int _health = 100;
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0.0f;
    private Spawn_Manager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.FindWithTag("SpawnManager").GetComponent<Spawn_Manager>();

        if(_spawnManager == null)
        {
            Debug.Log("Spawn manager not found!");
        }
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
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _horizontalLeftLimit, _horizontalRightLimit), Mathf.Clamp(transform.position.y, _verticalDownLimit, _verticalUpLimit), 0); //Limit player movement in the given boundaries
    }

    void Fire()
    {
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity); //Spawn lasers at given player position with an off-set
    }

    public void DamageTaken(int damage)
    {
        this._health -= damage;

        Debug.Log("Damage taken by: " + damage + "\nHealth is now: " + this._health);

        if(_health <= 0) //Player is dead i.e OnDeath()
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

}