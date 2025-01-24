using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Match.Skills;
using Match.View;


public class Asteroid : MonoBehaviour
{

 

     public float speed = 5f;  // Ateş topunun düşme hızı
    public float range = 10f; // Düşüş mesafesi
    // Start is called before the first frame update
    void Start()
    {
             Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.down * speed;
        }

        // Belirli bir süre sonra ateş topunu yok et
        Destroy(gameObject, range / speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Cisimlere çarptığında, cisimleri dağıt
        if (collision.gameObject.CompareTag("Moveable")) // Cisimlerin "Item" etiketi olduğunu varsayalım
        {
            // Cismi bir yöne fırlat
            Rigidbody itemRb = collision.gameObject.GetComponent<Rigidbody>();
            if (itemRb != null)
            {
                itemRb.AddForce(Random.insideUnitSphere * 5f, ForceMode.Impulse);
            }

            // Ateş topu çarpıştıktan sonra yok edilir
            Destroy(gameObject);
        }
    }

    
}
