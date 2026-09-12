using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFileItemData", menuName = "File System/File Item Data")]
public class FileItemData : ScriptableObject
{
    [Header("General Info")]
    public string fileName = "New Item";
    public FileType fileType = FileType.Text;
    public Sprite icon;

    [Header("Folder Content (Hanya diisi jika FileType == Folder)")]
    public List<FileItemData> subFiles = new List<FileItemData>();

    [Header("File Content (Hanya diisi jika FileType == Text/Image)")]
    [TextArea(5, 10)] public string textContent;
    public Sprite imageContent;

    [Header("Mission Target")]
    public bool isTargetItem = false; // Tandai true jika ini item kunci yang dicari
}