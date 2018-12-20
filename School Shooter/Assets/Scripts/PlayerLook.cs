using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {

    public float mouseSensitivity;
    private Vector3 eulerRotation;
    public Transform playerBody;
    private Vector3 playerRotation;

    // Use this for initialization
    void Awake ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        eulerRotation = new Vector3(0, 0, 0);
        playerRotation = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
    }

    void CameraRotation()
    {
        float MouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        eulerRotation.x -= MouseY;
        playerRotation.y += MouseX;
        if (eulerRotation.x < -90.0f)
        {
            eulerRotation.x = -90.0f;
            MouseY = 0.0f;
        }
        if (eulerRotation.x > 90.0f)
        {
            eulerRotation.x = 90.0f;
            MouseY = 0.0f;
        }
        playerBody.eulerAngles = playerRotation;
        transform.eulerAngles = eulerRotation + playerRotation;
    }
}
