using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    /* Action Map */
    public InputActionAsset inputAsset;
    public InputActionMap player;
    public PlayerInputManager playerInputManager;

    public List<PlayerInput> players = new List<PlayerInput>();

    public bool isJump {get; private set;} = false;
    public bool isSprint {get; private set;} = false;
    public Vector2 setMove {get; private set;} = Vector2.zero;
    public Vector2 setLook {get; private set;} = Vector2.zero;

    private void Awake() {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void OnEnable() {
        player.Enable();
        player.FindAction("Looking").performed += SetLook;
        player.FindAction("Movement").performed += SetMove;
        player.FindAction("Jump").performed += DoJump; //subscribe to event
        player.FindAction("Sprint").performed += DoSprint;
        
        playerInputManager.onPlayerJoined += AddPlayer;

        /*lambda expression*/
        //player.FindAction("Jump").started += ctx => isJump = ctx.ReadValueAsButton();
        //don't use above, can memory leak because not able to unsubscribe (Microsoft C# docs suggests)
    }

    private void OnDisable() {
        player.FindAction("Looking").performed -= SetLook;
        player.FindAction("Movement").performed -= SetMove;
        player.FindAction("Jump").performed -= DoJump;
        player.FindAction("Sprint").performed -= DoSprint;
        player.Disable();
    }

    private void DoJump (InputAction.CallbackContext ctx) {
        isJump = ctx.ReadValueAsButton();
    }

    private void DoSprint (InputAction.CallbackContext ctx) {
        isSprint = ctx.ReadValueAsButton();
    }

    private void SetMove (InputAction.CallbackContext ctx) {
        setMove = ctx.ReadValue<Vector2>();
    }

    private void SetLook (InputAction.CallbackContext ctx) {
        setLook = ctx.ReadValue<Vector2>();
    }
    
    private void AddPlayer(PlayerInput player) {
        player.transform.GetChild(0).gameObject.SetActive(false); //turn off Almanac model
        player.transform.GetChild(1).gameObject.SetActive(true); //turn on Zasshi model
        players.Add(player);
    }
}
