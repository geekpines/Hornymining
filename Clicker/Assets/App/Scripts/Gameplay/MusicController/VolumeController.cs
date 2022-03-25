using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volume;
    [SerializeField] private List<AudioSource> audioSources;


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
}
