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

    // 메인메뉴로 가는 로직과 메인 메뉴로 갈 때 실행될 로직을 분류해야함 (인게임으로 가는것도 마찬가지)
    // ex) 메인 메뉴로 가는 로직(게임 매니저의 현재 씬 위치 변경, 이미 메인 메뉴면 실행x)
    // ex) 메인 메뉴로 갈 때 실행될 로직(ToyManager처럼 옵션 초기화 등)

    private void PauseMenu_OnMainMenuEnter(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.Status == SystemStatus.Ingame)
            StartCoroutine(StartMainMenu());
    }

    private void MainMenu_OnIngameEnter(object sender, System.EventArgs e)
    { 
        StartCoroutine(StartInGame());
    }

    public IEnumerator StartInGame()
    {
        // 인 게임으로 갈 때 실행될 로직
        if (GameManager.Instance.Status == SystemStatus.Ingame) yield break; // 이미 인게임 진입이라면 실행 막기
        
        // 인 게임으로 가는 로직
        GameManager.Instance.StatusChange(SystemStatus.Ingame);

        FadeScript.Instance.FadeOut(0.5f);
        yield return new WaitForSeconds(1.5f);

        // 인 게임 가는 로직
        UIManager.Instance.InitializePannel();
        UIManager.Instance.AddPanel(UIManager.Instance.PausePanel);
        SoundManager.instance.PlayBGM(GameplaySound.Main);

        LoadSceneManager.Instance.LoadScene(SceneName.Ingame); 

        yield return new WaitForSeconds(1.5f);
        FadeScript.Instance.FadeIn(0.5f);
    }

    public IEnumerator StartMainMenu()
    {
        // 메인 메뉴로 갈 때 실행될 로직
        if (GameManager.Instance.Status == SystemStatus.Lobby) yield break; // 이미 메인메뉴 진입이라면 실행 막기

        // 메인 메뉴로 가는 로직
        GameManager.Instance.StatusChange(SystemStatus.Lobby); // 단, 페이드 인아웃중 이러면 일시정지 시도가 가능하니 이 부분 막아야함
        ToyManager.Instance.InitToy();

        FadeScript.Instance.FadeOut(0.5f);
        yield return new WaitForSeconds(1.5f);

        UIManager.Instance.InitializePannel();
        UIManager.Instance.AddPanel(mainMenu);
        SoundManager.instance.StopGameBGM();
        LoadSceneManager.Instance.UnLoadScene(SceneName.Ingame);
        UIManager.Instance.TopPanelShow();

        yield return new WaitForSeconds(1.5f);
        FadeScript.Instance.FadeIn(0.5f);
    }
}
