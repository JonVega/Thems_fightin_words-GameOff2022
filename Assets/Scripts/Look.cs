using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public PlayerController playerController;
    public Transform playerBody;
    public Camera cam;
    public float mouseSensitivity = 60f;
    private float xRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        float mouseX = playerController.looking.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float mouseY = playerController.looking.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * playerController.looking.ReadValue<Vector2>().x);
    }
}
