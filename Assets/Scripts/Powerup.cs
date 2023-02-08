using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float _verticalDownLimit = -2.0f;
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private int _powerupID;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
            

        if (transform.position.y < _verticalDownLimit)
        {
            Destroy(this.gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player _player = other.GetComponent<Player>();
            if (other != null)
            {
                switch(_powerupID)
                {
                    case 0: //TripleShot
                        _player.TripleShotActivate();
                        break;
                    case 1: //SpeedBoost
                        _player.SpeedBoostActivate();
                        break;
                    case 2: //SpeedBoost
                        //_player.SpeedBoostActivate();
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
