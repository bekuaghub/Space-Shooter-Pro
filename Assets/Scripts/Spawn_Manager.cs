using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    private IEnumerator spawn_enemy;
    [SerializeField]
    private float spawn_enemy_timer = 5.0f;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _spawning = true;
    // Start is called before the first frame update
    void Start()
    {
        spawn_enemy = SpawnRoutine(spawn_enemy_timer, _enemyPrefab);
        StartCoroutine(spawn_enemy);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnRoutine(float spawn_timer, GameObject currObject)
    {
        while (true && _spawning)
        {
            yield return new WaitForSeconds(spawn_timer);
            GameObject newEnemy = Instantiate(currObject);
            newEnemy.transform.parent = _enemyContainer.transform;
        }
    }

    public void OnPlayerDeath()
    {
        _spawning = false;
    }
}
