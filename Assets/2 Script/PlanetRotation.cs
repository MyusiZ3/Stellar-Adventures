using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    // Rotasi Speed
    public float rotationSpeedX =  10F;
    public float rotationSpeedY = 10f;
    
    //Arah Rotasi
    public bool rotateUp = true;
    public bool rotateDown = true;

    //call once per frame
    void Update()
    {
        float rotationX = (rotateUp ? 1 : -1)*rotationSpeedX*Time.deltaTime;
        float rotationY = (rotateDown ? 1 : -1)*rotationSpeedY*Time.deltaTime;

        transform.Rotate(rotationX, rotationY,0);

    }


}
