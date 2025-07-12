using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 로비(메인 메뉴)인지 인 게임인지 확인용 변수
public enum SystemStatus
{ 
    Lobby,
    Ingame
}

public class TempUIGameManager : MonoBehaviour
{
    public static TempUIGameManager Instance { get; private set; }

    public SystemStatus Status { get; private set; } = SystemStatus.Lobby;

    public bool isPause { get; private set; } // 일시 정지 확인

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // InputSystem 만들면 그거로 대체
        if (TempUIGameManager.Instance.Status == SystemStatus.Ingame) // 인 게임에서
        {
            if (Input.GetKeyDown(KeyCode.Escape)) // 정지 버튼을 누를 경우
            {
                if(TempUIGameManager.Instance.isPause) // 이미 활성화 된 경우 일 때
                {
                    if(UIManager.Instance.PannelCount() == 1) // 일시정지만 남은 경우
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
                if(UIManager.Instance.PannelCount() > 1)
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
        isPause = !isPause;
    }
}
