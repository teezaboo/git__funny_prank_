using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class main_controller_laugh : MonoBehaviour
{
    public manageaudio my_manageaudio;
    public float maxHp;
    public float hp;
    public int laugh_point;
    public int add_laugh_point;
    public Animator angel_ani;
    public TextMeshProUGUI text_laugh_point;
    public TextMeshProUGUI text_laugh_point2;
    public bool isCan_addAni;
    public bool isaniLike1;
    public bool isaniLike2;
    public bool isaniLike3;
    public RectTransform rectTransform1;
    public RectTransform rectTransform2;
    public GameObject objectToSpawn;
    public GameObject mom_Object;
    public SpawnManager SpawnItem_controller;
    int randomInt;
    public bool player_isDie = false;
    public mainController controll_menu;
    public bool isNev;
    IEnumerator loopDEEEE()
    {
        yield return new WaitForSeconds(5f);
        laugh_point -= controll_menu.item_deee;
        StartCoroutine(loopDEEEE());
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loopDEEEE());
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(player_isDie == false)
        if(laugh_point < 0){
            player_isDie = true;
            controll_menu.dieMenu();
        }
        add_ani();
    }
    public void chagne_laugh(int i){
        if(i < 0 && add_laugh_point > 0){
            add_laugh_point = 0;
        }
        laugh_point += i;
        add_laugh_point += i;
        if(laugh_point < 0){
            isNev = true;
        }
        if(isNev){
            text_laugh_point.text = "-"+AddCommasToNumber2(laugh_point*-1).ToString();
            text_laugh_point2.text = "-"+AddCommasToNumber2(laugh_point*-1).ToString();
        }else{
            
            text_laugh_point.text = AddCommasToNumber2(laugh_point).ToString();
            text_laugh_point2.text = AddCommasToNumber2(laugh_point).ToString();
        }
    }
    public void add_ani(){
        my_manageaudio.PlayRandomSound();
        if(isCan_addAni != true){
            if(add_laugh_point >= 100){
                add_laugh_point-=100;
                angel_ani.Play("angel_like3");
                StartCoroutine(delaySpawnLaugh(2, UnityEngine.Random.Range(0, 1f)));
                StartCoroutine(delaySpawnLaugh(2, UnityEngine.Random.Range(0, 1f)));
                StartCoroutine(delaySpawnLaugh(2, UnityEngine.Random.Range(0, 1f)));
                if(UnityEngine.Random.Range(0f,100f) <= 25f){
                    StartCoroutine(delaySpawnLaugh(3, UnityEngine.Random.Range(0, 1f)));
                }
            }
            if(add_laugh_point >= 50){
                add_laugh_point-=50;
                angel_ani.Play("angel_like2");
                StartCoroutine(delaySpawnLaugh(2, UnityEngine.Random.Range(0, 1f)));
                StartCoroutine(delaySpawnLaugh(2, UnityEngine.Random.Range(0, 1f)));
                if(UnityEngine.Random.Range(0f,100f) <= 12.5f){
                    StartCoroutine(delaySpawnLaugh(3, UnityEngine.Random.Range(0, 1f)));
                }
            }else if(add_laugh_point >= 20){
                add_laugh_point-=20;
                angel_ani.Play("angel_like1");
                StartCoroutine(delaySpawnLaugh(2, UnityEngine.Random.Range(0, 1f)));
                if(UnityEngine.Random.Range(0f,100f) <= 6.25f){
                    StartCoroutine(delaySpawnLaugh(3, UnityEngine.Random.Range(0, 1f)));
                }
            }else if(add_laugh_point <= -20){
                float de_count = add_laugh_point;
                add_laugh_point = 0;
                angel_ani.Play("angel_angry");
                while(de_count < 0){
                    de_count+=20;                    
                    StartCoroutine(delaySpawnLaugh(1, UnityEngine.Random.Range(0, 1f)));
                }
            }
            StartCoroutine(delayAddani());
        }
    }
    IEnumerator delaySpawnLaugh(int typeis, float i){
        yield return new WaitForSeconds(i);
        SpawnObjectBetweenRects(typeis);
    }
    IEnumerator delayAddani(){
        isCan_addAni = true;
        yield return new WaitForSeconds(0.8f);
        if(add_laugh_point < 20 || add_laugh_point > -20){
            angel_ani.Play("angel_idle");
        }
        isCan_addAni = false;
    }
    public void SpawnObjectBetweenRects(int i)
    {
        // หาตำแหน่ง x ที่สุ่ม
        float randomX = UnityEngine.Random.Range(Mathf.Min(rectTransform1.position.x, rectTransform2.position.x),
                                      Mathf.Max(rectTransform1.position.x, rectTransform2.position.x));

        // สร้าง Vector3 ใหม่ที่มีตำแหน่ง x เป็นตำแหน่งที่สุ่ม
        Vector3 spawnPosition = new Vector3(randomX, rectTransform1.position.y, 0f); // แก้ตำแหน่งในแกน z ตามที่ต้องการ

        // สร้าง Object ที่ตำแหน่งที่สุ่ม
        GameObject laughOBJ = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity, mom_Object.transform);
        laughOBJ.SetActive(true);
        
        Animator childAnimator = laughOBJ.GetComponentInChildren<Animator>();
        if(i == 1){
            childAnimator.Play("moveNoLaugh");
        }else if(i == 2){
            childAnimator.Play("moveLaugh");
        }else if(i == 3){
            childAnimator.Play("moveItem1");
            StartCoroutine(spawn_item(i));
        }
    }
    
    IEnumerator spawn_item(int typeis){
        yield return new WaitForSeconds(1f);
                    randomInt = UnityEngine.Random.Range(0, 3);
                    StartCoroutine(delaySpawnLaugh(3, randomInt));
        SpawnItem_controller.SpawnMonster(randomInt);
    }
string AddCommasToNumber2(int number)
{
    string numberString = number.ToString();
    
    // ตรวจสอบว่าตัวเลขมีมากกว่า 3 หลักหรือไม่
    if (numberString.Length > 3)
    {
        StringBuilder result = new StringBuilder();

        // หากมีทศนิยม
        int decimalIndex = numberString.IndexOf('.');
        string decimalPart = "";
        if (decimalIndex != -1)
        {
            decimalPart = numberString.Substring(decimalIndex);
            numberString = numberString.Substring(0, decimalIndex);
        }

        // ลูปทุกรอบ 3 หลัก
        int count = 0;
        for (int i = numberString.Length - 1; i >= 0; i--)
        {
            result.Insert(0, numberString[i]);
            count++;

            if (count == 3 && i > 0)
            {
                result.Insert(0, ",");
                count = 0;
            }
        }

        // เพิ่มทศนิยมกลับ
        result.Append(decimalPart);

        numberString = result.ToString();
    }

    return numberString;
}
}
