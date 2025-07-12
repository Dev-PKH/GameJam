using System;
using System.Collections.Generic;
using UnityEngine;

public class EventActions : MonoBehaviour
{
	private Dictionary<string, Action<string>> actionMap;

	private void Awake()
	{
		actionMap = new Dictionary<string, Action<string>>()
		{
			{ "LimitDistance", LimitDistance },
			{ "RandomDistance", RandomDistance },
			{ "UpdateDistance", UpdateDistance },
			{ "DiceRoll", DiceRoll },
			{ "NoneAdvanture", NoneAdvanture },
            { "HybridEvent", HybridEvent },
            { "UpdateMoney", UpdateMoney },
            { "UpdateSprite", UpdateSprite },
            { "DoNothing", DoNothing }
		};
	}

	public void Execute(string actionType, string param)
	{
		if (actionMap.TryGetValue(actionType, out var action))
		{
			//Debug.Log(actionType);
			action.Invoke(param);
			InGameManager.Instance.ExitEvent();
        }
		else
		{
			Debug.LogWarning($"[EventActions] 알 수 없는 액션 타입: {actionType}");
		}
	}


    /// <summary>
    ///  움직임을 제한한다.
    /// </summary>
    private void LimitDistance(string param)
    {
        int value = int.Parse(param);
		InGameManager.Instance.limitDistance = value;

        Debug.Log("리밋 실행");
    }

	private void RandomDistance(string param)
	{
		int num = 0;

		if(UnityEngine.Random.value <= 0.5f)
		{
			num = -2;
			UpdateDistance(num.ToString());
		}
		else
		{
            num = 3;
            UpdateDistance(num.ToString());
        }
	}

	/// <summary>
	/// (0: 이하, 1: 이상/ 주사위 제한수(포함)/이동할거리(100이면 다른 이벤트))
	/// ex) 0 5 3 // 5이하이면 +3
	/// </summary>
	/// <param name="param"></param>
	public void DiceRoll(string param)
	{
        string[] words = param.Split();

		int check = int.Parse(words[0]);
        int limit = int.Parse(words[1]);
        int dist = int.Parse(words[2]);

		InGameManager.Instance.EventRoll(check, limit, dist);
	}

    /// <summary>
    /// 움직임을 갱신한다.
    /// </summary>
    private void UpdateDistance(string param)
    {
        int value = int.Parse(param);
		InGameManager.Instance.UpdateDistance(-value);
    }

	private void NoneAdvanture(string param)
	{
        int value = int.Parse(param);
		InGameManager.Instance.NoneAdvanture = value;

    }

	/// <summary>
	/// 재화, 거리 순
	/// </summary>
	/// <param name="param"></param>
	private void HybridEvent(string param)
	{
		string[] words = param.Split();
		UpdateMoney(words[0]);
		UpdateDistance(words[1]);
    }

	private void UpdateMoney(string param)
	{
        int value = int.Parse(param);
		InGameManager.Instance.UpdateMoney(value);
    }

	private void UpdateSprite(string param)
	{
        int value = int.Parse(param);
		InGameManager.Instance.UpdateCarRender(value);
    }

    private void DoNothing(string param)
	{
		Debug.Log("아무일이 없는 이벤트");
	}
	
}
