using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class VolumeManager : MonoBehaviour
{
	[SerializeField] Slider slider;
	TMP_Text textContainer;

	void Start()
	{
		textContainer = GetComponent<TMP_Text>();
		slider.value = PlayerPrefs.GetFloat("volume", 1f);
	}

	public void SetVolume(float value)
	{
		PlayerPrefs.SetFloat("volume", value);
		textContainer.text = $"{Mathf.Round(value * 100f)}%";
	}
}
