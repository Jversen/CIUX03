using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Camera mainCamera;
    public float lookSpeed = 400f;

    private Vector3 mainCameraInitialPosition;
    private Quaternion mainCameraInitialRotation;

    void Start()
    {
        mainCameraInitialRotation = mainCamera.transform.localRotation;
        mainCameraInitialPosition = mainCamera.transform.position;
    }

    void Update()
    {
        bool freeLookPressed = Input.GetButton("Free Look");
        bool freeLookUp = Input.GetButtonUp("Free Look");

        if (freeLookPressed)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            mainCamera.transform.RotateAround(transform.position, transform.up, -mouseX * Time.deltaTime * lookSpeed);
            mainCamera.transform.RotateAround(transform.position, mainCamera.transform.right, mouseY * Time.deltaTime * lookSpeed);
        }
        else if (freeLookUp)
        {
            mainCamera.transform.position = mainCameraInitialPosition;
            mainCamera.transform.localRotation = mainCameraInitialRotation;
        }
    }
}
