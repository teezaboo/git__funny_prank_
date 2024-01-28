using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class settingSound : MonoBehaviour
{
   // public startMENU playani;

    [SerializeField] private Slider musicSli;
    [SerializeField] private Slider sfxSli;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!(PlayerPrefs.HasKey("Set_first_time"))){
            PlayerPrefs.SetInt("Set_first_time", 0);
            PlayerPrefs.SetFloat("soundMusicValue", 0.5f);
            PlayerPrefs.SetFloat("soundSFXValue", 0.5f);
            if(musicSli != null){
                musicSli.value = 0.5f;
            }
            if(sfxSli != null){
                sfxSli.value = 0.5f;
            }
        }else{
            print(PlayerPrefs.GetFloat("soundMusicValue"));
            if(musicSli != null){
                musicSli.value = PlayerPrefs.GetFloat("soundMusicValue");
            }
            if(sfxSli != null){
                sfxSli.value = 0.5f;
            }
        }
    }
    void Update()
    {
        
    }
}