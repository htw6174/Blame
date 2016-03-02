using UnityEngine;
using System.Collections;

/// <summary>
/// This script should be attached to the player, and handles all the forces applied.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public static GameObject player;
    public GameObject playerCamera;

    public bool forceMovement = false;
    public float speed = 5.0f;
    [Range(0f, 1f)]
    public float rotationSpeed = 0.1f;

    private Rigidbody playerRigidbody;

    void Start()
    {
        player = this.gameObject;
        playerCamera = GameObject.FindGameObjectWithTag(Tags.camera);
        playerRigidbody = GetComponent<Rigidbody>();
    }
    
    /// <summary>
    /// Moves the player smoothly in the direction of movementVector
    /// </summary>
    /// <param name="movementVector">Direction of movement</param>
    public void MovePlayer(Vector3 movementVector)
    {
        if (forceMovement) MoveForce(movementVector);
        else MoveVelocity(movementVector);
        if (movementVector.magnitude > 0.05f)
        {
            RotateToCamera();
        }
    }

    private void MoveForce(Vector3 movementVector)
    {
        playerRigidbody.AddRelativeForce(movementVector);
    }

    private void MoveVelocity(Vector3 movementVector)
    {
        if (movementVector.magnitude > 1f) movementVector = movementVector.normalized;
        playerRigidbody.velocity = playerCamera.transform.TransformVector(movementVector) * speed;
    }

    private void RotateToCamera()
    {
        player.transform.rotation = Quaternion.Slerp(transform.rotation, playerCamera.transform.rotation, rotationSpeed);
    }
}
