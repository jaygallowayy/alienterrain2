using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable]
public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 2f;

    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localEulerAngles = Vector3.right * xRotation;
        playerBody.Rotate(Vector3.up * mouseX);
    }

}
