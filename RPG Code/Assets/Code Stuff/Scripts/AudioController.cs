using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script contains functions pertatining to various uses of audio. Things like playing a provided clip, fading audio out over time, or playing a starting clip then having a second clip loop when the first has finished.
*/
public class AudioController : MonoBehaviour
{
	[SerializeField] private AudioSource introAudioSource; //Plays an audio clip once
	[SerializeField] private AudioSource loopAudioSource; //Plays an audio clip on loop
	//Getters
	public AudioSource IntroSource => introAudioSource;
	public AudioSource LoopSource => loopAudioSource;
	
	//Play an intro beat followed by a looping song
    public void IntroToLoop(AudioClip intro, AudioClip loop)
	{
		//Set proper clips to each source
		introAudioSource.clip = intro;
		loopAudioSource.clip = loop;
		
		//Play the intro followed by the loop
		double introDuration = (double)intro.samples / intro.frequency;
		double startTime = AudioSettings.dspTime + 0.2;
		introAudioSource.PlayScheduled(startTime);
		loopAudioSource.PlayScheduled(startTime + introDuration);
		
	}
	
	//Have the provded audio source play the provided audio clip at a given volume.
	public void PlaySong(AudioSource audioSource, AudioClip newClip, float newVol)
	{
		audioSource.volume = newVol;
		audioSource.clip = newClip;
		audioSource.Play();
	}
	
	//Have whatever audio the provided audio source is playing fade out over tiem
	public static class AudioFadeOut 
	{
		public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
		{
			float currentTime = 0;
			float start = audioSource.volume;
			while (currentTime < duration)
			{
				currentTime += Time.deltaTime;
				audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
				yield return null;
			}
			yield break;
		}
	}
}
