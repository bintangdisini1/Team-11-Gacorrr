using UnityEngine;

public class PointMissionData : MonoBehaviour
{
    public string missionID;
    public string title;
    [TextArea] public string description;

    [Header("Goal Poin")]
    public int targetPoints = 100;

    [System.NonSerialized] public int currentPoints = 0;
    [System.NonSerialized] public bool isCompleted = false;

    public void ResetProgress()
    {
        currentPoints = 0;
        isCompleted = false;
    }
}