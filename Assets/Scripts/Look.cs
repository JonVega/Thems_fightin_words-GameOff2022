using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    //public PlayerController playerController;

    public GameObject player;
    private PlayerController playerController;
    public Transform playerBody;
    public Camera cam;
    
    [SerializeField] float mouseSensitivity = 60f; //[SerializeField] means private but show in UnityEditor
    private float xRotation = 0f;
    private float mouseX = 0f;
    private float mouseY = 0f;

    void Start() {
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        mouseX = playerController.setLook.x * mouseSensitivity * Time.deltaTime;
        mouseY = playerController.setLook.y * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * playerController.setLook.x);
    }
}
