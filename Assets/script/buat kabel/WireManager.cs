using UnityEngine;

public class WireManager : MonoBehaviour
{
    public static WireManager instance;
    public Wire[] allWires;

    void Awake()
    {
        instance = this;
    }

    public void CheckCompletion()
    {
        foreach (Wire w in allWires)
        {
            // Cek status sambungan kabel
        }
        Debug.Log("Semua kabel berhasil terhubung!");
    }
}