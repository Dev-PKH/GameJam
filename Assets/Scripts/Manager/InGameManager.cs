using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus
{
    Select, Move
}

public enum Toys
{
    cicada, slinky, yoyo, fidget,
    puzzle, kite, blade, flipper,
    roulette, tamagochi, doll, horse,
    gongii, ballon, snake, icecream
}

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance { get; private set; }

    // 행성 인덱스를 담은 변수가 필요함. (배열 bool or list contain)

    [Header("Views")]
    [SerializeField] private GameObject selectView;
    [SerializeField] private GameObject moveView;
    //[SerializeField] private GameObject shopView;

    // 행성 정보
    public GameStatus status;
    public Planet[] planetPrefabs; // 행성 프리펩 종류들

    public Planets curPlanetStatus;
    public Toys curToysStatus;

    public int curDistance; // 현재 남은 거리

    public int stepDistance; // 단계별 거리
    public int completeCount; // 거쳐간 행성횟수
    private const float stepSize = 0.1f; // 단계별 거리 증가 값

    public SpawnPlanet spawnPlanet;

    public Sprite outPlanet; // 외딴섬 이미지

    // 움직임 화면
    public MoveView moveViewObject;
    public SpriteRenderer carRenderer;
    public SpriteRenderer planetRenderer;

    [Header("Status")]
    public int curRollCnt = 0; // 현재 던질 수 있는 주사위 수
    public int setRollCnt = 5; // 턴마다 던질 수 있는 주사위 수
    public int money = 5;
    

    public TextMeshPro rollCountText;
    public TextMeshPro moneyText;
    public const int completeMoney = 2; // 행성 도달 완료

    // 주사위 정보
    public Dice[] dices;
    public DiceChance[] dChances;
    public bool isDefence = false;


    // 이벤트
    public EventController eventController;
    public GameObject Description;

    public Sprite[] eventSprite;
    public int limitDistance = 0;
    public int NoneAdvanture = 0;
    public bool eventDice = false;

    public bool isUp; // 주사위가 몇 이상인지
    public int eventDiceNum; // 이벤트 주사위 몇
    public int eventDiceDistance; // 이벤트 주사위 이동거리

    //상점
    public Toys currentToy;
    public bool[] hasToys = new bool[16];
    public Sprite[] eyeValues;
    public GetDice[] diceShop;
    public GetDice selectedShop;
    public Button exitShopButton;
    public Button normalShopButton;
    public Button specialShopButton;
    public TextMeshProUGUI normalShopButtonText;
    public TextMeshProUGUI specialShopButtonText;
    public int curDiceIndex = 0; // 현재 선택된 눈 인덱스

    public const int specialValue = 3;
    public const int normalValue = 1;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        moneyText.text = money.ToString();
        //curPlanetStatus = (Planets)UnityEngine.Random.Range(0, planetPrefabs.Length);
        //planetRenderer.sprite = planetPrefabs[(int)curPlanetStatus].spriteRender.sprite;

    }
    void Start()
    {
        ExitMovePlanet();
        selectView.SetActive(true);
        SelectRun();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 움직임 종료
    /// </summary>
    private void ExitMovePlanet() // 행성 선택 진입
    {
        moveView.SetActive(false);
        //rollCountText.gameObject.SetActive(false);
        foreach (var dChance in dChances)
        {
            dChance.gameObject.SetActive(false);
        }
        
        moveViewObject.ClearList();
        DiceManager.Instance.SetDice(false);
    }

    /// <summary>
    /// SelectView를 실행
    /// </summary>
    private void SelectRun()
    {
        Debug.Log("주사위 총합 : " + GetDistance());

        int value = 0, min = 0;
        (value, min) = GetDistance();
        stepDistance = Mathf.RoundToInt(3.5f * value / 6f + 2.4f * Mathf.Sqrt(completeCount)) - min;

        Debug.Log("계산 값 : " + stepDistance);

        int cnt = UnityEngine.Random.Range(2, 4); // 2개이상 3개 이하 생성

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
        //carRenderer.sprite = eventSprite[0];
        selectView.SetActive(false);

        curRollCnt = setRollCnt;
        //rollCountText.gameObject.SetActive(true);
        rollCountText.text = curRollCnt.ToString();
        
        foreach (var dChance in dChances)
        {
            dChance.gameObject.SetActive(true);
            dChance.UpdateExistence();
        }
        moveView.SetActive(true);
        
        //Debug.Log("인덱스 체크 : " + (int)curPlanet.planetStatus);
        moveViewObject.PlanetSpawn(curDistance,(int)curPlanetStatus, 1);
    }

    public void SetPlanet(Planet planet)
    {
        //curPlanet = planet.planetStatus;
        curPlanetStatus = planet.planetStatus;
        curToysStatus = planet.planetToys;

        // 행성 선택이 이동중이 아닐 경우
        if (status != GameStatus.Move)
        {
            curDistance = planet.distance;
            isDefence = false;

            // 페이드 아웃 하고나서
            StartCoroutine(ChangeMove());
        }
    }


    public IEnumerator ChangeMove()
    {
        // 페이드 아웃 실행
        SoundManager.instance.PlaySFX(SFXSound.Booting);
        FadeScript.Instance.FadeOut(0.5f);
        yield return new WaitForSeconds(1f); // 0.5초 대기후 드라이브 화면 전환

        status = GameStatus.Move;

        EnterMovePlanet();

        yield return null;

        DiceManager.Instance.SetDice(true);
        spawnPlanet.ClearList();

        // 페이드 인 실행
        FadeScript.Instance.FadeIn(0.5f);
    }

    /// <summary>
    /// 왜딴섬에 감
    /// </summary>
    /// <returns></returns>
    public IEnumerator ChangeOut()
    {
        // 페이드 아웃 실행
        FadeScript.Instance.FadeOut(0.5f);
        yield return new WaitForSeconds(1f); // 0.5초 대기후 드라이브 화면 전환

        List<int> lostToyList = new List<int>();

        if (ToyManager.Instance.toyCount > 0)
        {
            for (int i = 0; i < 16; i++)
            {
                if (ToyManager.Instance.checkToy[i])
                {
                    lostToyList.Add(i);
                    Debug.Log("탈락후보 현재 인덱스 : " + i);
                }
            }
            int index = UnityEngine.Random.Range(0, lostToyList.Count);
            ToyManager.Instance.LostToy(lostToyList[index]);

            completeCount -= 2;
            if (completeCount < 0) completeCount = 0;
        }
        ExitMovePlanet();

        // 페이드 인 실행
        FadeScript.Instance.FadeIn(0.5f);

        selectView.SetActive(true);
        carRenderer.sprite = eventSprite[4]; // 차 교체
        planetRenderer.sprite = outPlanet;
        status = GameStatus.Select;

        yield return new WaitForSeconds(1f);
        //yield return new WaitForSeconds(1f); // 1초 대기후 선택 행성 로직 실행

        SelectRun();
    }

    public IEnumerator ChangeShop()
    {
        // 페이드 아웃 진행

        yield return new WaitForSeconds(1f);

        ExitMovePlanet();

        Debug.Log("행성 도착 완료! 이제 보상 아이템 얻어야함");
        
        ToyManager.Instance.GetToy((int)curPlanetStatus);

        if (ToyManager.Instance.toyCount >= 16) yield break;

        FadeScript.Instance.FadeOut(0.5f);
        yield return new WaitForSeconds(3f);
        FadeScript.Instance.FadeIn(0.5f);
        Description.SetActive(true);

        exitShopButton.gameObject.SetActive(true);

        foreach (var shop in diceShop)
        {
            shop.gameObject.SetActive(true);
            shop.SetDice();
        }

        carRenderer.sprite = eventSprite[0]; // 차 교체
        //selectView.SetActive(true);
        //StartCoroutine(ChangeSelect());
    }

    public IEnumerator ChangeSelect()
    {
        // 페이드 아웃 실행
        status = GameStatus.Select;
        FadeScript.Instance.FadeOut(0.5f);


        yield return new WaitForSeconds(1f); // 1초 대기 후 선택 행성 로직 실행
        Description.SetActive(false);



        exitShopButton.gameObject.SetActive(false);

        foreach (var shop in diceShop)
        {
            shop.gameObject.SetActive(false);
        }
        normalShopButton.gameObject.SetActive(false);
        specialShopButton.gameObject.SetActive(false);
        selectedShop.gameObject.SetActive(false);


        yield return new WaitForSeconds(1f);

        // 페이드 아웃 종료
        FadeScript.Instance.FadeIn(0.5f);

        selectView.SetActive(true);
        planetRenderer.sprite = planetPrefabs[(int)curPlanetStatus].spriteRender.sprite;


        // 이벤트 다 끝나면 실행
        SelectRun();
    }


    /// <summary>
    /// 다이스 굴리는 처리
    /// </summary>
    /// <param name="value"></param>
    public void DiceRoll(int value)
    {
        rollCountText.text = curRollCnt.ToString();
        if (curRollCnt == 0) isDefence = true;

        if (limitDistance != 0)
        {
            Debug.Log("현재 limit와 value값 : " + limitDistance + "/ " + value);
            value = value > limitDistance ? limitDistance : value;
            limitDistance = 0;
        }

        if (value >= curDistance) isDefence = true;

        UpdateDistance(-value);
    }    


    /// <summary>
    /// 값을 업데이트함
    /// </summary>
    /// <param name="value"></param>
    public void UpdateDistance(int value)
    {
        curDistance += value;
        if(curDistance <= 0)
        {
            completeCount++;
            moveViewObject.planetList[0]?.SetDistance(0);
            UpdateMoney(completeMoney + curRollCnt);

            //StartCoroutine(ChangeSelect(true)); // 상점으로 바꾸자
            StartCoroutine(ChangeShop());
        }
        else if(curRollCnt == 0)
        {
            moveViewObject.planetList[0]?.SetDistance(curDistance);
            OutPlanet();
        }
        else
        {
            moveViewObject.planetList[0]?.SetDistance(curDistance);
            if(!eventDice) CheckEvent();
        }
    }

    /// <summary>
    /// 주사위가 다 굴렸는데 도착못했는지 확인
    /// </summary>
    public void OutPlanet()
    {
        StartCoroutine(ChangeOut());
    }

    /// <summary>
    /// 매개변수만큼 돈이 업데이트
    /// </summary>
    public void UpdateMoney(int value)
    {
        money += value;
        if (money < 0) money = 0;
        
        moneyText.text = money.ToString();
    }

    /// <summary>
    ///  이벤트 체크 및 실행
    /// </summary>
    public void CheckEvent()
    {
        DiceManager.Instance.SetDice(false);

        // 이벤트 실행
        if (NoneAdvanture > 0)
        {
            NoneAdvanture--;
            RunEvent();
        }
        else if(UnityEngine.Random.value <= 0.9f)
        {
            RunEvent();
        }
        else
        {
            DiceManager.Instance.SetDice(true);
        }
        /*else
        {
            Debug.Log("탐험");
        }*/
    }

    public void RunEvent()
    {
        eventController.eventManager.gameObject.SetActive(true);
        eventController.TriggerRandomEvent();
        SoundManager.instance.PlaySFX(SFXSound.Event);
        //Description.SetActive(true);
    }

    public void ExitEvent()
    {
        eventController.eventManager.gameObject.SetActive(false);
        //Description.SetActive(false);
        DiceManager.Instance.SetDice(true);
    }

    public void ExitRollEvent()
    {
        isUp = false;
        eventDiceNum = 0;
        eventDiceDistance = 0;
        eventDice = false;

        DiceManager.Instance.SetDice(true);
    }

    public void EventRoll(int chcek, int limit, int dist)
    {
        isUp = chcek == 1 ? true : false;
        eventDiceNum = limit;
        eventDiceDistance = dist;

        eventDice = true;
        DiceManager.Instance.diceSlot.SetActive(true);
        DiceManager.Instance.RollDice();
        eventController.eventManager.gameObject.SetActive(false);
    }

    public void UpdateCarRender(int index)
    {
        carRenderer.sprite = eventSprite[index];
    }

    /// <summary>
    /// 상점 버튼 활성화 및 가격 표시 (주사위와 해당 눈을 받음)
    /// </summary>
    /// <param name="index"></param>
    public void SetShopButton(int diceIndex, int eyeIndex)
    {
        curDiceIndex = eyeIndex;

        specialShopButton.gameObject.SetActive(true);
        specialShopButtonText.text = specialValue.ToString();

        if (dices[diceIndex].eyesStatus[eyeIndex] == EyesStatus.basic)
        {
            if (dices[diceIndex].diceColor[eyeIndex] == DiceColor.red)
            {
                normalShopButton.gameObject.SetActive(false);
            }
            else
            {
                normalShopButton.gameObject.SetActive(true);
                normalShopButtonText.text = ((int)dices[diceIndex].diceColor[eyeIndex] + normalValue).ToString();
            }

        }
        else
        {
            normalShopButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 몇번째 주사위인지를 파악하여 눈을 구매
    /// </summary>
    /// <param name="index"></param>
    public void BuyNormalShopEyes(int diceIndex)
    {
        money -= GetNoramlEyesValue();
        if (money < 0) money = 0;
        moneyText.text = money.ToString();
        dices[diceIndex].diceColor[curDiceIndex]++;
        dices[diceIndex].InitEyes();
        diceShop[diceIndex].CheckDice(diceIndex); //
        diceShop[diceIndex].UpgradeViewButton(curDiceIndex);
        selectedShop.CheckDice(diceIndex); //
        selectedShop.UpgradeViewButton(curDiceIndex);
        //ExitShop();
    }

    public int GetNoramlEyesValue()
    {
       return int.Parse(normalShopButtonText.text);
    }

    public void BuySpecialShopEyes(int diceIndex)
    {
        money -= specialValue;
        if (money < 0) money = 0; 
        moneyText.text = money.ToString();
        dices[diceIndex].eyesStatus[curDiceIndex] = (EyesStatus)UnityEngine.Random.Range(0, eyeValues.Length);
        dices[diceIndex].InitEyes();
        diceShop[diceIndex].CheckDice(diceIndex); //
        diceShop[diceIndex].UpgradeViewButton(curDiceIndex);
        selectedShop.CheckDice(diceIndex); //
        selectedShop.UpgradeViewButton(curDiceIndex);
        //ExitShop();
    }

    public void ExitShop()
    {
        if (status == GameStatus.Select) return;
        if (GameManager.Instance.IsPause) return;
        StartCoroutine(ChangeSelect());
    }

    public void GameClear()
    {
        StartCoroutine(ChangeGameClear());
    }

    public IEnumerator ChangeGameClear()
    {
        FadeScript.Instance.FadeOut(0.5f);

        yield return new WaitForSeconds(1f);
        LoadSceneManager.Instance.ChangeScene(SceneName.GameClear, SceneName.Ingame);

        FadeScript.Instance.FadeIn(0.5f);
    }
}
