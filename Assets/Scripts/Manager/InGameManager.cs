using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    Select, Move
}

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance { get; private set; }

    // 행성 인덱스를 담은 변수가 필요함. (배열 bool or list contain)

    // 행성 정보
    public GameStatus status;
    public Planet[] planets; // 행성 프리펩 종류들

    public Planet curPlanet;

    public int stepDistance; // 단계별 거리
    public int planetCount; // 거쳐간 행성횟수
    private const float stepSize = 0.2f; // 단계별 거리 증가 값

    public SpawnPlanet spawnPlanet;

    // 주사위 정보
    public Dice[] dices;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        EnterSelectePlanet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterSelectePlanet() // 행성 선택 진입
    {
        Debug.Log("주사위 총합 : " + GetDistance());

        int value = 0, min = 0;
        (value, min) = GetDistance();
        stepDistance = Mathf.RoundToInt(4f * value / 6f + 2.4f * Mathf.Sqrt(planetCount)) - min;

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

    }

    public void SetPlanet(Planet planet)
    {
        curPlanet = planet;

        // 행성 선택이 이동중이 아닐 경우
        if (status != GameStatus.Move)
        {
            status = GameStatus.Move;

        }
        else
        {

        }
    }

}
