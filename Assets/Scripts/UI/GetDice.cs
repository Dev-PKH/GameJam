using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDice : MonoBehaviour
{
    public DiceStatus status;
    public bool isSelected;

    public Image[] images;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Vector2> GetImagePositions(int index)
    {
        List<Vector2> positions = new List<Vector2>();

        switch (index)
        {
            case 1:
                positions.Add(new Vector2(0, 0));
                break;
            case 2:
                positions.Add(new Vector2(-40, 40));
                positions.Add(new Vector2(40, -40));
                break;
            case 3:
                positions.Add(new Vector2(-40, 40));
                positions.Add(new Vector2(0, 0));
                positions.Add(new Vector2(40, -40));
                break;
            case 4:
                positions.Add(new Vector2(-40, 40));
                positions.Add(new Vector2(40, 40));
                positions.Add(new Vector2(-40, -40));
                positions.Add(new Vector2(40, -40));
                break;
            case 5:
                positions.Add(new Vector2(-40, 40));
                positions.Add(new Vector2(40, 40));
                positions.Add(new Vector2(0, 0));
                positions.Add(new Vector2(-40, -40));
                positions.Add(new Vector2(40, -40));
                break;
            case 6:
                positions.Add(new Vector2(-40, 40));
                positions.Add(new Vector2(40, 40));
                positions.Add(new Vector2(-40, 0));
                positions.Add(new Vector2(40, 0));
                positions.Add(new Vector2(-40, -40));
                positions.Add(new Vector2(40, -40));
                break;
        }

        return positions;
    }

    public void SetDice()
    {
        CheckDice((int)status);
    }

    // 다이스 종류 체크
    public void CheckDice(int index)
    {
        foreach (var img in images) img.gameObject.SetActive(false);


        Dice dice = InGameManager.Instance.dices[index];

        int len = dice.currentEyes;

        List<Vector2> pos = GetImagePositions(len);

        for (int i = 0; i < len; i++)
        {
            images[i].gameObject.SetActive(true);
            images[i].rectTransform.anchoredPosition = pos[i];

            if (dice.eyesStatus[i] == EyesStatus.basic)
            {
                images[i].sprite = InGameManager.Instance.eyeValues[0];
                switch (dice.diceColor[i])
                {
                    case DiceColor.basic:
                        images[i].color = Color.black;
                        break;
                    case DiceColor.green:
                        images[i].color = Color.green;
                        break;
                    case DiceColor.blue:
                        images[i].color = Color.blue;
                        break;
                    case DiceColor.purple:
                        images[i].color = new Color(0.5f, 0, 0.5f, 1f);
                        break;
                    case DiceColor.yellow:
                        images[i].color = Color.yellow;
                        break;
                    case DiceColor.red:
                        images[i].color = Color.red;
                        break;
                }
            }
            else
            {
                images[i].sprite = dice.diceEyes[i].sprite;
            }
        }
    }

    public void ViewDice(int index)
    {
        if (GameManager.Instance.IsPause) return;
        status = (DiceStatus)index;
        CheckDice(index);
        InGameManager.Instance.normalShopButton.gameObject.SetActive(false);
        InGameManager.Instance.specialShopButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// 버튼 번호를 받아 해당 번호를 강화
    /// </summary>
    /// <param name="index"></param>
    public void UpgradeViewButton(int index)
    {
        //if(co)
        if (GameManager.Instance.IsPause) return;
        InGameManager.Instance.SetShopButton((int)status, index);
        /*Dice dice = InGameManager.Instance.dices[(int)status];

        InGameManager.Instance.specialShopButton.SetActive(true);

        if (dice.eyesStatus[index] == EyesStatus.basic)
        {
            InGameManager.Instance.normalShopButton.SetActive(true);
        }
        else InGameManager.Instance.normalShopButton.SetActive(false);*/
    }

    public void NormalEyes()
    {
        if (GameManager.Instance.IsPause) return;
        if (InGameManager.Instance.GetNoramlEyesValue() > InGameManager.Instance.money) return;

        InGameManager.Instance.BuyNormalShopEyes((int)status);
    }

    public void SpecialEyes()
    {
        if (GameManager.Instance.IsPause) return;
        if (InGameManager.Instance.money < 3) return;
        InGameManager.Instance.BuySpecialShopEyes((int)status);
    }
}
