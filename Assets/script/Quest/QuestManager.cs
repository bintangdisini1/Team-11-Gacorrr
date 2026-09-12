using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("Pengaturan Quest")]
    public string questTitle = "Perbaiki Kabel Listrik";
    public int totalRequirement = 4;
    private int currentProgress = 0;
    private bool isQuestCompleted = false;

    [Header("UI Quest")]
    public TextMeshProUGUI questText;
    public GameObject wireGamePanel;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
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

    void UpdateQuestUI()
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

    void CompleteQuest()
    {
        isQuestCompleted = true;
        Debug.Log("🎉 QUEST SELESAI!");
        UpdateQuestUI();
        Invoke(nameof(CloseWirePanel), 0.5f);
    }

    void CloseWirePanel()
    {
        if (wireGamePanel != null)
        {
            wireGamePanel.SetActive(false);
        }
    }
}