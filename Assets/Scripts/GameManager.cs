using Match.Skills;
using Match.View;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Match
{
    public class GameManager : MonoBehaviour
    {
        // Singleton instance
        public static GameManager Instance;

        public ItemSpawner itemSpawner;
        public UIController uiController;
        public WindSkill windSkill;

        public Button resetButton; // Reset Button referansı

        public UFOController ufoController;  // UFOController referansı

           public Button AsteroidButton;  // UI'deki Ateş Topu Butonu
        public GameObject AsteroidPrefab;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            // UI'i başlat
            uiController.Initialize(itemSpawner);

            // Sahnedeki objeleri başlat
            itemSpawner.SpawnObjects();

            // WindSkill'i başlat
            windSkill.Initialize(itemSpawner);

            
            resetButton.onClick.AddListener(ResetGame);

            //AsteroidButton.onClick.AddListener(SpawnAsteroid);  // Butona tıklanınca SpawnFireball fonksiyonunu çalıştır
        }


       public void ResetGame()
{
    Debug.Log("Reset işlemi başlatılıyor...");

    // 1. UFO animasyonunu çalıştır
    ufoController.PlayUFOAnimation();

    // 2. Skoru sıfırla
    uiController.ResetScore();

    // 3. Cisimleri yeniden spawn et
    StartCoroutine(ClearAndSpawnItems());

    // 4. Butonu gizle ve animasyon sırasında bir süre beklet
    resetButton.gameObject.SetActive(false);
    Invoke("ReactivateResetButton", 3f); // 3 saniye sonra yeniden aktif olur
}

private IEnumerator ClearAndSpawnItems()
        {
            itemSpawner.ClearSpawnedObjects(0.5f); // Hızlı temizleme için 0.5 saniye beklet
            yield return new WaitForSeconds(0.5f);  // Temizleme süresini bekle
            yield return new WaitForSeconds(3f);    // UFO animasyonu süresi
            itemSpawner.SpawnObjects();    
        }


 private void ReactivateResetButton()
  {
    resetButton.gameObject.SetActive(true);
}

public void SpawnAsteroid()
    {
        // Spawn pozisyonunu belirliyoruz, (0, 30, 0) noktasına spawn edilecek
     Vector3 spawnPosition = new Vector3(0f, 30f, 0f);  // Yüksek bir yerden başlayacak şekilde belirliyoruz (y ekseni 30 olarak ayarlandı)

     // Asteroid prefab'ını spawn ediyoruz
     GameObject asteroid = Instantiate(AsteroidPrefab, spawnPosition, Quaternion.identity);

     // Rigidbody bileşenini alıyoruz
     Rigidbody rb = asteroid.GetComponent<Rigidbody>();

     // Eğer Rigidbody varsa
     if (rb != null)
     {
        // (0, 0, 0) noktasına doğru büyük kuvvet uyguluyoruz
        Vector3 forceDirection = (Vector3.zero - spawnPosition).normalized;  // (0, 0, 0) noktasına gidecek yön
        float forceMagnitude = 120f;  // Kuvvetin büyüklüğünü ayarlıyoruz (değeri değiştirebilirsiniz)

        // Kuvveti uyguluyoruz
        rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
     }

     // Collider bileşeni olan diğer objelerle çarpışmalarını algılamak için OnCollisionEnter fonksiyonu ekleyelim
     asteroid.AddComponent<AsteroidCollision>();  // Asteroidin çarpışma etkilerini yönetmek için bir bileşen ekliyoruz

    }
 public class AsteroidCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Çarpışan objeyi kontrol ediyoruz
        if (collision.gameObject.CompareTag("Moveable"))
        {
            // Çarpan objeye büyük bir kuvvet uygulayarak dağıtıyoruz
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Çarpma yönü ve kuvveti belirliyoruz
                Vector3 forceDirection = (collision.transform.position - transform.position).normalized;
                float forceMagnitude = 10f;  // Kuvvetin büyüklüğünü buradan ayarlayabilirsiniz
                rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
            }

        }
    }
}
    
  private Vector3 SpawnPosition()
{
    float xPos = 0;  
    float yPos = 30;
    return new Vector3(xPos, yPos, 0f);  
}

  }
}

