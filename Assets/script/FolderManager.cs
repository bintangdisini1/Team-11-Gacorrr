using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FolderManager : MonoBehaviour
{
    [Header("UI Reference")]
    public Transform fileContainer;
    public GameObject filePrefab;
    public TMP_Text pathText;

    [Header("Popup Panel / Viewer")]
    public GameObject viewerPanel;        // Panel Pop-up Utama
    public TMP_Text viewerTitleText;      // (Opsional) Judul/Nama File di Pop-up
    public TMP_Text viewerTextContent;    // Teks Isi File
    public Image viewerImageContent;      // Gambar Isi File
    public GameObject targetItemNotice;   // (Opsional) Teks/Banner "Item Target Ditemukan!"

    [Header("Root Folder")]
    public FileItemData rootFolder;

    private Stack<FileItemData> historyStack = new Stack<FileItemData>();
    private FileItemData currentFolder;

    void Start()
    {
        if (rootFolder != null)
        {
            OpenFolder(rootFolder);
        }
        else
        {
            Debug.LogError("Root Folder belum dimasukkan ke Inspector!");
        }

        // Pastikan panel popup tersembunyi di awal game
        if (viewerPanel != null) viewerPanel.SetActive(false);
    }

    public void OpenFolder(FileItemData targetFolder)
    {
        if (targetFolder == null) return;

        if (currentFolder != targetFolder)
        {
            currentFolder = targetFolder;
            historyStack.Push(targetFolder);
        }

        RefreshDisplay();
    }

    public void OpenFileOrFolder(FileItemData item)
    {
        if (item == null) return;

        if (item.fileType == FileType.Folder)
        {
            OpenFolder(item);
        }
        else
        {
            // Buka Panel Pop-up saat File diklik
            DisplayFileContent(item);
        }
    }

    private void RefreshDisplay()
    {
        foreach (Transform child in fileContainer)
        {
            Destroy(child.gameObject);
        }

        if (currentFolder != null && currentFolder.subFiles != null)
        {
            foreach (var item in currentFolder.subFiles)
            {
                if (item == null) continue;

                GameObject newObj = Instantiate(filePrefab, fileContainer);

                if (newObj.TryGetComponent<FileItemUI>(out FileItemUI fileItemUI))
                {
                    fileItemUI.Setup(item, this);
                }
            }
        }

        UpdatePathText();
    }

    private void DisplayFileContent(FileItemData item)
    {
        // 1. Munculkan Panel Popup
        if (viewerPanel != null) viewerPanel.SetActive(true);

        // Reset tampilan isi
        if (viewerTextContent != null) viewerTextContent.gameObject.SetActive(false);
        if (viewerImageContent != null) viewerImageContent.gameObject.SetActive(false);
        if (targetItemNotice != null) targetItemNotice.SetActive(false);

        // Set Judul File
        if (viewerTitleText != null) viewerTitleText.text = item.fileName;

        // 2. Tampilkan Konten berdasarkan Tipe File
        if (item.fileType == FileType.Text && viewerTextContent != null)
        {
            viewerTextContent.gameObject.SetActive(true);
            viewerTextContent.text = item.textContent;
        }
        else if (item.fileType == FileType.Image && viewerImageContent != null)
        {
            viewerImageContent.gameObject.SetActive(true);
            viewerImageContent.sprite = item.imageContent;
        }

        // 3. Cek jika item yang dibuka adalah Target Item Misi
        if (item.isTargetItem)
        {
            if (targetItemNotice != null) targetItemNotice.SetActive(true);
            Debug.Log("SELAMAT! Kamu berhasil menemukan item rahasia!");
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    // Panggil fungsi ini via OnClick () tombol pembuka
    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    // Dipanggil oleh Tombol Close ("X") pada Panel Popup
    public void CloseViewer()
    {
        if (viewerPanel != null)
        {
            viewerPanel.SetActive(false);
        }
    }

    public void GoBack()
    {
        if (historyStack.Count > 1)
        {
            historyStack.Pop();
            currentFolder = historyStack.Peek();
            RefreshDisplay();
        }
    }

    private void UpdatePathText()
    {
        if (pathText == null) return;

        FileItemData[] folders = historyStack.ToArray();
        System.Array.Reverse(folders);

        string fullPath = "";
        for (int i = 0; i < folders.Length; i++)
        {
            fullPath += folders[i].fileName;
            if (i < folders.Length - 1) fullPath += "/";
        }

        pathText.text = fullPath;
    }
}