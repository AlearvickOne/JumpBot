using UnityEngine;

public class EnemyCharacters : MonoBehaviour
{
    [SerializeField] BoxCollider2D playerColl;
    [SerializeField] Rigidbody2D enemyRb;
    [SerializeField] private CameraSettings scrCS;
    void Start()
    {
        scrCS = Camera.main.GetComponent<CameraSettings>();
        playerColl = GameObject.FindGameObjectWithTag("SPAWNS").transform.GetChild(1).GetChild(0).GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        EnemyLeftRight();
    }

    void EnemyLeftRight()
    {
        if (transform.position.x < -3)
        {
            transform.position = new Vector3(2.9f, transform.position.y);
        }
        if (transform.position.x > 3)
        {
            transform.position = new Vector3(-2.9f, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (playerColl == coll)
        {
            scrCS.gameIsPlay = false;
        }
    }
}
