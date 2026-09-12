using UnityEngine;
using UnityEngine.UI;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        Transform parent = transform.parent;
        Transform sfx = parent.Find("SFX");
        AudioSource thesfx = sfx.GetComponent<AudioSource>();
        thesfx.volume = volumeSlider.value;
        Save();
    }
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("sfxVolume", volumeSlider.value);
    }
}
