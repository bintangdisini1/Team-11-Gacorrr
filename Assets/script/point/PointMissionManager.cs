using UnityEngine;
using TMPro;

public class PointMissionManager : MonoBehaviour
{
    public static PointMissionManager Instance;

    [Header("Active Mission")]
    public PointMissionData activeMission;

    [Header("UI References")]
    public TMP_Text missionTitleText;
    public TMP_Text pointProgressText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (activeMission != null)
        {
            activeMission.ResetProgress();
            UpdateUI();
        }
    }

    // Panggil fungsi ini dari objek/aksi mana pun yang memberikan poin
    public void AddPoints(int amount)
    {
        if (activeMission == null || activeMission.isCompleted) return;

        activeMission.currentPoints += amount;

        // Cek jika poin mencapai target
        if (activeMission.currentPoints >= activeMission.targetPoints)
        {
            activeMission.currentPoints = activeMission.targetPoints;
            activeMission.isCompleted = true;
            CompleteMission();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (missionTitleText != null)
            missionTitleText.text = activeMission.title;

        if (pointProgressText != null)
        {
            if (activeMission.isCompleted)
                pointProgressText.text = $"Poin: {activeMission.currentPoints} / {activeMission.targetPoints} (<b>SELESAI!</b>)";
            else
                pointProgressText.text = $"Poin: {activeMission.currentPoints} / {activeMission.targetPoints}";
        }
    }

    private void CompleteMission()
    {
        Debug.Log($"Misi '{activeMission.title}' Selesai dengan total poin {activeMission.currentPoints}!");
    }
}