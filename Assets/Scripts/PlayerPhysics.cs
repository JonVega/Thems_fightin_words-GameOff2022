using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    public float speed = 9f;
    public float groundDistance = 0.4f; //radius of sphere going to check to see if on ground
    public float gravity = -9.81f * 2; //towards 0 = slower fall, further negative = faster fall
    public Vector3 velocity = Vector3.zero;
    public bool isGrounded = false;
}
