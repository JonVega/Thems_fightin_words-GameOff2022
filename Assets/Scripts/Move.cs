using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public PlayerController playerController;
    public Transform playerGroundCheck;
    public LayerMask groundMask;
    public CharacterController controller;

    public float speed = 9f;
    public float groundDistance = 0.4f; //radius of sphere going to check to see if on ground
    public float gravity = -9.81f * 2; //towards 0 = slower fall, further negative = faster fall
    public Vector3 velocity;
    public bool isGrounded;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //hides mouse cursor in game
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(playerGroundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0.0f) {
            velocity.y = -2f;
        }
        Vector3 move = transform.right * playerController.movement.ReadValue<Vector2>().x + transform.forward * playerController.movement.ReadValue<Vector2>().y;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
