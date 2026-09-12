using UnityEngine;

public class PointItem : MonoBehaviour
{
    [Header("Nilai Poin")]
    public int pointsGiven = 10; // Poin yang didapat dari objek ini

    private void OnMouseDown()
    {
        CollectItem();
    }

    public void CollectItem()
    {
        if (PointMissionManager.Instance != null)
        {
            PointMissionManager.Instance.AddPoints(pointsGiven);
        }

        // Hapus objek dari scene
        gameObject.SetActive(false);
    }
}