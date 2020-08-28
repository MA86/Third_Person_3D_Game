using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class RelMovement : MonoBehaviour
{
    // Reference to camera transform
    public Transform camera;
    public float rotationSpeed = 14.0f;
    public float movementSpeed = 6.0f;
    CharacterController controller;
    Animator animator;

    void Start()
    {
        // Get components
        this.controller = this.GetComponent<CharacterController>();
        this.animator = this.GetComponent<Animator>();
    }
    void Update()
    {
        // Create an empty vector 
        Vector3 movement = new Vector3(0f, 0f, 0f);

        // Get movement direction from keyboard input
        float horzontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        // Move only if there is a keyboard input...
        if (horzontalInput != 0 || verticalInput != 0)
        {
            // Fill the empty vector with direction in the xz-plane
            movement.x = horzontalInput;
            movement.z = verticalInput;

            // Store camera's current local coordinate system
            Quaternion temp = this.camera.rotation;

            // Orient camera's local coordinate system, pointing it to player
            this.camera.eulerAngles = new Vector3(0f, this.camera.eulerAngles.y, 0f);

            // Translate the vector from camera's local coordinate system to global
            movement = this.camera.TransformDirection(movement);
            this.camera.rotation = temp;

            // *LookRotation() returns a rotation 'looking' at the movement direction* //
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(movement), this.rotationSpeed * Time.deltaTime);

            // Prepare vector for moving now
            movement = movement * this.movementSpeed * Time.deltaTime;

            // Make sure diagonal movement is the same length as axis movement
            movement = Vector3.ClampMagnitude(movement, this.movementSpeed);

            // Move through character controller
            this.controller.Move(movement);

            // Play 'run' animation
            this.animator.SetFloat("Speed", 1f);
        }
        else
        {
            // Play 'idle' animation
            this.animator.SetFloat("Speed", 0f);
        } 
    }
}
