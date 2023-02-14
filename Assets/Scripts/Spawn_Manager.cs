using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    private float _horizontalRightLimit = 26f, _horizontalLeftLimit = -26f, _verticalUpLimit = 28f;
    private IEnumerator spawn_enemy;
    private IEnumerator spawn_powerup;
    [SerializeField] private float spawn_enemy_timer = 5.0f;
    [SerializeField] private float spawn_powerup_timer = 20.0f;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _astroidPrefab;
    [SerializeField] private GameObject[] powerups;
    private bool _spawning = true;


    void Start()
    {
        
    }

    void Update()
    {

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine(spawn_enemy_timer, _enemyPrefab));
        StartCoroutine(SpawnRoutinePowerup(spawn_powerup_timer));
    }

    IEnumerator SpawnRoutine(float spawn_timer, GameObject currObject) //Spawn enemy in random position within limits per spawn_timer
    {
        while (_spawning)
        {
            Vector3 loc = new Vector3(Random.Range(_horizontalLeftLimit, _horizontalRightLimit), _verticalUpLimit, 0);
            GameObject newEnemy = Instantiate(currObject, loc, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(spawn_timer);
        }
    }
    IEnumerator SpawnRoutinePowerup(float spawn_timer) //Spawn powerup in random position within limits per spawn_powerup_timer +- 2s
    {
        while (_spawning)
        {
            yield return new WaitForSeconds(spawn_powerup_timer + Random.Range(-2.0f, 2.0f));
            Vector3 loc = new Vector3(Random.Range(_horizontalLeftLimit, _horizontalRightLimit), _verticalUpLimit, 0);
            int randomPowerup = Random.Range(0, 3);
            switch (randomPowerup)
            {
                case 0:
                    GameObject newTripleShotPowerup = Instantiate(powerups[0], loc, Quaternion.identity); //Spawn tripleshotPowerup
                    break;
                case 1:
                    GameObject newSpeedboostPowerup = Instantiate(powerups[1], loc, Quaternion.identity); //Spawn SpeedPowerup
                    break;
                case 2:
                    GameObject newShieldPowerup = Instantiate(powerups[2], loc, Quaternion.identity); //Spawn shieldPowerup
                    break;
            }
        }
    }


    public void OnPlayerDeath()
    {
        _spawning = false;
    }
}
