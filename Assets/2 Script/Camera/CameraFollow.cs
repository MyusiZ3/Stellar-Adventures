using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f; // Kecepatan mengikuti target
    public Transform target;       // Target yang diikuti oleh kamera

    private Vector3 initialOffset; // Menyimpan offset awal kamera terhadap target

    void Start()
    {
        // Menyimpan offset awal berdasarkan posisi target dan kamera
        initialOffset = transform.position - target.position;
    }

    void Update()
    {
        // Menetapkan posisi baru kamera dengan mempertahankan offset
        Vector3 newPos = target.position + initialOffset; 
        newPos.y = transform.position.y; // Menjaga posisi vertikal kamera tetap sama

        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
