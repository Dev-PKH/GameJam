using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
	Master,
	Gameplay,
	Lobby,
	Battle,
	Click,
	Event
}

public enum GameplaySound
{
	Battle,
	Rest,
    Shop
}

public enum LobbySound
{
	Track1,
	Track2,
	Track3
}

public enum BattleSound
{
	Slash,
	Swing,
	Hit,
	Die
}

public enum ClickSound
{
	Btn_map,
	Btn_base
}

public enum EventSound
{
	Typing,
	CD_Broken,
	CD_Playing
}

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;

	[SerializeField] private AudioMixer mixer;
	[SerializeField] private AnimationCurve volumeCurve;

	int test = 0;

	//AudioClip 모음
	[SerializeField] private AudioClip[] gameplayClips;
	[SerializeField] private AudioClip[] lobbyClips;
	[SerializeField] private AudioClip[] battleClips;
	[SerializeField] private AudioClip[] clickClips;
	[SerializeField] private AudioClip[] eventClips;

	//AudioSource 모음 AS = AudioSource의 준말
	[SerializeField] private AudioSource gameplayAS;
	[SerializeField] private AudioSource lobbyAS;
	[SerializeField] private AudioSource battleAS;
	[SerializeField] private AudioSource clickAS;
	[SerializeField] private AudioSource eventAS;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		PlayBGM(LobbySound.Track1);
	}
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
			PlayBGM(LobbySound.Track1);
		if (Input.GetKeyDown(KeyCode.Alpha2))
			PlayBGM(LobbySound.Track2);
		if (Input.GetKeyDown(KeyCode.Alpha3))
			PlayBGM(LobbySound.Track3);
	}

	public void PlayBGM(GameplaySound gamePlaySound)
	{
		if (lobbyAS.isPlaying) lobbyAS.Stop();
		gameplayAS.clip = gameplayClips[(int)gamePlaySound];
		gameplayAS.Play();
	}

	public void PlayBGM(LobbySound lobbySound)
	{
        if (gameplayAS.isPlaying) gameplayAS.Stop();
        lobbyAS.clip = lobbyClips[(int)lobbySound];
		lobbyAS.Play();
	}

	public void StopLobbyBGM()
	{
		lobbyAS.Stop();
	}

	public void StopGameBGM()
	{
		gameplayAS.Stop();
	}

	public void PlaySFX(BattleSound battleSound)
	{
		battleAS.PlayOneShot(battleClips[(int)battleSound]);
	}

	public void PlaySFX(ClickSound clickSound)
	{
		clickAS.PlayOneShot(clickClips[(int)clickSound]);
	}

	public void PlaySFX(EventSound eventSound)
	{
		eventAS.PlayOneShot(eventClips[(int)eventSound]);
	}
	public void SetVolume(SoundType soundType, float volume)
	{
		float curvedVolume = volumeCurve.Evaluate(volume);
		float volumeInDb = Mathf.Log10(Mathf.Clamp(curvedVolume, 0.0001f, 1f)) * 20f;

		if (volume <= 0.0001f)
		{
			volumeInDb = -80f;
		}

		mixer.SetFloat(soundType.ToString(), volumeInDb);
	}
}
