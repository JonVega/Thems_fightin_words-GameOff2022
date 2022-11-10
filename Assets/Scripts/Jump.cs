using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public GameObject player;
    private PlayerController pc;
    private Move mv;

    private float jumpHeight = 2.0f;
    
    void Start()
    {
        pc = player.GetComponent<PlayerController>();
        mv = player.GetComponent<Move>();
    }

    void Update()
    {   
        if(pc.isJump && mv.isGrounded) {
            mv.velocity.y = Mathf.Sqrt(jumpHeight * -2f * mv.gravity);
        }
    }
}
