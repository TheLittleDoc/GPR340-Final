using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float sensitivity = 2f;

    private float cameraVerticalRotation = 0f;

    private bool lockedCursor = true;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * sensitivity;
        float inputY = Input.GetAxis("Mouse Y") * sensitivity;

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;
        
        player.Rotate(Vector3.up * inputX);
    }
}
