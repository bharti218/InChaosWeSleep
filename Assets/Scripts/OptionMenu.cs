using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] GameObject view_01, view_02;
    

    private void Start()
    {
        Invoke("EnbleSecondView", 3f);
    }

    private void EnbleSecondView()
    {
        view_01.SetActive(false);
        view_02.SetActive(true);

    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    


}
