using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameStatus
{
    Select, Move
}

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance { get; private set; }

    // 행성 인덱스를 담은 변수가 필요함. (배열 bool or list contain)

    [Header("Views")]
    [SerializeField] private GameObject selectView;
    [SerializeField] private GameObject moveView;

    // 행성 정보
    public GameStatus status;
    public Planet[] planetPrefabs; // 행성 프리펩 종류들

    //public Planet curPlanet;
    public Planets curPlanet;
    public int curDistance; // 현재 남은 거리

    public int stepDistance; // 단계별 거리
    public int completeCount; // 거쳐간 행성횟수
    private const float stepSize = 0.1f; // 단계별 거리 증가 값

    public SpawnPlanet spawnPlanet;

    // 움직임 화면
    public MoveView moveViewObject;

    public TextMeshPro rollCountText;
    public int curRollCnt = 0; // 현재 던질 수 있는 주사위 수
    public int setRollCnt = 5; // 턴마다 던질 수 있는 주사위 수

    // 주사위 정보
    public Dice[] dices;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        EnterSelectPlanet();
        SelectRun();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Select 뷰 진입시 실행
    /// </summary>
    private void EnterSelectPlanet() // 행성 선택 진입
    {
        moveView.SetActive(false);
        rollCountText.gameObject.SetActive(false);
        moveViewObject.ClearList();
        DiceManager.Instance.SetDice(false);

        selectView.SetActive(true); // 진입 후 이벤트 뜨는거면 함수를 더 세부분리
    }

    /// <summary>
    /// SelectView를 실행
    /// </summary>
    private void SelectRun()
    {
        Debug.Log("주사위 총합 : " + GetDistance());

        int value = 0, min = 0;
        (value, min) = GetDistance();
        stepDistance = Mathf.RoundToInt(4f * value / 6f + 2.4f * Mathf.Sqrt(completeCount)) - min;

        Debug.Log("계산 값 : " + stepDistance);

        int cnt = Random.Range(2, 4); // 2개이상 3개 이하 생성

        spawnPlanet.SpawnPlanets(cnt, stepDistance, stepSize);
    }

    /// <summary>
    /// 도합과 최소값 반환
    /// </summary>
    /// <returns></returns>
    public (int,int) GetDistance()
    {
        int sumDistance = 0; // 주사위의 합
        int min = int.MaxValue;
        foreach(var dice in dices)
        {
            int num = dice.GetDistance();
            if (min > num) min = num;
            sumDistance += num;
        }

        return (sumDistance, min);
    }

    public void EnterMovePlanet() // 행성 이동 진입
    {
        selectView.SetActive(false);

        rollCountText.gameObject.SetActive(true);
        moveView.SetActive(true);
        curRollCnt = setRollCnt;
        rollCountText.text = curRollCnt.ToString();

        //Debug.Log("인덱스 체크 : " + (int)curPlanet.planetStatus);
        moveViewObject.PlanetSpawn(curDistance,(int)curPlanet, 1);
    }

    public void SetPlanet(Planet planet)
    {
        curPlanet = planet.planetStatus;

        // 행성 선택이 이동중이 아닐 경우
        if (status != GameStatus.Move)
        {
            curDistance = planet.distance;

            // 페이드 아웃 하고나서
            StartCoroutine(ChangeMove());
        }
        else
        {

        }
    }


    public IEnumerator ChangeMove()
    {
        // 페이드 아웃 실행
        status = GameStatus.Move;

        // 페이드 아웃 종료
        EnterMovePlanet();

        yield return null;

        // 페이드 인 실행


        DiceManager.Instance.SetDice(true);

        spawnPlanet.ClearList();
    }

    public IEnumerator ChangeSelect(bool checkMemorial)
    {
        // 페이드 아웃 실행
        status = GameStatus.Select;

        // 페이드 아웃 종료


        yield return null;

        // 페이드 인 실행

        if(checkMemorial)
        {
            Debug.Log("행성 도착 완료! 이제 보상 아이템 얻어야함");
        }

        // 페이드 아웃 실행

        // 강화(상점) 실행


        // 이벤트 다 끝나면 실행
        EnterSelectPlanet();

        yield return new WaitForSeconds(1f); // 1초 대기후 선택 행성 로직 실행

        SelectRun();
    }


    /// <summary>
    /// 다이스 굴리는 처리
    /// </summary>
    /// <param name="value"></param>
    public void DiceRoll(int value)
    {
        curRollCnt--;
        rollCountText.text = curRollCnt.ToString();

        curDistance -= value;
        if(curDistance <= 0 ) // 도착
        {
            completeCount++;
            SetDistance(0);
           
            StartCoroutine(ChangeSelect(true));
        }
        else if(curRollCnt == 0) // 도착못함
        {
            OutPlanet();
        }
        else // 이동
        {
            SetDistance(curDistance);
        }
    }    

    /// <summary>
    ///  거리 갱신
    /// </summary>
    /// <param name="value"></param>
    public void SetDistance(int value)
    {
        moveViewObject.planetList[0].SetDistance(value);
    }

    /// <summary>
    /// 주사위가 다 굴렸는데 도착못했는지 확인
    /// </summary>
    public void OutPlanet()
    {
        Debug.Log("주사위 다굴렸다.");
    }
}
