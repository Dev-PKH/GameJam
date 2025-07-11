using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsPause { get; private set; } // 일시 정지 여부

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
