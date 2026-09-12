using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    [Header("Task System")]
    // Dictionary untuk menghubungkan nama file dengan teks UI-nya
    public List<TMP_Text> taskTextUIList;
    private Dictionary<string, TMP_Text> taskDictionary = new Dictionary<string, TMP_Text>();

    private int remainingFiles;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Mendaftarkan teks UI ke dictionary berdasarkan teks aslinya
        foreach (TMP_Text textUI in taskTextUIList)
        {
            string key = textUI.text.Trim();
            if (!taskDictionary.ContainsKey(key))
            {
                taskDictionary.Add(key, textUI);
            }
        }

        remainingFiles = taskDictionary.Count;
    }

    public void OnFileFound(string fileName)
    {
        if (taskDictionary.ContainsKey(fileName))
        {
            // Coret/ubah warna teks di UI jika ditemukan
            TMP_Text textUI = taskDictionary[fileName];
            textUI.fontStyle = FontStyles.Strikethrough;
            textUI.color = Color.gray;

            // Hapus dari dictionary agar tidak bisa diklik dua kali
            taskDictionary.Remove(fileName);
            remainingFiles--;

            // Cek kondisi menang
            if (remainingFiles <= 0)
            {
                Debug.Log("Semua file tersembunyi berhasil ditemukan!");
                OnAllFilesFound();
            }
        }
    }

    private void OnAllFilesFound()
    {
        // Tambahkan logika saat game selesai (pindah scene/munculkan pop-up)
    }
}