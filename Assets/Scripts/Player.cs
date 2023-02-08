using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private float _horizontalRightLimit = 8.8f, _horizontalLeftLimit = -8.8f, _verticalUpLimit = 9.2f, _verticalDownLimit = 0.0f;
    [SerializeField] private int _health = 100;
    [SerializeField] private float _horizontalSpeed = 4.5f;
    [SerializeField] private float _verticalSpeed = 3f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleLaserPrefab;
    [SerializeField] private float _fireRate = 0.5f;
    private float _nextFire = 0.0f;
    private Spawn_Manager _spawnManager;
    private bool _tripleShotActive = false;
    [SerializeField] private float _tripleshotDuration = 5.0f;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.FindWithTag("SpawnManager").GetComponent<Spawn_Manager>();

        if(_spawnManager == null)
        {
            Debug.Log("Spawn manager not found!");
        }
    }

    void Update()
    {
        Move();
        Fire();
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    Fire();
        //}
    }


    void Move()
    { 
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput * _horizontalSpeed, verticalInput * _verticalSpeed, 0);

        transform.Translate(direction * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _horizontalLeftLimit, _horizontalRightLimit), Mathf.Clamp(transform.position.y, _verticalDownLimit, _verticalUpLimit), 0); //Limit player movement in the given boundaries
    }

    void Fire()
    {
        if (Time.time > _nextFire) //Check if fire cooldown is reset
        {
            _nextFire = Time.time + _fireRate;
            if (_tripleShotActive)
                Instantiate(_tripleLaserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            else
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity); //Spawn lasers at given player position with an off-set
        }
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

    public void TripleShotActivate()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotDuration());
    }
    IEnumerator TripleShotDuration()
    {
        while (true && _tripleShotActive)
        {
            yield return new WaitForSeconds(_tripleshotDuration);
            _tripleShotActive = false;
        }
    }

}