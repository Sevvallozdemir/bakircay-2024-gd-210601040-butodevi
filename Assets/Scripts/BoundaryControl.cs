using UnityEngine;

public class BoundaryControl : MonoBehaviour
{
    public float minX = -4.54f; // X ekseni minimum sýnýr
    public float maxX = 4.54f;  // X ekseni maksimum sýnýr
    public float minY = -3.34f; // Y ekseni minimum sýnýr
    public float maxY = 4.80f; // Y ekseni MAX sýnýr
    public float minZ = 0.55f;
    public float maxZ = 20.39f;
    private void Update()
    {
        // Mevcut pozisyonu al
        Vector3 position = transform.position;

        // X ekseninde sýnýrlandýr
        position.x = Mathf.Clamp(position.x, minX, maxX);

        // Y ekseninde sýnýrlandýr
        position.y = Mathf.Clamp(position.y, minY, maxY);

        // Z ekseninde sýnýrlandýr
        position.z = Mathf.Clamp(position.z, minZ, maxZ);

        // Pozisyonu güncelle
        transform.position = position;
    }
}
