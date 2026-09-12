using UnityEngine;
using UnityEngine.Playables; // Wajib ditambahkan untuk Timeline

public class CreditManager : MonoBehaviour
{
    [Header("Panels Reference")]
    public GameObject creditsPanel;
    public GameObject settingsPanel;

    [Header("Timeline Reference")]
    public PlayableDirector creditDirector; // Drag GameObject PlayableDirector ke sini

    void OnEnable()
    {
        // Berlangganan event ketika Timeline berhenti / selesai
        if (creditDirector != null)
        {
            creditDirector.stopped += OnTimelineEnded;

            // Putar Timeline dari awal tiap kali panel aktif
            creditDirector.time = 0;
            creditDirector.Play();
        }
    }

    void OnDisable()
    {
        // Melepas event agar tidak memicu error / memory leak
        if (creditDirector != null)
        {
            creditDirector.stopped -= OnTimelineEnded;
        }
    }

    void Update()
    {
        // Jika player menekan ESC, langsung skip credit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCreditsAndOpenSettings();
        }
    }

    // Callback otomatis yang dipanggil persis saat Timeline selesai diputar
    private void OnTimelineEnded(PlayableDirector director)
    {
        if (director == creditDirector)
        {
            CloseCreditsAndOpenSettings();
        }
    }

    public void CloseCreditsAndOpenSettings()
    {
        // Stop Timeline jika dipotong di tengah jalan
        if (creditDirector != null)
        {
            creditDirector.Stop();
        }

        // Matikan panel Credit, aktifkan panel Settings
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }
}