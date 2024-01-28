using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class player_Controller : MonoBehaviour
{
    public bool isInMenu = false;
    public List<Sprite> mySprite;
    public SpriteRenderer my_SpriteRenderer;
    public bool isDust = false;
    public bool isDust2 = false;
     GameObject Dusttest;
     GameObject Dusttest2;
    public GameObject dustEffectLT;
    public GameObject Spawn_Dust;
    public float dustDelay = 0.1f;
    public float dustDelay2 = 0.1f;
    public Collider2D playerCollider;
    public bool isPick = false;
    public bool isClick = false;
    public controll_collider my_collider;
    public int skill_type = 0;
    public int type_item_pick;
    public int type_pick;
    public List<GameObject> List_item;
    public List<GameObject> List_ra;
    public Sprite backup_mySprite;
    public float spawn_X;
    public float spawn_Y;
    public void isMENUIN(){
        backup_mySprite = my_SpriteRenderer.sprite;
        my_SpriteRenderer.sprite  = mySprite[0];
        isInMenu = true;
    }
    public void isMENUOUT(){
        my_SpriteRenderer.sprite = backup_mySprite;
        isInMenu = false;
    }
    void Update()
    {
        move_cersor();
        if(skill_type == 0){
            if(isClick == true){
                my_SpriteRenderer.sprite  = mySprite[2];
            }else if(isPick == true){
                my_SpriteRenderer.sprite  = mySprite[1];
            }else{
                my_SpriteRenderer.sprite  = mySprite[0];
            }
            if(type_pick == 1){
                if(isPick){
                    if (Input.GetMouseButtonDown(0)){
                        my_collider.isActive = true;
                        if(my_collider.obj_pick.GetComponent<BotController>().isfire != true){
                            if(my_collider != null){
                                if(my_collider.obj_pick.GetComponent<BotController>().numbTime != null){
                                    StopCoroutine(my_collider.obj_pick.GetComponent<BotController>().numbTime);
                                }
                            }
                            my_collider.obj_pick.GetComponent<BotController>().Runed = false;
                            my_collider.obj_pick.GetComponent<BotController>().isAniPick = false;
                            if(my_collider.obj_pick.GetComponent<BotController>().numbTime != null){
                                StopCoroutine(my_collider.obj_pick.GetComponent<BotController>().numbTime);
                            }
                            if(my_collider.obj_pick.GetComponent<BotController>().numbTime2 != null){
                                StopCoroutine(my_collider.obj_pick.GetComponent<BotController>().numbTime2);
                            }
                            if(my_collider.obj_pick.GetComponent<BotController>().numbTime3 != null){
                                StopCoroutine(my_collider.obj_pick.GetComponent<BotController>().numbTime3);
                            }
                            my_collider.obj_pick.GetComponent<BotController>().SetNewRandomTarget();
                            isClick = true;
                            my_collider.obj_pick.GetComponent<BotController>().isPick = true;
                            my_collider.obj_pick.GetComponent<BotController>().isPanic = false;
                            if(my_collider.obj_pick.GetComponent<BotController>().isOnPee == true){
                                my_collider.obj_pick.GetComponent<BotController>().setAniOnpickOnPee();
                            }else{
                                my_collider.obj_pick.GetComponent<BotController>().setAniOnpick();
                            }
                            // my_collider.obj_pick.GetComponent<BotController>().npc_ani.Play("idle");
                        }else{
                            if(my_collider != null){
                                if(my_collider.obj_pick.GetComponent<BotController>().numbTime != null){
                                    StopCoroutine(my_collider.obj_pick.GetComponent<BotController>().numbTime);
                                }
                            }
                            my_collider.obj_pick.GetComponent<BotController>().Runed = false;
                            my_collider.obj_pick.GetComponent<BotController>().isAniPick = false;
                            isClick = true;
                        }
                    }
                }
                if (Input.GetMouseButtonUp(0)){
                    my_collider.boxCollider.enabled = false;
                    my_collider.boxCollider.enabled = true;
                    if(my_collider.obj_pick.GetComponent<BotController>().isOnPee == true){
                        my_collider.obj_pick.GetComponent<BotController>().isPee = false;
                        my_collider.obj_pick.GetComponent<BotController>().isOnPee = false;
                    }
                    if(my_collider.obj_pick.GetComponent<BotController>().isfire == true){
                        my_collider.obj_pick.GetComponent<BotController>().npc_ani.Play("panic_move");
                    }
                    my_collider.obj_pick.GetComponent<BotController>().setR_inMove();
                    my_collider.obj_pick.GetComponent<BotController>().SetNewRandomTarget();
                    if(my_collider.obj_pick != null){
                        my_collider.obj_pick.GetComponent<BotController>().isPick = false;
                        my_collider.obj_pick.GetComponent<BotController>().setAniNumb();
                    }
                    isClick = false;
                    isPick = false;
                    my_collider.obj_pick = null;
                    my_collider.boxCollider.enabled = false;
                    my_collider.boxCollider.enabled = true;
                    my_collider.isActive = false;
                }
            }else if(type_pick == 2){
                if (Input.GetMouseButtonDown(0)){
                    my_collider.isActive = true;
                    isClick = true;
                }
                if (Input.GetMouseButtonUp(0)){
                    
                    skill_type = type_item_pick;
                    type_item_pick = 0;
                    type_pick = 0;
                    my_collider.obj_pick.GetComponent<item_controller>().die();
                    my_collider.obj_pick = null;
                    isClick = false;
                    isPick = false;
                    my_collider.boxCollider.enabled = false;
                    my_collider.boxCollider.enabled = true;
                   // my_collider.isActive = false;
                }
            }
        }else if(skill_type > 0){
                    my_collider.isActive = true;
            my_SpriteRenderer.sprite  = mySprite[2+skill_type];
            List_ra[skill_type - 1].SetActive(true);
            if (Input.GetMouseButtonDown(0)){
                my_collider.isActive = false;
                GameObject newItem = Instantiate(List_item[skill_type - 1], new Vector3(transform.position.x  -0.2f, transform.position.y -0.3f, transform.position.z), Quaternion.identity);
                newItem.SetActive(true);
                List_ra[skill_type - 1].SetActive(false);
                skill_type = 0;
            }
        }else if(skill_type == 2){
                    my_collider.isActive = true;
            my_SpriteRenderer.sprite  = mySprite[2+skill_type];
            List_ra[skill_type - 1].SetActive(true);
            if (Input.GetMouseButtonDown(0)){
                my_collider.isActive = false;
                GameObject newItem = Instantiate(List_item[skill_type - 1], new Vector3(transform.position.x  -0.2f, transform.position.y -0.3f, transform.position.z), Quaternion.identity);
                newItem.SetActive(true);
                List_ra[skill_type - 1].SetActive(false);
                skill_type = 0;
            }
        }
    }
    public void pickSkill(int i){
        skill_type = i;
    }

    public void move_cersor(){
        if(isInMenu ==false){
            // ดึงตำแหน่งของเมาส์ในโลก 2D
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ตั้งตำแหน่งของ Object ให้เท่ากับตำแหน่งของเมาส์ (ไม่เกี่ยวข้องกับแกน Z)
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
            
            //สปอนฝุ่น
            if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0)
            {
                if(isDust2 == false){
                    StartCoroutine(delayDust_idle());
                    Dusttest = Instantiate(dustEffectLT, new Vector3(Spawn_Dust.transform.position.x, Spawn_Dust.transform.position.y, Spawn_Dust.transform.position.z),Quaternion.identity);
                    Dusttest.transform.gameObject.SetActive(true);
                }
            }else{
                if(isDust == false){
                    StartCoroutine(delayDustWalk());
                    Dusttest = Instantiate(dustEffectLT, new Vector3(Spawn_Dust.transform.position.x, Spawn_Dust.transform.position.y, Spawn_Dust.transform.position.z),Quaternion.identity);
                    Dusttest.transform.gameObject.SetActive(true);
                }
            }
        }
    }
    
    IEnumerator delayDust_idle(){
        isDust2 = true;
        yield return new WaitForSeconds(dustDelay);
        isDust2 = false;
    }
    IEnumerator delayDustWalk(){
        isDust = true;
        yield return new WaitForSeconds(dustDelay2);
        isDust = false;
    }
}