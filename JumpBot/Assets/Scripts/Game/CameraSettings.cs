using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] Rigidbody2D cameraRb;
    [SerializeField] protected internal bool gameIsPlay = true;
    [SerializeField] GameObject playerGamepad;
    void Start()
    {
        cameraRb = Camera.main.GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if(gameIsPlay)
        {
            mainCamera.transform.position = new Vector3(0, -0.18f, -10);
        }
        if (!gameIsPlay)
        {
            cameraRb.velocity = new Vector2(0, -10.5f) * 500 * Time.deltaTime;
            if(transform.position.y <= -10.5f)
            {
                playerGamepad.SetActive(false);
                cameraRb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
}
