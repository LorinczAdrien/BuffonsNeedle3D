using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    // Camera movement with WASD
    [SerializeField]
    private float cameraMoveSpeed = 3.0f;
    
    // Camera movement with mouse
    [SerializeField]
    private float lookSpeed = 2.0f;
    [SerializeField]
    private float cameraMoveYSpeed = 500.0f;
    [SerializeField]
    Vector2 mouseTurn = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.moveCameraWASD();
        this.moveCameraMouse();
    }

    private void moveCameraWASD()
    {
        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0, 0, 0);

        // In which directions we are moving
        if (Input.GetKey(KeyCode.W))
        {
            movement.z += cameraMoveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.z -= cameraMoveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x += cameraMoveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x -= cameraMoveSpeed;
        }

        // Horizontal movement
        if (xpos >= 0 && xpos < cameraMoveSpeed)
        {
            movement.x -= cameraMoveSpeed;
        }
        else if (xpos <= Screen.width && xpos > Screen.width - cameraMoveSpeed)
        {
            movement.x += cameraMoveSpeed;
        }

        // Vertical movement
        if (ypos >= 0 && ypos < cameraMoveSpeed)
        {
            movement.z -= cameraMoveSpeed;
        }
        else if (ypos <= Screen.height && ypos > Screen.height - cameraMoveSpeed)
        {
            movement.z += cameraMoveSpeed;
        }

        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;
        // away from ground movement
        movement.y -= cameraMoveYSpeed * Input.GetAxis("Mouse ScrollWheel");

        // Calculate desired camera position based on received input
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x; destination.z += movement.z;

        // If a change in position is detected perform the necessary update
        if (destination != origin)
        {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * cameraMoveSpeed);
        }

        // Move with a different scalar in the y direction
        destination = origin;
        destination.y += movement.y;
        if (destination != origin)
        {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * cameraMoveYSpeed);
        }
    }

    private void moveCameraMouse()
    {
        // Get mouse movement amount and multiply by scalar
        this.mouseTurn.x += Input.GetAxis("Mouse X") * this.lookSpeed;
        this.mouseTurn.y += Input.GetAxis("Mouse Y") * this.lookSpeed;

        // Apply rotation
        this.transform.localRotation = Quaternion.Euler(-mouseTurn.y, mouseTurn.x, 0);
    }
}
