using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerController;
    private PlayerPhysics playerPhysics;

    private float jumpHeight = 2.0f;
    
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        playerPhysics = player.GetComponent<PlayerPhysics>();
    }

    void Update()
    {   
        if(playerController.isJump && playerPhysics.isGrounded) {
            playerPhysics.velocity.y = Mathf.Sqrt(jumpHeight * -2f * playerPhysics.gravity);
        }
    }
}
