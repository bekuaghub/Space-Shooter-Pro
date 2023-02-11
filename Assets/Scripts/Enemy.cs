using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _verticalDownLimit = -2.0f;
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private int _damage = 10;
    private Animator _enemyAnimation;
    private Player _player;
    private AudioSource _explosionSound;
    bool _isAlive = true;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _explosionSound = GetComponent<AudioSource>();
        _enemyAnimation = GetComponent<Animator>();
    }
    
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < _verticalDownLimit)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //Collision with player or laser
    {
        if(other.tag == "Player" && _isAlive)
        {
            Player player = other.GetComponent<Player>();

            _player.AddToScore(10);
            if (player != null)
                player.DamageTaken(_damage);
            _enemyAnimation.SetTrigger("OnEnemyDeath");
            _isAlive = false;
            _explosionSound.Play();
            Destroy(this.gameObject, 1f);
        }

        else if(other.tag == "Laser" && _isAlive)
        {
            Destroy(other.gameObject);
            if(_player != null)
                _player.AddToScore(10);
            _enemyAnimation.SetTrigger("OnEnemyDeath");
            _isAlive = false;
            _explosionSound.Play();
            Destroy(this.gameObject, 1f);
        }
    }
}
