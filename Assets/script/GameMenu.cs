using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject creditsMenuUI;
    public GameObject joystick; // drag joystick kamu ke sini (opsional)

    private bool isPaused = false;

    void Start()
    {
        // Memastikan semua UI panel tersembunyi saat game dimulai
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (settingsMenuUI != null) settingsMenuUI.SetActive(false);
        if (creditsMenuUI != null) creditsMenuUI.SetActive(false);
    }

    void Update()
    {
        // Tombol pause (PC)
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            // Jika menu Credits sedang terbuka, tekan Escape/P akan menutup Credits dulu
            if (creditsMenuUI != null && creditsMenuUI.activeSelf)
            {
                CloseCredits();
            }
            // Jika menu Settings sedang terbuka, tekan Escape/P akan menutup Settings dulu
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
        // Aktifkan game kembali
        Time.timeScale = 1f;
        isPaused = false;

        // Matikan semua UI panel
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (settingsMenuUI != null) settingsMenuUI.SetActive(false);
        if (creditsMenuUI != null) creditsMenuUI.SetActive(false);

        // Reset input mobile
        Input.ResetInputAxes();

        // Reset EventSystem (biar touch/click balik normal)
        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);

        // Refresh joystick
        if (joystick != null)
        {
            joystick.SetActive(false);
            joystick.SetActive(true);
        }

        // Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Audio
        AudioListener.pause = false;
    }

    public void Pause()
    {
        // Pause game
        Time.timeScale = 0f;
        isPaused = true;

        // Tampilkan UI pause & sembunyikan sub-menu lainnya
        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);
        if (settingsMenuUI != null) settingsMenuUI.SetActive(false);
        if (creditsMenuUI != null) creditsMenuUI.SetActive(false);

        // Cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Audio
        AudioListener.pause = true;
    }

    // ==========================================
    // ⚙️ SETTINGS SYSTEM
    // ==========================================

    public void OpenSettings()
    {
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
    // 📜 CREDITS SYSTEM
    // ==========================================

    public void OpenCredits()
    {
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (settingsMenuUI != null) settingsMenuUI.SetActive(false);
        if (creditsMenuUI != null) creditsMenuUI.SetActive(true);
    }

    public void CloseCredits()
    {
        if (creditsMenuUI != null) creditsMenuUI.SetActive(false);
        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);
    }

    // ==========================================

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("MAINMENU");
    }
}