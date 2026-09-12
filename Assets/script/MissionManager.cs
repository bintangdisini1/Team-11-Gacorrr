using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }

    [Header("Mission Objective")]
    public string requiredItemID = "EVIDENCE_01"; // ID Item untuk menang
    public GameObject winScreenPanel;

    [Header("Inventory UI")]
    public Transform inventoryContainer;
    public GameObject inventoryItemPrefab;
    public TextMeshProUGUI statusText;

    private List<MissionItem> collectedItems = new List<MissionItem>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void CollectItem(MissionItem item)
    {
        if (collectedItems.Contains(item)) return;

        collectedItems.Add(item);
        UpdateInventoryUI();

        // Cek syarat kemenangan
        CheckMissionComplete();
    }

    private void UpdateInventoryUI()
    {
        // Bersihkan UI lama
        foreach (Transform child in inventoryContainer)
        {
            Destroy(child.gameObject);
        }

        // Tampilkan item yang baru dikumpulkan
        foreach (MissionItem item in collectedItems)
        {
            GameObject obj = Instantiate(inventoryItemPrefab, inventoryContainer);
            // Setup ikon/teks item di UI (sesuai prefab kamu)
        }

        if (statusText != null)
            statusText.text = $"Item Ditemukan: {collectedItems.Count}";
    }

    private void CheckMissionComplete()
    {
        foreach (MissionItem item in collectedItems)
        {
            if (item.itemID == requiredItemID)
            {
                // Misi Selesai!
                if (winScreenPanel != null) winScreenPanel.SetActive(true);
                Debug.Log("Misi Selesai! Kamu menemukan item kunci.");
                break;
            }
        }
    }
}