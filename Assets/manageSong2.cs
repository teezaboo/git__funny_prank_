using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageSong2 : MonoBehaviour
{
    public string what;
     AudioSource sound;
     
     public float volume = 1f;

    public void changeValueAudio(){
        float offset = 1f*PlayerPrefs.GetFloat(what);
        sound.volume = offset*volume;
    }
    // Start is called before the first frame update
    void Start()
    {
        sound = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        changeValueAudio();
        PlayerPrefs.GetFloat(what);
    }

    public void playMyAduio(){
        sound.Play();
    }
    public void stopplay(){
        sound.Stop();
    }
}
