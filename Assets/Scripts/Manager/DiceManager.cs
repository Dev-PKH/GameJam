using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    //SpriteRenderer
    public static DiceManager Instance { get; private set; }

    // Dice
    public Dice[] dices;
    public DiceChance[] dChances;
    public GameObject diceSlot;
    public GameObject diceButton;

    public bool isDiceRoll { get; private set; } // 다이스를 진행하는지
    public bool isSlowing { get; private set; } // 다이스가 느려지는지
    public float diceSpeed { get; private set; }
    public float diceTimer { get; private set; }
    public float diceFastTimer = 7f;
    public float diceSlowTimer = 3f;

    [Header("다이스 속도")]
    [SerializeField] private float maxSpeed = 7f;
    [SerializeField] private float minSpeed = 3f;

    private float timer = 0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!isDiceRoll) return;

        timer += Time.deltaTime;

        if (!isSlowing && timer >= diceFastTimer)
        {
            isSlowing = true;
        }
    }

    /// <summary>
    /// 다이스 켜기/끄기
    /// </summary>
    public void SetDice(bool check)
    {
        diceSlot.SetActive(check);
        diceButton.SetActive(check);

        if (check) foreach (var d in dices) d.InitEyes();
    }


    public void RollDice()
    {
        if (isDiceRoll) return;
        if (InGameManager.Instance.isDefence) return;
        if (GameManager.Instance.IsPause) return;

        if (!InGameManager.Instance.eventDice) // 이동을 위한 주사위 굴리기 일 때
        {
            InGameManager.Instance.curRollCnt--;
            foreach (var dChance in dChances)
            {
                dChance.UpdateExistence();
            }
        }
        
        isDiceRoll = true;

        foreach (var dice in dices) // 즉각적인 주사위 면 snap
        {
            dice.HardAdjustDice();
        }

        diceSpeed = Random.Range(minSpeed, maxSpeed);
        StartCoroutine(IDiceRoll());
       //SoundManager.instance.PlaySFX(SFXSound.Rolling);
    }

    public IEnumerator IDiceRoll()
    {
        yield return new WaitForSeconds(diceFastTimer);

        float slowTimer = 0f;
        float startSpeed = diceSpeed;
        while (slowTimer < diceSlowTimer)
        {
            slowTimer += Time.deltaTime;
            float t = slowTimer / diceSlowTimer;
            diceSpeed = Mathf.Lerp(startSpeed, 0f, t);
            yield return null;
        }

        float minDistance = float.MaxValue;

        Dice selectedDice = null;

        foreach(var dice in dices)
        {
            float distance = Mathf.Abs(dice.transform.position.y - transform.position.y);
            if (distance < minDistance)
            {
                selectedDice = dice;
                minDistance = distance;
            }
        }

        foreach (var dice in dices) // 부드러운 주사위 면 snap
        {
            dice.AdjustDice(-selectedDice.transform.position.y);
        }

        yield return new WaitForSeconds(1f); // 주사위가 다 돌아갈때까지 대기

        if(InGameManager.Instance.eventDice)
        {
            if(InGameManager.Instance.isUp) // 수를 넘겨야할때
            {
                if(selectedDice.GetDistance() >= InGameManager.Instance.eventDiceNum) // 저장값 이상이면
                {
                    if (InGameManager.Instance.eventDiceDistance == 100)
                    {
                        InGameManager.Instance.UpdateCarRender(2);
                    }
                    else
                        InGameManager.Instance.UpdateDistance(-InGameManager.Instance.eventDiceDistance); // 정해진값만큼 넘겨준다
                }
            }
            else
            {
                if (selectedDice.GetDistance() <= InGameManager.Instance.eventDiceNum)
                {
                    InGameManager.Instance.UpdateDistance(-InGameManager.Instance.eventDiceDistance);
                }
            }

            InGameManager.Instance.ExitRollEvent();
        }
        else
        {
            // 움직인 만큼 감소
            InGameManager.Instance.DiceRoll(selectedDice.CompleteDice());
        }
        isDiceRoll = false;
    }
}
