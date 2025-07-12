using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameEnter : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;

    private void Start()
    {
        mainMenu.OnIngameEnter += MainMenu_OnIngameEnter;
    }

    private void MainMenu_OnIngameEnter(object sender, System.EventArgs e)
    {
        UIManager.Instance.InitializePannel();
        UIManager.Instance.AddPanel(UIManager.Instance.PausePanel);
        SoundManager.instance.PlayBGM(GameplaySound.Battle);
        //LoadSceneManager.Instance.ChangeScene(SceneName.Ingame, SceneName.MainMenu);
        LoadSceneManager.Instance.LoadScene(SceneName.Ingame); // 현재는 1이 Ingme Test임
        // 추후에는 이 스크립트는 메인 메뉴 전용 이벤트임으로 LoadSceneManager.Instance.ChangeScene(SceneName.Ingame, SceneName.MainMenu)
        // 로 MainMenu 씬 언로드하고 인게임 씬 로드해야함
        TempUIGameManager.Instance.StatusChange();
    }
}
