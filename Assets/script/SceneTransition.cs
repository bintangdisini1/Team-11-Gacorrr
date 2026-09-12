using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;

    public CanvasGroup fadeCanvas;
    public float fadeDuration = 0.5f; // 🔥 dipercepat

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        StartCoroutine(FadeIn());
    }

    public void LoadNextScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeIn()
    {
        if (fadeCanvas != null)
            fadeCanvas.gameObject.SetActive(true);

        float time = 0;
        fadeCanvas.alpha = 1;

        while (time < fadeDuration)
        {
            fadeCanvas.alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        fadeCanvas.alpha = 0;
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        if (fadeCanvas != null)
            fadeCanvas.gameObject.SetActive(true);

        float time = 0;

        while (time < fadeDuration)
        {
            fadeCanvas.alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        fadeCanvas.alpha = 1;

        // 🔥 HAPUS delay lama (biar cepat)
        // yield return new WaitForSeconds(0.5f);

        // 🔥 GANTI ke async biar tidak freeze
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        while (!op.isDone)
        {
            yield return null;
        }
    }
}