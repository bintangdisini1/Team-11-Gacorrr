using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [Header("Quest Settings")]
    public int totalRequirement = 5; // Disesuaikan dengan CodePuzzleManager
    public float timeLimit = 120f;

    [Header("UI References")]
    public Text progressText;
    public Text timerText;
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Quest State")]
    public int currentProgress = 0; // Public agar bisa diakses langsung oleh CodePuzzleManager

    private float currentTime;
    private bool isQuestCompleted = false;
    private bool isTimerRunning = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentTime = timeLimit;
        isTimerRunning = true;
        UpdateQuestUI();
    }

    private void Update()
    {
        if (isTimerRunning && !isQuestCompleted)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                currentTime = 0;
                UpdateTimerUI();
                FailQuest();
            }
        }
    }

    // Mendukung pemanggilan AddProgress() tanpa parameter maupun dengan parameter (e.g. AddProgress(1))
    public void AddProgress(int amount = 1)
    {
        if (isQuestCompleted) return;

        currentProgress += amount;
        UpdateQuestUI();

        if (currentProgress >= totalRequirement)
        {
            CompleteQuest();
        }
    }

    // Method untuk memperbarui tampilan UI progress quest
    public void UpdateQuestUI()
    {
        if (progressText != null)
        {
            progressText.text = "Progress: " + currentProgress + " / " + totalRequirement;
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void CompleteQuest()
    {
        isQuestCompleted = true;
        isTimerRunning = false;

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

    public void FailQuest()
    {
        isQuestCompleted = false;
        isTimerRunning = false;

        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CloseCodePanel()
    {
    }

    public void CloseWirePanel()
    {
    }
}