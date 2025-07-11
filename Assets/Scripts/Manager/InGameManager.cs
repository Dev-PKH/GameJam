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
        stepDistance = Mathf.RoundToInt(4 * GetDistance() / 6 + 2.4f * Mathf.Sqrt(planetCount));

        int cnt = Random.Range(2, 4); // 2개이상 3개 이하 생성

        spawnPlanet.SpawnPlanets(cnt, stepDistance, stepSize);
    }

    public int GetDistance()
    {
        int sumDistance = 0; // 주사위의 합
        foreach(var dice in dices)
        {
            sumDistance += dice.GetDistance();
        }

        return sumDistance;
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
