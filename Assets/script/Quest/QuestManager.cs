using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("Pengaturan Quest")]
    public string questTitle = "Lengkapi Kode C#";
    public int totalRequirement = 4;
    public int currentProgress = 0;
    public bool isQuestCompleted = false;

    [Header("UI Quest")]
    public TextMeshProUGUI questText;
    public GameObject codePuzzlePanel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateQuestUI();
    }

    public void AddProgress()
    {
        if (isQuestCompleted) return;

        currentProgress++;
        UpdateQuestUI();

        if (currentProgress >= totalRequirement)
        {
            CompleteQuest();
        }
    }

    public void RemoveProgress()
    {
        if (isQuestCompleted) return;

        currentProgress = Mathf.Max(0, currentProgress - 1);
        UpdateQuestUI();
    }

    public void UpdateQuestUI()
    {
        if (questText != null)
        {
            if (isQuestCompleted)
            {
                questText.text = $"<s>{questTitle} (Selesai)</s>";
                questText.color = Color.green;
            }
            else
            {
                questText.text = $"{questTitle} ({currentProgress}/{totalRequirement})";
            }
        }
    }

    private void CompleteQuest()
    {
        isQuestCompleted = true;
        Debug.Log("🎉 PUZZLE KODE SELESAI!");
        UpdateQuestUI();
        Invoke(nameof(CloseCodePanel), 0.5f);
    }

    private void CloseCodePanel()
    {
        if (codePuzzlePanel != null)
        {
            codePuzzlePanel.SetActive(false);
        }
    }
}