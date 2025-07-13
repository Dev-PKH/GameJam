using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameEnter : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private PauseMenu pauseMenu;

    private void Start()
    {
        mainMenu.OnIngameEnter += MainMenu_OnIngameEnter;
        pauseMenu.OnMainMenuEnter += PauseMenu_OnMainMenuEnter;
    }

    private void PauseMenu_OnMainMenuEnter(object sender, System.EventArgs e)
    {
        
        StartCoroutine(StartMainMenu());
    }

    private void MainMenu_OnIngameEnter(object sender, System.EventArgs e)
    {
        StartCoroutine(StartInGame());

        /*
        UIManager.Instance.InitializePannel();
        UIManager.Instance.AddPanel(UIManager.Instance.PausePanel);
        SoundManager.instance.PlayBGM(GameplaySound.Battle);
        //LoadSceneManager.Instance.ChangeScene(SceneName.Ingame, SceneName.MainMenu);
        LoadSceneManager.Instance.LoadScene(SceneName.Ingame); // 현재는 1이 Ingme Test임
        // 추후에는 이 스크립트는 메인 메뉴 전용 이벤트임으로 LoadSceneManager.Instance.ChangeScene(SceneName.Ingame, SceneName.MainMenu)
        // 로 MainMenu 씬 언로드하고 인게임 씬 로드해야함
        GameManager.Instance.StatusChange();*/
    }

    public IEnumerator StartInGame()
    {
        FadeScript.Instance.FadeOut(0.5f);
        yield return new WaitForSeconds(1.5f);

        UIManager.Instance.InitializePannel();
        UIManager.Instance.AddPanel(UIManager.Instance.PausePanel);
        SoundManager.instance.PlayBGM(GameplaySound.Main);
        //LoadSceneManager.Instance.ChangeScene(SceneName.Ingame, SceneName.MainMenu);
        LoadSceneManager.Instance.LoadScene(SceneName.Ingame); // 현재는 1이 Ingme Test임
        // 추후에는 이 스크립트는 메인 메뉴 전용 이벤트임으로 LoadSceneManager.Instance.ChangeScene(SceneName.Ingame, SceneName.MainMenu)
        // 로 MainMenu 씬 언로드하고 인게임 씬 로드해야함
        GameManager.Instance.StatusChange();

        yield return new WaitForSeconds(1.5f);
        FadeScript.Instance.FadeIn(0.5f);
    }

    public IEnumerator StartMainMenu()
    {
        FadeScript.Instance.FadeOut(0.5f);
        yield return new WaitForSeconds(1.5f);

        UIManager.Instance.InitializePannel();
        UIManager.Instance.AddPanel(mainMenu);
        SoundManager.instance.StopGameBGM();
        LoadSceneManager.Instance.UnLoadScene(SceneName.Ingame);
        GameManager.Instance.StatusChange();
        UIManager.Instance.TopPanelShow();

        yield return new WaitForSeconds(1.5f);
        FadeScript.Instance.FadeIn(0.5f);
    }
}
