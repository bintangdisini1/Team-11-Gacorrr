using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
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
        Transform bgm = parent.Find("BGM");
        AudioSource themusic = bgm.GetComponent<AudioSource>();
        themusic.volume = volumeSlider.value;
        Save();
    }
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
