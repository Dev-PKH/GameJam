using UnityEngine;

public class EventController : MonoBehaviour
{
	[SerializeField] private EventManager eventManager;
	[SerializeField] private EventData[] allEvents; // 여러 ScriptableObject들

	bool isActive = false;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha0))
		{
			TriggerRandomEvent();
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			isActive = !isActive;
			eventManager.gameObject.SetActive(isActive);

        }
	}

	public void TriggerRandomEvent()
	{
		if (allEvents.Length == 0)
		{
			Debug.LogWarning("이벤트 목록이 비어 있음!");
			return;
		}

		int index = Random.Range(0, allEvents.Length);
		EventData selected = allEvents[index];

		eventManager.ShowEvent(selected);
	}
}