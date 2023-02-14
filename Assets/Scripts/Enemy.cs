using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _verticalDownLimit = -2.0f;
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private int _damage = 10;
    private bool _isAlive = true;
    public Transform target;
    private Animator _enemyAnimation;
    private Player _player;
    private AudioSource _explosionSound;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _explosionSound = GetComponent<AudioSource>();
        _enemyAnimation = GetComponent<Animator>();
        if(_player != null)
            target = _player.transform;
    }
    
    void Update()
    {
        //transform.Rotate(Quaternion.identity);
        //transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (_player != null && _isAlive)
        {
            MoveTowardsTarget();
            RotateTowardsTarget();
        }

        if (transform.position.y < _verticalDownLimit)
        {
            Destroy(this.gameObject);
        }
    }
    private void MoveTowardsTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
    }

    private void RotateTowardsTarget()
    {
        var offset = 90f;
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset)) ;
    }


private void OnTriggerEnter2D(Collider2D other) //Collision with player or laser
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            _player.AddToScore(10);
            if (player != null)
                player.DamageTaken(_damage);
            _enemyAnimation.SetTrigger("OnEnemyDeath");
            Destroy(GetComponent<Collider2D>());
            _explosionSound.Play();
            _isAlive = false;
            Destroy(this.gameObject, 1f);
        }

        else if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
                _player.AddToScore(10);
            _enemyAnimation.SetTrigger("OnEnemyDeath");
            Destroy(GetComponent<Collider2D>());
            _explosionSound.Play();
            _isAlive = false;
            Destroy(this.gameObject, 1f);
        }
    }
}
