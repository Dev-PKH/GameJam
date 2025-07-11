using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Event/EventData")]
public class EventData : ScriptableObject
{
	public string title;                // 이벤트 제목
	[TextArea] public string description;  // 본문 설명
	public Sprite image;                // 이벤트 이미지
	public List<ChoiceData> choices;    // 1~4개의 선택지
}
