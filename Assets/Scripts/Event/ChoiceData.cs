using System;

[Serializable]
public class ChoiceData
{
	public string choiceText;     // 버튼에 표시될 텍스트
	public string actionType;     // 어떤 종류의 액션인지
	public string actionParam;    // 액션의 세부 파라미터 (숫자/이름 등)
}
