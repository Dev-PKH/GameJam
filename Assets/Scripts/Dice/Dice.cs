using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum DiceStatus
{
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    None
}

public enum EyesStatus
{ 
    basic,
    rhombus,
    triangle,
    star
}

public enum DiceColor
{
    basic,
    green,
    blue,
    purple,
    yellow,
    red
}


public class Dice : MonoBehaviour
{
    public DiceStatus status;
    public SpriteRenderer[] diceEyes;
    public EyesStatus[] eyesStatus;
    public DiceColor[] diceColor;

    public int upgradeIndex = 0; // 강화 인덱스
    public int currentEyes = 0; // 현재 주사위 눈의 개수

    public const float Offest = 2f;
    public const float maxLine = -10f;




    void Start()
    {
        InitEyes();
    }

    // Update is called once per frame
    void Update()
    {
        if(DiceManager.Instance.isDiceRoll)
        {
            if (maxLine >= transform.position.y)
            {
                transform.position = new Vector3(0, Offest, 0);
            }
            else
            {
                transform.position += Vector3.down * DiceManager.Instance.diceSpeed * Time.deltaTime;
            }
        }

    }

    public void AdjustDice(float adjustValue)
    {
        float unit = 2f; // 각 눈 간의 거리 단위 (슬롯 간격)
        Vector3 newPos = transform.position + new Vector3(0, adjustValue, 0);

        // 스냅 처리: 가장 가까운 정수 위치로 정렬
        float snappedY = Mathf.Round(newPos.y / unit) * unit;

        Vector3 snappedPos = new Vector3(newPos.x, snappedY, newPos.z);

        //Vector3 newPos = transform.position + new Vector3(0, adjustValue, 0);
        transform.DOMove(snappedPos, 1f);
    }

    public int CompleteDice()
    {
        Debug.Log(status + "다이스 선택");
        return GetDistance();
    }

    // 주사위 합 반환
    public int GetDistance()
    {
        int value = 0;
        for(int i=0; i<currentEyes; i++)
        {
            if (eyesStatus[i] == EyesStatus.basic)
            {
                value += (int)diceColor[i] + 1;
            }
        }

        return value;
    }

    /// <summary>
    /// 주사위 눈을 초기 설정
    /// </summary>
    public void InitEyes()
    {
        for(int i=0; i< currentEyes; i++)
        {
            if(eyesStatus[i] == EyesStatus.basic)
            {
                switch(diceColor[i])
                {
                    case DiceColor.basic:
                        diceEyes[i].color = Color.black;
                        break;
                    case DiceColor.green:
                        diceEyes[i].color = Color.green;
                        break;
                    case DiceColor.blue:
                        diceEyes[i].color = Color.blue;
                        break;
                    case DiceColor.purple:
                        diceEyes[i].color = new Color(0.5f, 0, 0.5f,1f);
                        break;
                    case DiceColor.yellow:
                        diceEyes[i].color = Color.yellow;
                        break;
                    case DiceColor.red:
                        diceEyes[i].color = Color.red;
                        break;
                }
            }
        }
    }
}
