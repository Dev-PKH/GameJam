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
    public bool[] checkToy = new bool[16];

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
    }
}
