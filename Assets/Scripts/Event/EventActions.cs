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
			{ "IncreaseHP", IncreaseHP },
			{ "DecreaseHP", DecreaseHP },
			{ "GainRelic", GainRelic },
			{ "RemoveCard", RemoveCard },
			{ "Escape", Escape },
			{ "DoNothing", DoNothing }
		};
	}

	public void Execute(string actionType, string param)
	{
		if (actionMap.TryGetValue(actionType, out var action))
		{
			action.Invoke(param);
		}
		else
		{
			Debug.LogWarning($"[EventActions] 알 수 없는 액션 타입: {actionType}");
		}
	}

	// 각 액션 로직 안에서만 Debug 찍힘
	private void IncreaseHP(string param)
	{
		int value = int.Parse(param);
		Debug.Log($"[액션 실행] 체력 {value} 회복");
	}

	private void DecreaseHP(string param)
	{
		int value = int.Parse(param);
		// 예: player.TakeDamage(value);
		Debug.Log($"[액션 실행] 체력 {value} 감소");
	}

	private void GainRelic(string param)
	{
		// 예: inventory.AddRelic(param);
		Debug.Log($"[액션 실행] 유물 '{param}' 획득");
	}

	private void RemoveCard(string param)
	{
		// 예: cardManager.Remove(param);
		Debug.Log($"[액션 실행] 카드 '{param}' 제거");
	}

	private void Escape(string _)
	{
		// 예: battleSystem.Flee();
		Debug.Log("[액션 실행] 전투에서 도망침");
	}

	private void DoNothing(string _)
	{
		Debug.Log("[액션 실행] 아무 일도 일어나지 않음");
	}
}
