using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StonePlatform : MonoBehaviour
{
    [Header("List & GameObjects")]
    [SerializeField] private List<GameObject> spawnsList;
    [SerializeField] private GameObject parentSPlatforms;
    [Header("Scripts")]
    [SerializeField] private SpawnsPlatforms scrSP;
    [SerializeField] private SpawnEnemy scrSE;
    [Header("Colliders")]
    [SerializeField] private BoxCollider2D playerColl;
    [Header("Text")]
    [SerializeField] private TMP_Text scoreText;
    [Space(10)]
    [SerializeField] private int speedPlatformDown;
    private Rigidbody2D platformRb;
    private bool block = false;
    protected internal float secondDown = 0.3f;

    private void Start()
    {
        SpawnsList();
        platformRb = GetComponent<Rigidbody2D>();
        GameObject all_Ui = GameObject.FindGameObjectWithTag("ALL_UI").transform.gameObject;
        GameObject spawns = GameObject.FindGameObjectWithTag("SPAWNS").transform.gameObject;
        scoreText = all_Ui.transform.GetChild(2).GetComponent<TMP_Text>();
        scrSP = GetComponentInParent<SpawnsPlatforms>();
        scrSE = spawns.transform.GetChild(2).GetComponent<SpawnEnemy>();

    }
    private void FixedUpdate()
    {
        if (block == false)
        {
            StartCoroutine(IStartGameSpeedFust());
        }
    }
    private void Update()
    {
        FrezeRotationObject();
    }

    #region [ Private Methods ]
    private void SpawnsList()
    {
        GameObject spawns = GameObject.FindGameObjectWithTag("SPAWNS").transform.gameObject;
        playerColl = spawns.transform.GetChild(1).GetChild(0).GetComponent<BoxCollider2D>();
        foreach (Transform child in spawns.transform)
        {
            spawnsList.Add(child.gameObject);
        }

        parentSPlatforms = spawnsList[0];
        scrSP = spawnsList[0].GetComponent<SpawnsPlatforms>();

        gameObject.transform.parent = parentSPlatforms.transform;
        scrSP.spawnPlatformsList.Add(gameObject);
    }

    private void PlatformAndEnemyMoveDown()
    {
        if (secondDown == 3 || secondDown == 1)
        {
            playerColl.enabled = false;
            Rigidbody2D playerRb = playerColl.GetComponent<Rigidbody2D>();
            playerRb.constraints = RigidbodyConstraints2D.FreezePositionY;
            
            scrSP.score += 50;
            scoreText.text = "" + scrSP.score.ToString();
        }
        foreach (Transform child in scrSP.spawnPlatformsArray)
        {
            Rigidbody2D childRb = child.GetComponent<Rigidbody2D>();
            childRb.velocity = Vector2.down * speedPlatformDown * Time.fixedDeltaTime;
            childRb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        foreach(Transform child in scrSE.enemyList)
        {
            Rigidbody2D childRb = child.GetComponent<Rigidbody2D>();
            childRb.velocity = Vector2.down * speedPlatformDown * Time.fixedDeltaTime;
        }
    }

    private void PlatformAndEnemyMoveStop()
    {
        foreach (Transform child in scrSP.spawnPlatformsArray)
        {
            Rigidbody2D childRb = child.GetComponent<Rigidbody2D>();
            childRb.velocity = Vector2.zero * speedPlatformDown * Time.fixedDeltaTime;
            childRb.constraints = RigidbodyConstraints2D.FreezePositionY;
            platformRb.gravityScale = 0;
        }
        foreach (Transform child in scrSE.enemyList)
        {
            Rigidbody2D childRb = child.GetComponent<Rigidbody2D>();
            childRb.velocity = Vector2.zero * speedPlatformDown * Time.fixedDeltaTime;
        }
        secondDown = 0.3f;
        playerColl.enabled = true;
        Rigidbody2D playerRb = playerColl.GetComponent<Rigidbody2D>();
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void FrezeRotationObject()
    {
        transform.rotation = new Quaternion(0,0,0,0);
    }

    #endregion

    #region [ IEnumerators ]
    IEnumerator IStartGameSpeedFust()
    {
        platformRb.velocity = Vector2.down * speedPlatformDown * Time.fixedDeltaTime;
        speedPlatformDown = 1000;
        yield return new WaitForSeconds(5);
        foreach (Transform child in scrSP.spawnPlatformsArray)
        {
            Rigidbody2D childRb = child.GetComponent<Rigidbody2D>();
            childRb.constraints = RigidbodyConstraints2D.FreezePositionX;
            childRb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        platformRb.gravityScale = 1;
        speedPlatformDown = 300;
        block = true;

    }
    IEnumerator IPlatformAndEnemyDown()
    {
        PlatformAndEnemyMoveDown();
        yield return new WaitForSeconds(secondDown);
        PlatformAndEnemyMoveStop();
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider == playerColl)
        {
            StartCoroutine(IPlatformAndEnemyDown());

            scrSP.score += 10;
            scoreText.text ="" + scrSP.score.ToString(); 
        }
    }

}
