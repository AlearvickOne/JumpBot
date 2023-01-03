using System.Collections.Generic;
using UnityEngine;

public class SpawnsPlatforms : MonoBehaviour
{
    [SerializeField] protected internal List<GameObject> spawnPlatformsList;
    [SerializeField] protected internal Transform[] spawnPlatformsArray;
    [SerializeField] GameObject[] prefabsPlatform;
    protected internal int score;
    Vector2 randomPoint;
    void Start()
    {
        score = ScriptableParametrs.scoreStatic;
        StartDefaultPlatformSpawn();
        SuperJumpPlatformSpawn();
        TurboPlatformSpawn();
    }

    void Update()
    {
        TeleportPatformStartPosition();
        ScriptableParametrs.scoreStatic = score;
        PlayerPrefs.SetInt("ScorePoint", ScriptableParametrs.scoreStatic);
        PlayerPrefs.Save();
    }

    void SpawnRandomPoint(int randXMin, int randXMax, int randYMin, int randYMax)
    {
        int randomX = Random.Range(randXMin, randXMax);
        int randomY = Random.Range(randYMin, randYMax);

        randomPoint = new Vector2(randomX, randomY);
    }

    void TeleportPatformStartPosition()
    {
        spawnPlatformsArray = GetComponentsInChildren<Transform>();
        while (true)
        {
            for (int i = 0; i < spawnPlatformsList.Count; i++)
            {
                if (spawnPlatformsList[i].transform.position.y < -7)
                {
                    SpawnRandomPoint(-2, 2, 5, 15);
                    spawnPlatformsList[i].transform.position = randomPoint;
                }
            }
            return;
        }
    }

    void StartDefaultPlatformSpawn()
    {
        for (int i = 0; i < 25; i++)
        {
            SpawnRandomPoint(-2,2,5,15);
            Instantiate(prefabsPlatform[0], randomPoint, Quaternion.identity);
        }
    }

    void SuperJumpPlatformSpawn()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnRandomPoint(-2,2,15,25);
            Instantiate(prefabsPlatform[1], randomPoint, Quaternion.identity);
        }
    }

    void TurboPlatformSpawn()
    {
        for (int i = 0; i < 2; i++)
        {
            SpawnRandomPoint(-2,2,25,30);
            Instantiate(prefabsPlatform[2], randomPoint, Quaternion.identity);
        }
    }
}
