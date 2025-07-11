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
    public Planet curPlanet;

    public int planetCount; // 거쳐간 행성횟수

    // 주사위 정보
    public Dice[] dices;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlanet(Planet planet)
    {
        // 행성 선택이 이동중이 아닐 경우
        if (status != GameStatus.Move)
        {
            status = GameStatus.Move;

        }
        else
        {

        }

        curPlanet = planet;
    }
}
