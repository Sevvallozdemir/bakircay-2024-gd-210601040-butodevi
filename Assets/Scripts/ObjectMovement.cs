using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float maxSpeed = 20f; // Maksimum h�z s�n�r�
    public float gravityMultiplier = 2f; // Yer�ekimi �arpan� (2 kat art�r�lm�� yer�ekimi)

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Varsay�lan yer�ekimini devre d��� b�rak
    }

    void Update()
    {
        // Klavye girdisi al
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Hareket y�n�
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Kuvvet uygula
        rb.AddForce(movement * moveSpeed);

        // Maksimum h�z� kontrol et
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    void FixedUpdate()
    {
        // Manuel yer�ekimi uygula
        Vector3 gravity = Physics.gravity * gravityMultiplier;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
