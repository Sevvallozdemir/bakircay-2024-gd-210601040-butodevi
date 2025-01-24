using UnityEngine;

public class InitialVelocity : MonoBehaviour
{
    public float forceMagnitude = 10f; // Atýþ kuvveti
    public float throwAngle = 15f; // Atýþ açýsý (derece)

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Açý ve yön
            float angleInRadians = throwAngle * Mathf.Deg2Rad;
            Vector3 forceDirection = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0);

            // Kuvvet uygula
            rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
        }
    }
}
