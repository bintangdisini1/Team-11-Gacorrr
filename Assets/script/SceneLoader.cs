using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Fungsi ini akan dipanggil saat tombol ditekan
    public void PindahKeCredit()
    {
        // Pastikan nama scene di dalam tanda kutip sama persis dengan nama file scene Anda
        SceneManager.LoadScene("Credit Scene");
    }
}