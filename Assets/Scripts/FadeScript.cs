using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeScript : MonoBehaviour
{
    public static FadeScript Instance;

    private Image fadeImage;
    private Coroutine currentFade;

    public bool isFadeCheck { get; private set; } // 페이드 아웃 진행중인지 체크 변수

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            fadeImage = GetComponent<Image>();
        }
    }

    public void FadeIn(float duration = 1f)
    {
        StartFade(1f, 0f, duration);
        isFadeCheck = false;
    }

    public void FadeOut(float duration = 1f)
    {
        if (isFadeCheck) return;

        if(GameManager.Instance.IsPause)
        {
            UIManager.Instance.TopPaneHide();
            GameManager.Instance.PauseChange(false);
        }

        isFadeCheck = true;
        
        StartFade(0f, 1f, duration);
    }

    private void StartFade(float from, float to, float duration)
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(FadeRoutine(from, to, duration));
    }

    private IEnumerator FadeRoutine(float from, float to, float duration)
    {
        float time = 0f;
        Color color = fadeImage.color;

        while (time < duration)
        {
            float t = time / duration;
            float a = Mathf.Lerp(from, to, t);
            fadeImage.color = new Color(color.r, color.g, color.b, a);
            time += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, to);
        currentFade = null;
    }
}
