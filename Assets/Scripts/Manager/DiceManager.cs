using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    //SpriteRenderer
    public static DiceManager Instance { get; private set; }

    // Dice
    public Dice[] dices;
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

    public void RollDice()
    {
        if (isDiceRoll) return;

        isDiceRoll = true;
        diceSpeed = Random.Range(minSpeed, maxSpeed);
        StartCoroutine(IDiceRoll());
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

        foreach (var dice in dices)
        {
            dice.AdjustDice(-selectedDice.transform.position.y);
        }

        selectedDice?.CompleteDice();

        isDiceRoll = false;
    }
}
