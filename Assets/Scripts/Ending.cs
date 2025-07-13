using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Ending : MonoBehaviour
{
    public MainMenu mainMenu;

    public Button button;
    public TextMeshProUGUI text;

    public float fadeDuration = 3f;

    private void Awake()
    {
        mainMenu = FindObjectOfType<MainMenu>();
        button.interactable = false;
        text.color = new Color(1, 1, 1, 0);
    }

    void Start()
    {
        StartCoroutine(FadeInTextWithDelay());
        SoundManager.instance.StopGameBGMWithFade();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (timer > maxTimer) return; 
        timer += Time.deltaTime;
        button.image.color = new Color(1, 1, 1, timer / maxTimer);*/
    }

    public IEnumerator StartFade()
    {
        FadeScript.Instance.FadeOut(0.5f);
        text.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(1.5f);

        UIManager.Instance.InitializePannel();
        UIManager.Instance.AddPanel(mainMenu);
        SoundManager.instance.StopGameBGM();
        GameManager.Instance.StatusChange();
        UIManager.Instance.TopPanelShow();
        LoadSceneManager.Instance.UnLoadScene(SceneName.GameClear);

        FadeScript.Instance.FadeIn(0.5f);
    }

    public void MainMenuEnter()
    {
        StartCoroutine(StartFade());
    }

    private IEnumerator FadeInTextWithDelay()
    {
        yield return new WaitForSeconds(1f); // 시작 후 1초 대기

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            text.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        button.interactable = true; // 완전히 보이면 클릭 가능하게
    }
}
