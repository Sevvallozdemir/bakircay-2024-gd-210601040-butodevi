using UnityEngine;

public class UFOController : MonoBehaviour
{
    public Animator animator; // UFO animatörü
    //public GameObject spawnButton; // Spawn button objesi

     public GameObject ufoObject;  // UFO objesi (görünürlüğü kontrol edilecek)

    private void Start()
    {
        // Başlangıçta UFO ve spawn button'ı gizle
        ufoObject.SetActive(false);
        //spawnButton.SetActive(false);
    }
    public void PlayUFOAnimation()
    {
        /*ufoObject.SetActive(true); // UFO'yu görünür yap
        animator.Play("UFOFly"); // UFOFly animasyonunu oynat
        Invoke(nameof(HideUFO), 3f); // 3 saniye sonra UFO'yu gizle*/
    }
 private void HideUFO()
    {
        ufoObject.SetActive(false); // UFO'yu sahneden kaldır
    }

    /*    private void ShowSpawnButton()
    {
        spawnButton.SetActive(true); // Butonu aktif et
    }*/

}
