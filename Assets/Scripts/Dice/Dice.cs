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

    //public int upgradeIndex = 0; // 강화 인덱스
    public int currentEyes = 0; // 현재 주사위 눈의 개수

    public const float Offest = 1f;
    public const float maxLine = -11f;
    public const float maxOffDifference = 12f;




    void Start()
    {
        InitEyes();
    }

    // Update is called once per frame
    void Update()
    {
        if(DiceManager.Instance.isDiceRoll)
        {
            /*if (maxLine >= transform.position.y)
            {
                transform.position = new Vector3(0, Offest, 0);
            }
            else
            {
                transform.position += Vector3.down * DiceManager.Instance.diceSpeed * Time.deltaTime;
            }*/

            /*if (transform.position.y <= maxLine)
            {
                transform.position = new Vector3(0f, Offest, 0f); // 정확히 리셋
            }
            else
            {
                transform.position += Vector3.down * DiceManager.Instance.diceSpeed * Time.deltaTime;
            }*/

            if (transform.position.y <= maxLine)
            {
                transform.position += new Vector3(0f, maxOffDifference, 0f); // 상대적인 리셋
                SoundManager.instance.PlaySFX(SFXSound.Rolling);
            }
            else
            {
                transform.position += Vector3.down * DiceManager.Instance.diceSpeed * Time.deltaTime;
            }
        }

    }
    public void HardAdjustDice()
    {
        // 주사위 굴리기 전 위치를 바로잡아 이상한 모습을 방지
        float snappedY = Mathf.Round(transform.position.y);
        Vector3 snappedPos = new Vector3(transform.position.x, snappedY, transform.position.z);
        transform.position = snappedPos;
    }

    public void AdjustDice(float adjustValue)
    {
        float unit = 2f; // 각 눈 간의 거리 단위 (슬롯 간격)
        Vector3 newPos = transform.position + new Vector3(0, adjustValue, 0);

        // 스냅 처리: 가장 가까운 정수 위치로 정렬
        float snappedY = Mathf.Round(newPos.y / unit) * unit - 3f;

        Vector3 snappedPos = new Vector3(newPos.x, snappedY, newPos.z);

        //Vector3 newPos = transform.position + new Vector3(0, adjustValue, 0);
        transform.DOMove(snappedPos, 1f);
    }

    public int CompleteDice()
    {
        Debug.Log(status + " 다이스 선택");
        return GetDistance();
    }

    // 주사위 합 반환
    public int GetDistance()
    {
        int value = 0;
        int doubleMult = 0;
        for (int i = 0; i < currentEyes; i++)
        {
            switch (eyesStatus[i])
            {
                case EyesStatus.basic:
                    value += (int)diceColor[i] + 1;
                    break;
                case EyesStatus.rhombus:
                    InGameManager.Instance.UpdateMoney(1);
                    break;
                case EyesStatus.triangle:
                    value += (int)Mathf.Round(Random.Range(-3, 3));
                    break;
                case EyesStatus.star:
                    doubleMult += 1;
                    break;
            }
        }

        while (doubleMult > 0)
        {
            value *= 2;
            doubleMult -= 1;
        }

        return value;
    }

    /// <summary>
    /// 주사위 눈을 초기 설정
    /// </summary>
    public void InitEyes()
    {
        for(int i=0; i<currentEyes; i++)
        {
            if(eyesStatus[i] == EyesStatus.basic)
            {
                diceEyes[i].sprite = InGameManager.Instance.eyeValues[0];

                switch (diceColor[i])
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
                        diceEyes[i].color = new Color(0.6f, 0, 0.8f,1f);
                        break;
                    case DiceColor.yellow:
                        diceEyes[i].color = Color.yellow;
                        break;
                    case DiceColor.red:
                        diceEyes[i].color = Color.red;
                        break;
                }
            }
            else
            {
                diceEyes[i].sprite = InGameManager.Instance.eyeValues[(int)eyesStatus[i]];
            }
        }
    }
}
