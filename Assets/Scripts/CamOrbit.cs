using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <note>
/// Simple vector math was used in this script. Review it if you need to.
/// </note>
public class CamOrbit : MonoBehaviour
{
    // Get reference to just position component of target
    public Transform target;
    public float Speed = 7f;

    private float rotateY;
    private Vector3 _offset;


    void Start()
    {
        // Create the offset vector (originates at target and points to camera)
        this._offset = this.transform.position - this.target.transform.position;

        // Initialize rotateY with current y-rotation of camera
        this.rotateY = this.transform.eulerAngles.y;
    }
    // Called after Update()
    void LateUpdate()
    {
        // Append new rotation from input
        rotateY += Input.GetAxis("Mouse X") * this.Speed;

        // Convert to quaternion rotation
        Quaternion rotation = Quaternion.Euler(0f, this.rotateY, 0f);

        // Apply rotation to the offset vector
        Vector3 shiftedOffset = rotation * this._offset;

        // Add target's position to shifted-offset; assign result to camera's position
        this.transform.position = this.target.position + shiftedOffset;

        // Lookat target
        this.transform.LookAt(this.target);
    }
}
