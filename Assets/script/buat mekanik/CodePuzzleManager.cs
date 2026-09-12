using System.Collections.Generic;
using UnityEngine;

public class CodePuzzleManager : MonoBehaviour
{
    public static CodePuzzleManager Instance { get; private set; }

    [Header("Puzzle Configuration")]
    public List<CodeSlot> puzzleSlots = new List<CodeSlot>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (QuestManager.Instance != null && puzzleSlots.Count > 0)
        {
            QuestManager.Instance.totalRequirement = puzzleSlots.Count;
            QuestManager.Instance.currentProgress = 0;
            QuestManager.Instance.UpdateQuestUI();
        }
    }

    public void CheckPuzzleStatus()
    {
        int correctCount = 0;

        foreach (CodeSlot slot in puzzleSlots)
        {
            if (slot != null && slot.IsCorrect)
            {
                correctCount++;
            }
        }

        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.currentProgress = correctCount;
            QuestManager.Instance.UpdateQuestUI();

            // Jika semua slot terisi dengan benar, panggil penyelelesaian quest
            if (correctCount >= puzzleSlots.Count && puzzleSlots.Count > 0)
            {
                QuestManager.Instance.AddProgress();
            }
        }
    }
}