using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Matthias : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    private void Start()
    {
        button.onClick.AddListener(Middle);
    }
    private void Middle()
    {
        Transform parent1 = transform.parent;
        Transform sfx1 = parent1.Find("Bullseye");
        AudioSource thesfx = sfx1.GetComponent<AudioSource>();
        Transform parent2 = transform.parent;
        Transform sfx2 = parent2.Find("Iriwara");
        AudioSource thesfx2 = sfx2.GetComponent<AudioSource>();

        int number = Random.Range(-20, 78);
        if (number < 15)
        {
            thesfx.PlayOneShot(thesfx.clip, thesfx.volume);
        }
        else
        {
            thesfx2.PlayOneShot(thesfx2.clip, thesfx2.volume);
        }

    }
}
