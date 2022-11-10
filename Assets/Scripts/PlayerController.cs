using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    /* Action Map */
    public InputActionAsset inputAsset;
    public InputActionMap player;
    public InputAction movement;
    public InputAction looking;
    public PlayerInputManager playerInputManager;

    public List<PlayerInput> players = new List<PlayerInput>();

    public bool isJump {get; private set;} = false;

    private void Awake() {
        //note: inputAction asset is not static or global
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void OnEnable() {
        player.Enable();
        movement = player.FindAction("Movement");
        looking = player.FindAction("Looking");
        player.FindAction("Jump").performed += DoJump; //subscribe to event
        
        playerInputManager.onPlayerJoined += AddPlayer;

        /*lambda expression*/
        //player.FindAction("Jump").started += ctx => isJump = ctx.ReadValueAsButton();
        //don't use above, can memory leak because not able to unsubscribe (Microsoft C# docs suggests)
    }

    private void OnDisable() {
        player.FindAction("Jump").performed -= DoJump;
        player.Disable();
    }

    private void DoJump(InputAction.CallbackContext ctx) {
        isJump = ctx.ReadValueAsButton();
    }
    
    private void AddPlayer(PlayerInput player) {
        player.transform.GetChild(0).gameObject.SetActive(false); //turn off Almanac model
        player.transform.GetChild(1).gameObject.SetActive(true); //turn on Zasshi model
        players.Add(player);
    }
}
