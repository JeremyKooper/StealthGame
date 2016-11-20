using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float cameraLookSpeed = 1.0f;

    public Camera playerCamera;
    public CharacterController characterController;
    

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

	void Update ()
    {
        //handle look position
        Vector2 mouseIn = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        mouseIn *= cameraLookSpeed;
        characterController.transform.Rotate(new Vector3(0, mouseIn.x, 0));
        playerCamera.transform.Rotate(new Vector3(mouseIn.y * -1, 0, 0));

        //lock camera between 90 and 270 degrees of rotation.(no upside down cameras)
        float verticalLookAngle = Quaternion.Angle(playerCamera.transform.rotation, characterController.transform.rotation);
        Vector3 rotEuler = playerCamera.transform.rotation.eulerAngles;
        if (verticalLookAngle > 90 && playerCamera.transform.rotation.eulerAngles.x > 180)
        {
            rotEuler.x = 270;
            playerCamera.transform.transform.rotation = Quaternion.Euler(rotEuler);
        }
        else if (verticalLookAngle > 90 && playerCamera.transform.rotation.eulerAngles.x > 0)
        {
            rotEuler.x = 90;
            playerCamera.transform.transform.rotation = Quaternion.Euler(rotEuler);
        }


        //handle movement
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        Vector3 movementV = new Vector3(horz * movementSpeed, 0, vert * movementSpeed);
        movementV = characterController.transform.rotation * movementV; //rotate movement vector by camera direction
        characterController.SimpleMove(movementV);
    }
}
