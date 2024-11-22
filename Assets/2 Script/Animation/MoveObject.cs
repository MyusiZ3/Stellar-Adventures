using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Transform targetPosition; // Posisi target ke mana objek akan berpindah
    public float moveSpeed = 5f;     // Kecepatan perpindahan

    private bool shouldMove = false;
    private bool moveToStart = false;
    private Vector3 startPosition;  // Posisi awal objek

    void Start()
    {
        // Simpan posisi awal objek saat game dimulai
        startPosition = transform.position;
    }

    void Update()
    {
        // Jika tombol untuk bergerak ke target ditekan
        if (shouldMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);

            // Hentikan pergerakan jika sudah sampai ke target
            if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
            {
                shouldMove = false;
            }
        }

        // Jika tombol untuk kembali ke posisi awal ditekan
        if (moveToStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

            // Hentikan pergerakan jika sudah kembali ke posisi awal
            if (Vector3.Distance(transform.position, startPosition) < 0.01f)
            {
                moveToStart = false;
            }
        }
    }

    // Fungsi untuk mulai memindahkan objek ke posisi target
    public void StartMove()
    {
        shouldMove = true;
        moveToStart = false; // Pastikan hanya bergerak ke target, bukan ke posisi awal
    }

    // Fungsi untuk mengembalikan objek ke posisi awal
    public void ReturnToStart()
    {
        moveToStart = true;
        shouldMove = false; // Pastikan hanya bergerak ke posisi awal, bukan ke target
    }
}
