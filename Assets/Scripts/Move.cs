using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public Transform playerGroundCheck;
    public LayerMask groundMask;
    public CharacterController controller;
    
    public GameObject player;
    private PlayerController playerController;
    private PlayerPhysics playerPhysics;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //hides mouse cursor in game
        playerController = player.GetComponent<PlayerController>();
        playerPhysics = player.GetComponent<PlayerPhysics>();
    }

    void Update()
    {
        playerPhysics.isGrounded = Physics.CheckSphere(playerGroundCheck.position, playerPhysics.groundDistance, groundMask);
        if(playerPhysics.isGrounded && playerPhysics.velocity.y < 0.0f) {
            playerPhysics.velocity.y = -2f;
        }
        Vector3 move = transform.right * playerController.setMove.x + transform.forward * playerController.setMove.y;
        controller.Move(move * playerPhysics.speed * Time.deltaTime);
        playerPhysics.velocity.y += playerPhysics.gravity * Time.deltaTime;
        controller.Move(playerPhysics.velocity * Time.deltaTime);
    }
}
