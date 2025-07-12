using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyManager : MonoBehaviour
{
    public static ToyManager Instance { get; private set; }

    [Header("Sprites")]
    public Image[] toyPhoto; // 사진 넣는곳
    public Sprite[] toys;
    public Sprite lockToy;

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
        toyPhoto[index].sprite = toys[index];
        toyCount++;
        if(toyCount == 18)
        {
            InGameManager.Instance.GameClear();
        }
    }

    public void LostToy(int index)
    {
        toyCount--;
        checkToy[index] = false;
        toyPhoto[index].sprite = lockToy;
        if (toyCount < 0) toyCount = 0;
    }
}
