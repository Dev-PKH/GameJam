using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SystemStatus
{
    Lobby,
    Ingame
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsPause { get; private set; } // 일시 정지 여부

    public SystemStatus Status { get; private set; } = SystemStatus.Lobby;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        if (Status == SystemStatus.Ingame) // 인 게임에서
        {
            if (Input.GetKeyDown(KeyCode.Escape)) // 정지 버튼을 누를 경우
            {
                if (IsPause) // 이미 활성화 된 경우 일 때
                {
                    if (UIManager.Instance.PannelCount() == 1) // 일시정지만 남은 경우
                    {
                        UIManager.Instance.TopPaneHide(); // 일시정지 비활성화
                        PauseChange(); // 일시정지 비활성화
                    }
                    else
                    {
                        UIManager.Instance.BackPannel(); // 현재 패널 비활성화 후 이전 패널 활성화
                    }
                }
                else
                {
                    UIManager.Instance.TopPanelShow(); // 현재 일시정지 화면 활성화.
                    PauseChange(); // 일시정지 활성화
                }
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (UIManager.Instance.PannelCount() > 1)
                {
                    UIManager.Instance.BackPannel();
                }
            }
        }
    }

    /// <summary>
    /// 로비 <-> 인게임 전환 용도
    /// </summary>
    public void StatusChange()
    {
        Status = Status == SystemStatus.Lobby ? SystemStatus.Ingame : SystemStatus.Lobby;
    }

    public void PauseChange()
    {
        IsPause = !IsPause;
    }
}
