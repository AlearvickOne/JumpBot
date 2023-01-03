using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D collPlayer;
    [SerializeField] private int jumpUp;
    [SerializeField] private int speedPlayerLeftRight;
    [SerializeField] private CameraSettings scrCS;
    private Rigidbody2D playerRb;

    private bool buttonIsDown;
    private bool isGround;
    private bool startJump = true;
    void Awake()
    {
        collPlayer = GetComponent<BoxCollider2D>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        PlayerToParent();
        StartCoroutine(IStartPlayerJumpUp(5));
    }
    private void Update()
    {
        WallsPlayer();
        PlayerLose();
    }

    #region [ Player To Parent Transform ]

    [SerializeField] GameObject parentPlayers;

    void PlayerToParent()
    {
        gameObject.transform.parent = parentPlayers.transform;
    }

    #endregion

    #region [ Private Methods ]
    void PlayerJump()
    {
        if (isGround == true)
        {
            playerRb.velocity = (Vector2.up * jumpUp * Time.fixedDeltaTime);
            isGround = false;
        }
    }

    void WallsPlayer()
    {
        if (playerRb.velocity.y > 0)
        {
            collPlayer.isTrigger = true;
        }

        if (playerRb.velocity.y < 0)
        {
            collPlayer.isTrigger = false;
        }

        if (transform.position.y > 4)
        {
            transform.position = new Vector2(transform.position.x, 4);
        }

        if (transform.position.x < -2.5f)
        {
            transform.position = new Vector2(2.5f, transform.position.y);
        }

        if (transform.position.x > 2.5f)
        {
            transform.position = new Vector2(-2.5f, transform.position.y);
        }
    }

    void PlayerLose()
    {
        if(scrCS.gameIsPlay == false)
        {
            collPlayer.isTrigger = true;
        }
    }

    #endregion

    #region  [ IEnumerators ]
    IEnumerator IStartPlayerJumpUp(int second)
    {
        if (startJump)
        {
            playerRb.velocity = (Vector2.up * jumpUp * Time.fixedDeltaTime);
            playerRb.gravityScale = 0;
            collPlayer.isTrigger = true;
            yield return new WaitForSeconds(0.5f);
            playerRb.constraints = RigidbodyConstraints2D.FreezePositionY;
            playerRb.gravityScale = 1;
            yield return new WaitForSeconds(second);
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            playerRb.gravityScale = 1;
            collPlayer.isTrigger = false;
            startJump = false;
        }

    }
    #endregion

    #region [ Triggers And Collisions ]

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Platforms"))
        {
            isGround = true;
            PlayerJump();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Lost"))
        {
            scrCS.gameIsPlay = false;
        }
    }

    #endregion

    #region [ Buttons Ui Player Left || Right ]

    public void OnPointerDown()
    {
        this.buttonIsDown = true;
    }

    public void OnPointerUp()
    {
        this.buttonIsDown = false;
    }

    public void PlayerToRightButton()
    {
        if (!this.buttonIsDown == false)
        {
            playerRb.velocity = (Vector2.zero * 0);

            return;
        }
        Debug.Log("left");
        playerRb.velocity = (Vector2.right * speedPlayerLeftRight * Time.fixedDeltaTime);
    }
    public void PlayerToLeftButton()
    {
        if (!this.buttonIsDown == false)
        {
            playerRb.velocity = (Vector2.zero * 0);
            return;
        }
        Debug.Log("right");
        playerRb.velocity = (Vector2.left * speedPlayerLeftRight * Time.fixedDeltaTime);
    }
    #endregion
}
