using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /* Action Map */
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction movement;
    private InputAction looking;

    public Transform playerBody;
    public Transform playerGroundCheck;
    public Camera cam;
    public LayerMask groundMask;
    public CharacterController controller;
    public float mouseSensitivity = 70f;
    private float xRotation = 0f;
    public float groundDistance = 0.4f; //radius of sphere going to check to see if on ground
    public float speed = 12f;
    public float gravity = -9.81f * 2; //towards 0 = slower fall, further negative = faster fall
    public float jumpHeight = 1.5f * 1;
    bool isGrounded;

    Vector3 velocity;

    private void Awake() {
        //note: inputAction asset is not static or global
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
    }

    private void OnEnable() {
        player.FindAction("Jump").started += DoJump; //subscribe to event
        player.Enable();
        movement = player.FindAction("Movement");
        looking = player.FindAction("Looking");
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
}
