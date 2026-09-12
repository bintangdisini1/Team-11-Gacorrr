using UnityEngine;
using UnityEngine.UI;

public class NotepadItemItem : MonoBehaviour
{
    [Header("UI Windows & Panels")]
    public GameObject notepadWindow;      // Panel Window Notepad
    public GameObject taskCompletePanel;  // Pop-up "Misi Selesai" / Win Screen

    [Header("Item Settings")]
    public Button itemButton;             // Tombol gambar item di dalam Notepad

    private void Start()
    {
        // Pastikan Panel Misi Selesai mati di awal
        if (taskCompletePanel != null)
            taskCompletePanel.SetActive(false);

        // Menghubungkan fungsi klik tombol item secara otomatis
        if (itemButton != null)
        {
            itemButton.onClick.AddListener(OnItemClicked);
        }
    }

    // Dipanggil saat ikon Notepad di dalam Folder diklik
    public void OpenNotepad()
    {
        if (notepadWindow != null)
        {
            notepadWindow.SetActive(true);
            notepadWindow.transform.SetAsLastSibling(); // Bawa window ke tumpukan paling depan
        }
    }

    // Dipanggil saat gambar ITEM di dalam Notepad diklik oleh pemain
    private void OnItemClicked()
    {
        Debug.Log("Item di dalam Notepad berhasil ditekan!");

        if (taskCompletePanel != null)
        {
            taskCompletePanel.SetActive(true);
            taskCompletePanel.transform.SetAsLastSibling();
        }
    }
}