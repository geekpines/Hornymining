using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volume;
    [SerializeField] private List<AudioSource> audioSources;
    private string key = "HMVolume";


    private void Start()
    {
        
        volume.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    private void ChangeVolume()
    {
        foreach (var source in audioSources)
        {
            source.volume = volume.value;
        }

    }

    

    private void OnApplicationQuit()
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            PlayerPrefs.SetFloat(key + i, audioSources[i].volume);
            PlayerPrefs.Save();
        }
    }
}
