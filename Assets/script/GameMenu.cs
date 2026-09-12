using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Playables; // 👈 Wajib ditambahkan untuk Timeline

public class GameMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject creditsMenuUI;
    public GameObject joystick; // drag joystick kamu ke sini (opsional)

    [Header("Timeline Credit")]
    public PlayableDirector creditDirector; // 👈 Drag GameObject yang punya PlayableDirector ke sini

    private bool isPaused = false;

    void Start()
    {
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (settingsMenuUI != null) settingsMenuUI.SetActive(false);
        if (creditsMenuUI != null) creditsMenuUI.SetActive(false);
    }

    void OnEnable()
    {
        // Berlangganan event ketika Timeline berhenti/selesai
        if (creditDirector != null)
        {
            creditDirector.stopped += OnCreditTimelineEnded;
        }
    }

    void OnDisable()
    {
        // Melepas langganan event agar tidak memicu memory leak
        if (creditDirector != null)
        {
            creditDirector.stopped -= OnCreditTimelineEnded;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (creditsMenuUI != null && creditsMenuUI.activeSelf)
            {
                CloseCredits();
            }
            else if (settingsMenuUI != null && settingsMenuUI.activeSelf)
            {
                CloseSettings();
            }
            else
            {
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("level1");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;

        // Stop Timeline jika dipotong di tengah jalan
        if (creditDirector != null) creditDirector.Stop();

        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (settingsMenuUI != null) settingsMenuUI.SetActive(false);
        if (creditsMenuUI != null) creditsMenuUI.SetActive(false);

        Input.ResetInputAxes();

        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);

        if (joystick != null)
        {
            joystick.SetActive(false);
            joystick.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        AudioListener.pause = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;

        if (creditDirector != null) creditDirector.Stop();

        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);
        if (settingsMenuUI != null) settingsMenuUI.SetActive(false);
        if (creditsMenuUI != null) creditsMenuUI.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        AudioListener.pause = true;
    }

    // ==========================================
    // ⚙️ SETTINGS SYSTEM
    // ==========================================

    public void OpenSettings()
    {
        if (creditDirector != null) creditDirector.Stop();

        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (settingsMenuUI != null) settingsMenuUI.SetActive(true);
        if (creditsMenuUI != null) creditsMenuUI.SetActive(false);
    }

    public void CloseSettings()
    {
        if (settingsMenuUI != null) settingsMenuUI.SetActive(false);
        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);
    }

    // ==========================================
    // 📜 CREDITS SYSTEM (TIMELINE)
    // ==========================================

    public void OpenCredits()
    {
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (settingsMenuUI != null) settingsMenuUI.SetActive(false);
        if (creditsMenuUI != null) creditsMenuUI.SetActive(true);

        // Putar Timeline animasi credit dari awal
        if (creditDirector != null)
        {
            creditDirector.time = 0;
            creditDirector.Play();
        }
    }

    public void CloseCredits()
    {
        // Stop Timeline jika player skip/menutup manual
        if (creditDirector != null) creditDirector.Stop();

        if (creditsMenuUI != null) creditsMenuUI.SetActive(false);
        if (settingsMenuUI != null) settingsMenuUI.SetActive(true);
    }

    // Callback otomatis yang dipanggil saat Timeline selesai diputar
    private void OnCreditTimelineEnded(PlayableDirector director)
    {
        if (director == creditDirector)
        {
            CloseCredits(); // Pindah otomatis ke panel Settings
        }
    }

    // ==========================================

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("MAINMENU");
    }
}