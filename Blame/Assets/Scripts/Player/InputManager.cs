using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public bool debugInputs;

    public GameObject player;
    public PlayerCamera playerCamera;
    public PlayerMovement playerMovement;

    public string axisLeftRight = "LeftRight";
    public string axisForwardBack = "ForwardBack";
    public string axisUpDown = "UpDown";

    public string walkModifier = "Walk";
    [Range(0f, 1f)]
    public float walkSpeed = 0.5f;

    public bool invertCamera;
    [Range(0.1f, 10f)]
    public float camHorizontalSensitivity = 1f;
    [Range(0.1f, 10f)]
    public float camVerticalSensitivity = 1f;
    public string camLeftRight = "CamLeftRight";
    public string camUpDown = "CamUpDown";

    public string pause = "Pause";
    public bool paused = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerCamera = GameObject.FindGameObjectWithTag(Tags.camera).GetComponent<PlayerCamera>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        CheckInputs();
    }

    private void CheckInputs()
    {
        if(paused == false)
        {
            Moveplayer();
            MoveCamera();
        }
        Pause();
    }

    private void Moveplayer()
    {
        Vector3 movementVector = Vector3.zero;
        movementVector.x = Input.GetAxis(axisLeftRight);
        movementVector.y = Input.GetAxis(axisUpDown);
        movementVector.z = Input.GetAxis(axisForwardBack);

        if (Input.GetButton(walkModifier))
        {
            movementVector *= walkSpeed;
        }

        playerMovement.MovePlayer(movementVector);

        if (debugInputs)
        {
            Debug.Log(movementVector);
        }
    }

    private void MoveCamera()
    {
        Vector3 movementVector = Vector3.zero;
        movementVector.x = Input.GetAxis(camLeftRight) * camHorizontalSensitivity;
        movementVector.y = Input.GetAxis(camUpDown) * camVerticalSensitivity * (invertCamera ? 1f : -1f);

        //TODO: Pass vertical movement to camera, and horizontal movement to player
        //playerMovement.rotatePlayer(movementVector.x);
        playerCamera.MoveCamera(movementVector);

        if (debugInputs)
        {
            Debug.Log(movementVector);
        }
    }

    private void Pause()
    {
        //float pauseValue = Input.GetAxis(pause);
        if(Input.GetButtonDown(pause))
        {
            SwitchMouseLock();
            paused = ! paused;
        }
    }

    private void SwitchMouseLock()
    {
        if (Cursor.lockState == CursorLockMode.None) Cursor.lockState = CursorLockMode.Locked;
        else if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;

        Cursor.visible = !Cursor.visible;
    }
}