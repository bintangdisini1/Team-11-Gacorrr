using UnityEngine;
using UnityEngine.EventSystems;

public class CodeSlot : MonoBehaviour, IDropHandler
{
    [Header("Slot Settings")]
    public string correctCodeId;
    public Transform currentBlock;

    public bool IsCorrect
    {
        get
        {
            if (currentBlock == null) return false;
            CodeBlock block = currentBlock.GetComponent<CodeBlock>();
            return block != null && block.codeId == correctCodeId;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObj = eventData.pointerDrag;
        if (droppedObj == null) return;

        CodeBlock block = droppedObj.GetComponent<CodeBlock>();
        if (block != null)
        {
            // Jika slot sudah ada isinya, kembalikan blok lama ke parent asal
            if (currentBlock != null && currentBlock != block.transform)
            {
                CodeBlock existingBlock = currentBlock.GetComponent<CodeBlock>();
                if (existingBlock != null)
                {
                    existingBlock.ResetToOriginalParent();
                }
            }

            // Tetapkan blok baru ke slot ini
            block.parentAfterDrag = transform;
            currentBlock = block.transform;
        }
    }

    // Panggil ini saat blok dilepas/ditarik dari slot
    public void ClearSlot()
    {
        currentBlock = null;
    }
}