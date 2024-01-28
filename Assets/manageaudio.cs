using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageaudio : MonoBehaviour
{
    public List<AudioClip> soundClips; // สร้าง List สำหรับเก็บเสียง
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // ดึง AudioSource ที่ติดตั้งอยู่ใน GameObject นี้
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // ตรวจสอบหากต้องการดำเนินการใด ๆ ใน Update
    }
    public void StopPlay(){
        audioSource.Stop();
    }

    public void PlayRandomSound()
    {
        if (soundClips.Count == 0)
        {
            Debug.LogWarning("No sound clips assigned.");
            return;
        }
        audioSource.Stop();
        // เลือกเสียงสุ่มจาก List
        int soundIndex = Random.Range(0, soundClips.Count);

        // ตรวจสอบหากเสียงที่เลือกถูกต้องก่อนที่จะเล่น
        if (soundIndex >= 0 && soundIndex < soundClips.Count)
        {
            audioSource.PlayOneShot(soundClips[soundIndex]);
        }
        else
        {
            Debug.LogWarning("Invalid sound index: " + soundIndex);
        }
    }
}