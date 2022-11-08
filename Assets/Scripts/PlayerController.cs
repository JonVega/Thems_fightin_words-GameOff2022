using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    /* Action Map */
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction movement;
    private InputAction looking;
    public PlayerInputManager playerInputManager;

    public Transform playerBody;
    public Transform playerGroundCheck;
    public Camera cam;
    public LayerMask groundMask;
    public CharacterController controller;
    public float mouseSensitivity = 60f;
    private float xRotation = 0f;
    private float groundDistance = 0.4f; //radius of sphere going to check to see if on ground
    private float speed = 9f;
    private float gravity = -9.81f * 2; //towards 0 = slower fall, further negative = faster fall
    private float jumpHeight = 1.5f;
    bool isGrounded;
    Vector3 velocity;


    public List<PlayerInput> players = new List<PlayerInput>();

    private void Awake() {
        //note: inputAction asset is not static or global
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void OnEnable() {
        player.Enable();
        
        player.FindAction("Jump").started += DoJump; //subscribe to event
        movement = player.FindAction("Movement");
        looking = player.FindAction("Looking");

        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable() {
        player.FindAction("Jump").started -= DoJump;
        player.Disable();
    }

    void Start() {
        Cursor.lockState = CursorLockMode.Locked; //hides mouse cursor in game
    }

    private void FixedUpdate() //use physics engine
    {

    }

    private void Update() {
        Move();
        Look();
    }

    private void DoJump(InputAction.CallbackContext obj) {
        if(isGrounded) {
            //formula: v = sqrt(h * -2 * g)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void Look() {
        float mouseX = looking.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float mouseY = looking.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * looking.ReadValue<Vector2>().x);
    }

    private void Move() {
        isGrounded = Physics.CheckSphere(playerGroundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0.0f) {
            velocity.y = -2f;
        }
        Vector3 move = transform.right * movement.ReadValue<Vector2>().x + transform.forward * movement.ReadValue<Vector2>().y;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
    private void AddPlayer(PlayerInput player) {
        player.transform.GetChild(0).gameObject.SetActive(false); //turn off Almanac model
        player.transform.GetChild(1).gameObject.SetActive(true); //turn on Zasshi model
        players.Add(player);
    }
}
