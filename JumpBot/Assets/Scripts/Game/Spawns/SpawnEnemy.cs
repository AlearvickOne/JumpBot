using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject[] enemy;
    [SerializeField] protected internal List<Transform> enemyList;
    [Space(10)]
    [SerializeField] BoxCollider2D playerColl;
    [Space(10)]
    [SerializeField] float timerSpawn;
    static float timerSpawnStatic;


    Transform spawnTransform;
    private void Start()
    {
        timerSpawnStatic = timerSpawn;
        spawnTransform = transform;
    }
    void Update()
    {
        RandomEnemySpawn();
    }
    private void FixedUpdate()
    {

    }

    void RandomEnemySpawn()
    {
        timerSpawn -= Time.deltaTime;

        if (timerSpawn <= 0)
        {
            int randomEnemy = Random.Range(0, enemy.Length);
            Vector2 randomPos = new Vector2(Random.Range(-2, 2), Random.Range(5, 15));

            GameObject spawnEnemy = Instantiate(enemy[randomEnemy], randomPos, Quaternion.identity);
            spawnEnemy.transform.parent = spawnTransform;
            enemyList.Add(spawnEnemy.transform);
            timerSpawn = timerSpawnStatic;
        }
    }


}
