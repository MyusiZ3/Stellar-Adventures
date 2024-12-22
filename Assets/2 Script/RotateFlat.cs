using UnityEngine;

public class RotateFlat : MonoBehaviour
{
    // Kecepatan rotasi dalam derajat per detik
    public float rotationSpeed = 30f;

    // Poros rotasi, default adalah sumbu Y
    public Vector3 rotationAxis = Vector3.up;

    // Arah rotasi, 1 untuk searah jarum jam, -1 untuk berlawanan jarum jam
    public int rotationDirection = 1;

    void Update()
    {
        // Rotasi objek secara terus-menerus pada poros yang ditentukan
        transform.Rotate(rotationAxis, rotationSpeed * rotationDirection * Time.deltaTime);
    }
}
