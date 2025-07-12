using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToyManager : MonoBehaviour
{
    public static ToyManager Instance { get; private set; }

    [Header("Sprites")]
    public Image[] toyPhoto; // 사진 넣는곳
    public Sprite[] toys;
    public Sprite[] inGameToySprite;
    public Sprite lockToy;

    [Header("Texts")]
    public TextMeshProUGUI[] toyTexts;

    public string[] toyNames;
    public string lockToyName = "???";

    // Toy
    public bool[] checkToy;
    public int toyCount = 0; // 현재 나의 장난감 갯수

    private void Awake()
    {
        if (Instance == null) Instance = this;

        InitToy();
    }

    // 장난감 초기화
    private void InitToy()
    {
        for(int i=0; i<toyPhoto.Length; i++)
        {
            toyPhoto[i].sprite = lockToy;
            checkToy[i] = false;
            toyTexts[i].text = lockToyName;
        }
        toyCount = 0;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetToy(int index)
    {
        if (checkToy[index]) return;
        checkToy[index] = true;
        toyPhoto[index].sprite = inGameToySprite[index];
        toyTexts[index].text = toyNames[index];
        toyCount++;
        if(toyCount == 16)
        {
            InGameManager.Instance.GameClear();
        }
    }

    public void LostToy(int index)
    {
        toyCount--;
        checkToy[index] = false;
        toyPhoto[index].sprite = lockToy;
        toyTexts[index].text = lockToyName;
        if (toyCount < 0) toyCount = 0;
    }
}
