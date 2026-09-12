using UnityEngine;

[CreateAssetMenu(fileName = "NewMissionItem", menuName = "File System/Mission Item")]
public class MissionItem : ScriptableObject
{
    public string itemID;          // Contoh: "KEY_RED", "EVIDENCE_01"
    public string itemName;        // Contoh: "Kunci Folder Admin"
    public Sprite itemIcon;
    [TextArea] public string description;
}