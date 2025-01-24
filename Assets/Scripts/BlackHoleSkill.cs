using UnityEngine;
using DG.Tweening;
using Match;
using UnityEngine.UI; // UI bileşenlerine erişmek için


public class BlackHoleSkill : MonoBehaviour
{
    public float pullDuration = 2f; // Çekim süresi
    public float pullRadius = 5f;  // Çekim alanı yarıçapı
    public Ease pullEase = Ease.InQuad; // Çekim eğrisi
    public GameObject blackHoleVisual; // Kara delik görseli (örneğin, bir model ya da sprite)


    public AudioClip blackHoleSound; // Kara delik sesi
    private AudioSource audioSource; // Ses kaynağı

    public Button blackHoleButton;



    public ItemSpawner itemSpawner;

    private bool isActivated = false; // Kara deliğin aktiflik durumu
    private void Start()
    {
        // Kara delik başlangıçta görünmez
        if (blackHoleVisual != null)
        {
            blackHoleVisual.SetActive(false);
        }

        // AudioSource bileşeni kontrolü ve atama
      audioSource = GetComponent<AudioSource>();
      if (audioSource == null)
     {
        audioSource = gameObject.AddComponent<AudioSource>();
     }

      // Kara delik sesini ayarla
      audioSource.clip = blackHoleSound;
    }

    public void ActivateBlackHole()
    {
        // Kara deliği görünür hale getir ve aktif hale getir
        if (blackHoleVisual != null)
        {
            blackHoleVisual.SetActive(true);
        }

        // Kara delik butonunu devre dışı bırak
      if (blackHoleButton != null)
      {
        blackHoleButton.interactable = false; // Butonu devre dışı bırak
      }

      // Kara delik sesini çal
       if (audioSource != null && blackHoleSound != null)
      {
        audioSource.Play();
      }

        isActivated = true;

        // Sahnedeki tüm uygun objeleri kara deliğe çek
        PullAllObjects();

        // Belirli bir süre sonra kara deliği devre dışı bırak
        Invoke(nameof(DeactivateBlackHole), pullDuration + 1f); // Çekim süresi + 1 saniye
         Invoke(nameof(EnableBlackHoleButton), 5f); // Butonu 5 saniye sonra etkinleştir
    }

    private void PullAllObjects()
    {
        // Sahnedeki tüm objeleri bul
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Moveable");

        foreach (GameObject obj in objects)
        {
            PullToBlackHole(obj);
        }


    }

    private void PullToBlackHole(GameObject obj)
    {
        // Obje kara deliğe doğru hareket eder
        obj.transform.DOMove(transform.position, pullDuration)
            .SetEase(pullEase)
            .OnComplete(() =>
            {
                // Obje çekim tamamlandığında yok edilir
                itemSpawner.ClearSpawnedObjects();
                
                 itemSpawner.SpawnObjects();
                

                // İsteğe bağlı: Yok edilme efekti
                CreateDestructionEffect();
            });
    }

    private void CreateDestructionEffect()
    {
        // İsteğe bağlı olarak patlama veya partikül efekti ekleyebilirsin
        Debug.Log("Bir obje yok edildi!");
    }

    private void DeactivateBlackHole()
    {
        // Kara deliği devre dışı bırak
        if (blackHoleVisual != null)
        {
            blackHoleVisual.SetActive(false);
        }

        isActivated = false;
        Debug.Log("Kara delik etkisi sona erdi.");
    }

    private void OnDrawGizmos()
    {
        // Çekim alanını görmek için sahnede bir çizim yapar
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }

    private void EnableBlackHoleButton()
      {
    if (blackHoleButton != null)
    {
        blackHoleButton.interactable = true; // Butonu tekrar etkinleştir
    }
      }
}
