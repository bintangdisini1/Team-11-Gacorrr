using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class WireNode : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Pengaturan Kabel")]
    public string wireID = "Red"; // ID kabel (misal: Red, Blue, Yellow)
    public RectTransform wireBody; // Drag & Drop badan kabel ke sini
    public Transform startPoint;  // Drag & Drop titik pangkal (Wire_Red_Start) ke sini

    private Vector3 initialPosition;
    private bool isConnected = false;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        initialPosition = transform.position;
        UpdateWireBody();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isConnected) return;

        // Mematikan raycast objek kabel ini saat ditarik 
        // agar kursor bisa mendeteksi objek WireTarget di bawahnya
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isConnected) return;

        // Ikuti posisi kursor/sentuhan jari
        transform.position = eventData.position;

        // Update panjang dan arah rotasi kabel
        UpdateWireBody();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isConnected) return;

        // Nyalakan kembali raycast setelah drag selesai
        canvasGroup.blocksRaycasts = true;

        // Cek objek UI di bawah kursor saat dilepas
        GameObject targetObj = eventData.pointerCurrentRaycast.gameObject;

        if (targetObj != null && targetObj.TryGetComponent<WireTarget>(out WireTarget target))
        {
            // Jika ID-nya cocok dengan target tujuan
            if (target.wireID == this.wireID)
            {
                // SNAP / TEMPELKAN otomatis ke posisi target
                transform.position = target.transform.position;
                isConnected = true;
                UpdateWireBody();
                Debug.Log("Kabel " + wireID + " Berhasil Terhubung!");

                // UPDATE PROGRESS QUEST
                if (QuestManager.Instance != null)
                {
                    QuestManager.Instance.AddProgress();
                }

                return;
            }
        }

        // Jika salah target atau dilepas di tempat kosong, kembali ke titik awal
        transform.position = initialPosition;
        UpdateWireBody();
    }

    // Fungsi untuk memanjangkan & memutar badan kabel ke arah ujung kabel
    void UpdateWireBody()
    {
        if (wireBody == null || startPoint == null) return;

        // Posisi kabel dari start ke head
        Vector3 startPos = startPoint.position;
        Vector3 endPos = transform.position;

        // Atur posisi badan kabel di titik start
        wireBody.position = startPos;

        // Hitung jarak (panjang) dan arah rotasi
        Vector3 direction = endPos - startPos;
        float distance = direction.magnitude;

        // Set ukuran panjang badan kabel
        wireBody.sizeDelta = new Vector2(distance / wireBody.lossyScale.x, wireBody.sizeDelta.y);

        // Putar badan kabel mengarah ke ujung yang ditarik
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        wireBody.rotation = Quaternion.Euler(0, 0, angle);
    }
}