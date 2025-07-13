using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
	[Header("UI Components")]
	[SerializeField] private TextMeshProUGUI titleText;
	//[SerializeField] private Image eventImage;
	[SerializeField] private TextMeshProUGUI descriptionText;
	[SerializeField] private Button[] choiceButtons;

	[Header("Action Executor")]
	[SerializeField] private EventActions eventActions;

	public void ShowEvent(EventData data)
	{
		titleText.text = data.title;
		descriptionText.text = data.description;
		//eventImage.sprite = data.image;

		for (int i = 0; i < choiceButtons.Length; i++)
		{
			if (i < data.choices.Count)
			{
				var choice = data.choices[i];
				var button = choiceButtons[i];

				button.gameObject.SetActive(true);
				button.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceText;

				button.onClick.RemoveAllListeners();
				button.onClick.AddListener(() =>
				{
					eventActions.Execute(choice.actionType, choice.actionParam);
				});
			}
			else
			{
				choiceButtons[i].gameObject.SetActive(false);
			}
		}
	}
}
