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

		int eventPickMax = allEvents.Length;
		for (int i = 0; i < allEvents.Length; i++)
		{
            eventPickMax += allEvents[i].chanceMult;
        }
        int eventPick = Random.Range(0, eventPickMax);
		int index = 0;
        for (int i = 0; i < allEvents.Length; i++)
        {
            eventPick -= allEvents[i].chanceMult;
			if (eventPick < 0)
			{
				index = i;
                break;
			}
        }
        EventData selected = allEvents[index];

		eventManager.ShowEvent(selected);
	}
}