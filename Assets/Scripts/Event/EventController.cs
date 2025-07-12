using UnityEngine;

public class EventController : MonoBehaviour
{
	[SerializeField] public EventManager eventManager;
	[SerializeField] private EventData[] allEvents; // 여러 ScriptableObject들

	int prevIndex = -1;
	bool isActive = false;

	/*private void Update()
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
	}*/

    // 이번에 올 이벤트의 인덱스를 구함
	public int FetchEventIndex()
	{
        // 선택 가중치를 전부 더하여 선택 풀을 만든다
		int eventPickMax = allEvents.Length;
		for (int i = 0; i < allEvents.Length; i++)
		{
            eventPickMax += allEvents[i].chanceMult;
        }
        // 선택 풀에서 랜덤 정수를 선택
        int eventPick = Random.Range(0, eventPickMax);
		int returnIndex = 0;
        for (int i = 0; i < allEvents.Length; i++)
        {
            // 각 이벤트별 가중치를 하나씩 빼기
            eventPick -= allEvents[i].chanceMult;
			if (eventPick < 0) // 랜덤 정수가 음수가 되면 당선
			{
                returnIndex = i;
                break;
			}
        }

        // 이전 이벤트와 중복됨 -> 다시
        if (returnIndex == prevIndex) {
            return FetchEventIndex();
        }
        // 이전 이벤트가 중복되지 않았음 -> 전달
        return returnIndex;
    }

    public void TriggerRandomEvent()
    {
        if (allEvents.Length == 0)
        {
            Debug.LogWarning("이벤트 목록이 비어 있음!");
            return;
        }

        // 이전 이벤트와 겹치지 않는 이벤틑 인덱스를 가져옴
        int index = FetchEventIndex();
        EventData selected = allEvents[index];
        // 이전 이벤트의 인덱스를 갱신
        prevIndex = index;

        eventManager.ShowEvent(selected);
    }
}