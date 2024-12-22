using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;

    // Set the gravity scale to 0 to make the object float
    public bool useGravity = false;

    void Start()
    {
        // Nonaktifkan gravitasi agar objek tidak jatuh
        if (useGravity)
        {
            rb.useGravity = true;
        }
        else
        {
            rb.useGravity = false; // Matikan gravitasi untuk objek yang melayang
        }
    }

    public void FixedUpdate()
    {
        // Dapatkan input dari joystick
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;

        // Menambahkan gaya pada objek untuk gerakan horizontal dan vertikal
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}
