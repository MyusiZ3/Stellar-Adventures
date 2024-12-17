using UnityEngine;

public class GyroCameraController : MonoBehaviour
{
    private Gyroscope gyro;
    private Quaternion rotation;

    public float sensitivity = 0.5f;  // Sensitivitas untuk kontrol touch/mouse
    private bool isMobile;

    // Start is called before the first frame update
    void Start()
    {
        // Menentukan apakah perangkat mendukung gyroscope
        isMobile = SystemInfo.supportsGyroscope;

        if (isMobile)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            // Jika tidak mendukung gyroscope, kita akan menggunakan input mouse/touch
            Debug.Log("Gyroscope not supported. Using touch/mouse input.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMobile)
        {
            // Gunakan data gyroscope untuk memutar kamera
            transform.rotation = gyro.attitude * rotation;
        }
        else
        {
            // Kontrol kamera dengan touch atau mouse
            if (Input.touchCount > 0)  // Untuk perangkat mobile dengan layar sentuh
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    Debug.Log("Touch Moved");  // Debugging log
                    float rotationX = touch.deltaPosition.x * sensitivity;
                    float rotationY = touch.deltaPosition.y * sensitivity;

                    transform.Rotate(Vector3.up, -rotationX, Space.World);  // Rotasi horisontal
                    transform.Rotate(Vector3.right, rotationY, Space.Self);  // Rotasi vertikal
                }
            }
            else if (Input.GetMouseButton(0))  // Untuk perangkat desktop dengan mouse
            {
                Debug.Log("Mouse Moved");  // Debugging log
                float rotationX = Input.GetAxis("Mouse X") * sensitivity;
                float rotationY = Input.GetAxis("Mouse Y") * sensitivity;

                transform.Rotate(Vector3.up, -rotationX, Space.World);  // Rotasi horisontal
                transform.Rotate(Vector3.right, rotationY, Space.Self);  // Rotasi vertikal
            }
        }
    }
}
