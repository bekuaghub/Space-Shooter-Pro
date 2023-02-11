using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private float _horizontalRightLimit = 8.8f, _horizontalLeftLimit = -8.8f, _verticalUpLimit = 9.2f, _verticalDownLimit = 0.0f;
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _horizontalSpeed = 4.5f;
    [SerializeField] private float _verticalSpeed = 3f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleLaserPrefab;
    [SerializeField] private GameObject _damageTakenLeft;
    [SerializeField] private GameObject _damageTakenRight;
    [SerializeField] private GameObject _explosion;
    [SerializeField] AudioSource _laserSoundSource;
    [SerializeField] private float _fireRate = 0.5f;
    private float _nextFire = 0.0f;
    private Spawn_Manager _spawnManager;
    private UI_Manager _uiManager;
    private bool _tripleShotActive = false;
    private bool _speedBoostActive = false;
    private bool _shieldActive = false;
    [SerializeField] private float _tripleshotDuration = 5.0f;
    [SerializeField] private float _speedboostDuration = 5.0f;
    //[SerializeField] private float _shieldDuration = 5.0f;
    [SerializeField] private GameObject _shield;
    [SerializeField] private int _score = 0;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.FindWithTag("SpawnManager").GetComponent<Spawn_Manager>();
        _uiManager = GameObject.FindWithTag("UI_Manager").GetComponent<UI_Manager>();
        _laserSoundSource = GetComponent<AudioSource>();
        

        if (_spawnManager == null)
        {
            Debug.Log("Spawn manager not found!");
        }

        if (_uiManager == null)
        {
            Debug.Log("UI manager not found!");
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
        Vector3 direction;

        if (_speedBoostActive)
            direction = new Vector3(horizontalInput * (_horizontalSpeed * 2), verticalInput * (_verticalSpeed * 2), 0);
        else
            direction = new Vector3(horizontalInput * _horizontalSpeed, verticalInput * _verticalSpeed, 0);

        transform.Translate(direction * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _horizontalLeftLimit, _horizontalRightLimit), Mathf.Clamp(transform.position.y, _verticalDownLimit, _verticalUpLimit), 0); //Limit player movement in the given boundaries
    }

    void Fire()
    {
        if (Time.time > _nextFire) //Check if fire cooldown is reset
        {
            _laserSoundSource.Play();
            _nextFire = Time.time + _fireRate;
            if (_tripleShotActive)
                Instantiate(_tripleLaserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            else
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity); //Spawn lasers at given player position with an off-set
        }
    }

    public void DamageTaken(int damage)
    {
        if (_shieldActive)
        {
            _shieldActive = false;
            _shield.SetActive(false);
            return;
        }

        this._health -= damage;
        _uiManager.OnHealthUpdate(_health / _maxHealth);

        Debug.Log("Damage taken by: " + damage + "\nHealth is now: " + this._health);

        if(_health <= 0) //Player is dead i.e OnDeath()
        {
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
            _spawnManager.OnPlayerDeath();
            _uiManager.GameOverActivate();
            Destroy(this.gameObject);
        }
        else if(_health <= 40)
        {
            _damageTakenLeft.SetActive(true);
        }
        else if (_health <= 60)
        {
            _damageTakenRight.SetActive(true);
        }
    }

    public void TripleShotActivate()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotDurationRoutine());
    }
    IEnumerator TripleShotDurationRoutine()
    {
        yield return new WaitForSeconds(_tripleshotDuration);
        _tripleShotActive = false;
    }
    public void SpeedBoostActivate()
    {
        _speedBoostActive = true;
        StartCoroutine(SpeedBoostDurationRoutine());
    }
    IEnumerator SpeedBoostDurationRoutine()
    {
        yield return new WaitForSeconds(_speedboostDuration);
        _speedBoostActive = false;
    }

    public void ShieldActivate()
    {
        _shieldActive = true;
        _shield.SetActive(true);
        //StartCoroutine(ShieldDurationRoutine());
    }

    public void AddToScore(int value)
    {
        _score += value;
        _uiManager.UpdateScore(_score);
    }


    //IEnumerator ShieldDurationRoutine()
    //{
    //    yield return new WaitForSeconds(_shieldDuration);
    //    _shieldActive = false;
    //}

}