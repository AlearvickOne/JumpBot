using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] BoxCollider2D playerColl;
    [SerializeField] StonePlatform scrSP;

    void Start()
    {
        playerColl = GameObject.FindGameObjectWithTag("SPAWNS").transform.GetChild(1).GetChild(0).GetComponent<BoxCollider2D>();
        scrSP = GetComponentInParent<StonePlatform>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (playerColl == coll)
        {
            scrSP.secondDown = 1;
        }
    }
}
