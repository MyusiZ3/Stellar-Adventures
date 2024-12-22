using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickCameraControl : MonoBehaviour
{
    public float moveSpeed = 10f; // Kecepatan gerakan kamera
    public float rotationSpeed = 100f; // Kecepatan rotasi kamera
    public VariableJoystick variableJoystick; // Referensi joystick

    private void Update()
    {
        // Input joystick untuk gerakan (Horizontal dan Vertical)
        float moveHorizontal = variableJoystick.Horizontal;
        float moveVertical = variableJoystick.Vertical;

        // Gerakan kamera berdasarkan input joystick
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed * Time.deltaTime;

        // Gerakkan kamera dengan mengubah posisinya
        transform.Translate(movement, Space.World);

        // Rotasi kamera berdasarkan joystick (menggunakan horizontal untuk rotasi kiri/kanan)
        float rotateHorizontal = variableJoystick.Horizontal;
        float rotateVertical = variableJoystick.Vertical;

        // Rotasi horizontal (gerakan kiri/kanan)
        if (rotateHorizontal != 0)
        {
            transform.Rotate(Vector3.up, rotateHorizontal * rotationSpeed * Time.deltaTime);
        }

        // Rotasi vertikal (gerakan atas/bawah)
        if (rotateVertical != 0)
        {
            transform.Rotate(Vector3.right, -rotateVertical * rotationSpeed * Time.deltaTime);
        }
    }
}
