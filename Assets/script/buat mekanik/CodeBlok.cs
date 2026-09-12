using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(CanvasGroup))]
public class CodeBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Block Data")]
    public string codeId;

    [HideInInspector] public Transform parentAfterDrag;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private Canvas mainCanvas;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        mainCanvas = GetComponentInParent<Canvas>();
        if (mainCanvas != null && !mainCanvas.isRootCanvas)
        {
            mainCanvas = mainCanvas.rootCanvas;
        }

        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Jika blok sebelumnya ada di dalam slot, kosongkan slot tersebut
        CodeSlot previousSlot = transform.parent.GetComponent<CodeSlot>();
        if (previousSlot != null)
        {
            previousSlot.ClearSlot();
        }

        parentAfterDrag = transform.parent;
        transform.SetParent(mainCanvas.transform, true);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        transform.SetParent(parentAfterDrag);
        transform.localPosition = Vector3.zero;

        // Pastikan jika di-drop ke CodeSlot, set currentBlock pada slot tersebut
        CodeSlot newSlot = parentAfterDrag.GetComponent<CodeSlot>();
        if (newSlot != null)
        {
            newSlot.currentBlock = transform;
        }

        // Jalankan pengecekan puzzle & update quest
        if (CodePuzzleManager.Instance != null)
        {
            CodePuzzleManager.Instance.CheckPuzzleStatus();
        }
    }

    public void ResetToOriginalParent()
    {
        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;
    }
}