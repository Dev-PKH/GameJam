using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : UISelector
{
    /*[Header("버튼")]
    [SerializeField] private Button gameplayButton;
    [SerializeField] private Button keyBindingButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button LanguageButton;
    */

    private void Start()
    {
        //UIManager.Instance.AddPanel(this);
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {

        }*/
    }

    public void ResumeButton()
    {
        //UIManager.Instance.ChangePanel(UIManager.Instance.gameplayPanel);
        Debug.Log("계속하기");
    }

    public void GiveUpButton()
    {
        Debug.Log("포기하기");
    }

    public void SettingButton()
    {
        UIManager.Instance.ChangePanel(UIManager.Instance.SettingPanel);
        Debug.Log("셋팅");
    }

    public void MainMenuButton()
    {
        Debug.Log("메인 메뉴");
    }
}
