using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Panggil fungsi ini saat tombol 'Play' ditekan
    public void PlayGame()
    {
        // Mengubah scene ke scene berikutnya di Build Settings (biasanya scene game utama)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Atau bisa panggil pakai nama scene-nya langsung:
        // SceneManager.LoadScene("NamaSceneGameKamu");
    }

    // Panggil fungsi ini jika ada panel/pop-up Settings
    public void OpenSettings()
    {
        Debug.Log("Menu Settings Dibuka");
        // Tambahkan logika untuk menampilkan panel settings di sini
    }

    // Panggil fungsi ini saat tombol 'Quit' ditekan
    public void QuitGame()
    {
        Debug.Log("Game Keluar");

        // Keluar dari game saat di-build (.exe / .apk)
        Application.Quit();
    }
}