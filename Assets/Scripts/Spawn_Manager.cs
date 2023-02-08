using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    private float _horizontalRightLimit = 8.8f, _horizontalLeftLimit = -8.8f, _verticalUpLimit = 12f;
    private IEnumerator spawn_enemy;
    private IEnumerator spawn_powerup;
    [SerializeField] private float spawn_enemy_timer = 5.0f;
    [SerializeField] private float spawn_powerup_timer = 20.0f;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _tripleShotPowerupPrefab;
    private bool _spawning = true;

    void Start()
    {
        spawn_enemy = SpawnRoutine(spawn_enemy_timer, _enemyPrefab);
        StartCoroutine(spawn_enemy);
        spawn_powerup = SpawnRoutinePowerup(spawn_powerup_timer, _tripleShotPowerupPrefab);
        StartCoroutine(spawn_powerup);
    }

    void Update()
    {

    }

    IEnumerator SpawnRoutine(float spawn_timer, GameObject currObject) //Spawn enemy in random position within limits per spawn_timer
    {
        while (_spawning)
        {
            yield return new WaitForSeconds(spawn_timer);
            Vector3 loc = new Vector3(Random.Range(_horizontalLeftLimit, _horizontalRightLimit), _verticalUpLimit, 0);
            GameObject newEnemy = Instantiate(currObject, loc, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
        }
    }
    IEnumerator SpawnRoutinePowerup(float spawn_timer, GameObject currObject) //Spawn powerup in random position within limits per spawn_powerup_timer +- 4s
    {
        while (_spawning)
        {
            yield return new WaitForSeconds(spawn_powerup_timer + Random.Range(-4.0f, 4.0f));
            Vector3 loc = new Vector3(Random.Range(_horizontalLeftLimit, _horizontalRightLimit), _verticalUpLimit, 0);
            GameObject newPowerup = Instantiate(currObject, loc, Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        _spawning = false;
    }
}
