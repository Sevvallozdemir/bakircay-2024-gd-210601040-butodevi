using UnityEngine;

public class CollisionResponse : MonoBehaviour
{
    public float pushForce = 10f; // �arp��ma kuvveti
    public float bounceMultiplier = 1.5f; // Z�plama kuvvet �arpan�
    public float speedIncreaseFactor = 1.2f; // �arp��ma sonras� h�z art���

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRb = collision.rigidbody;
        if (otherRb != null)
        {
            // �arp��ma y�n�
            Vector3 collisionDirection = collision.contacts[0].normal;

            // �tme kuvveti uygula
            otherRb.AddForce(-collisionDirection * pushForce, ForceMode.Impulse);

            // Z�plama ve h�zlanma etkisi
            Vector3 bounceVelocity = Vector3.Reflect(otherRb.linearVelocity, collisionDirection) * bounceMultiplier;
            otherRb.linearVelocity = bounceVelocity;

            // H�z art���
            otherRb.linearVelocity *= speedIncreaseFactor;
        }
    }
}
