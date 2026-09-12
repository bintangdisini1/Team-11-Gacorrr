using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Wajib untuk mengontrol komponen Image
using TMPro;

// Struktur data untuk satu baris dialog lengkap
[System.Serializable]
public struct DialogueLine
{
    public string speakerName;     // Nama pembicara
    public Sprite speakerSprite;   // Gambar/Sprite karakter pembicara
    [TextArea(2, 5)]
    public string sentence;        // Isi teks dialog
}

public class SimpleDialogue : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI nameText;     // Slot untuk UI Teks Nama
    public TextMeshProUGUI dialogueText; // Slot untuk UI Teks Dialog
    public Image characterImage;         // Slot untuk UI Image Karakter

    [Header("Pengaturan Typing")]
    public float typingSpeed = 0.04f;

    [Header("Daftar Dialog")]
    public DialogueLine[] lines; // Daftar percakapan (Nama, Sprite, Teks)

    private int index = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        // Pastikan UI Image mati dulu jika belum ada dialog
        if (characterImage != null && characterImage.sprite == null)
        {
            characterImage.gameObject.SetActive(false);
        }

        StartDialogue();
    }

    void Update()
    {
        // Lanjut dengan Klik Kiri, Spasi, atau Enter
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            NextLine();
        }
    }

    void StartDialogue()
    {
        index = 0;
        if (lines != null && lines.Length > 0)
        {
            ShowLine();
        }
    }

    public void NextLine()
    {
        if (lines == null || lines.Length == 0) return;

        // FITUR SKIP: Jika sedang mengetik, langsung tamatkan teks
        if (isTyping)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            dialogueText.text = lines[index].sentence;
            isTyping = false;
            return;
        }

        // Lanjut ke indeks berikutnya
        index++;

        if (index < lines.Length)
        {
            ShowLine();
        }
        else
        {
            // Dialog Selesai
            if (nameText != null) nameText.text = "";
            dialogueText.text = "[Selesai]";

            // Opsional: Sembunyikan gambar karakter saat selesai
            if (characterImage != null) characterImage.gameObject.SetActive(false);

            Debug.Log("Dialog Selesai!");
        }
    }

    void ShowLine()
    {
        DialogueLine currentLine = lines[index];

        // 1. Ganti Nama
        if (nameText != null)
        {
            nameText.text = currentLine.speakerName;
        }

        // 2. Ganti Gambar Karakter
        if (characterImage != null)
        {
            if (currentLine.speakerSprite != null)
            {
                characterImage.sprite = currentLine.speakerSprite;
                characterImage.gameObject.SetActive(true); // Munculkan jika ada sprite

                // Opsional: Jika ingin gambar karakter tidak mencong (tetap aspek rasio)
                // characterImage.preserveAspect = true; 
            }
            else
            {
                // Jika di elemen dialog ini spritenya dikosongkan, sembunyikan UI Image
                characterImage.gameObject.SetActive(false);
            }
        }

        // 3. Mulai Ketik Isi Dialog
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeLine(currentLine.sentence));
    }

    IEnumerator TypeLine(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in sentence.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}