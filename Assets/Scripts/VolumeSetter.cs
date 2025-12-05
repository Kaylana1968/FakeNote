using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VolumeSetter : MonoBehaviour
{
  AudioSource audioSource;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    float volume = PlayerPrefs.GetFloat("volume", 1f);
    audioSource.volume = volume;
  }
}
