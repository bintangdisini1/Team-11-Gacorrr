using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FileItemUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;

    private FileItemData currentData;
    private FolderManager manager;

    public void Setup(FileItemData data, FolderManager folderManager)
    {
        currentData = data;
        manager = folderManager;

        if (nameText != null)
            nameText.text = data.fileName;

        if (iconImage != null && data.icon != null)
            iconImage.sprite = data.icon;
    }

    public void OnClick()
    {
        if (manager != null && currentData != null)
        {
            manager.OpenFileOrFolder(currentData);
        }
    }
}