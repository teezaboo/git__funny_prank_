using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controll_collider : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public player_Controller player;
    public GameObject obj_pick;
    public BoxCollider2D obj_pick_collider;
    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player.isClick && player.isPick)
        {
            obj_pick.transform.position = player.transform.position;
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isActive == false){
        // ตรวจสอบว่า Collider ที่ชนกันเป็น Collider ของมอนสเตอร์หรือไม่
            if (other.gameObject.CompareTag("Entity"))
            {
                if(obj_pick != null){
                    if(obj_pick.GetComponent<BotController>().numbTime != null){
                        StopCoroutine(obj_pick.GetComponent<BotController>().numbTime);
                    }
                }
                obj_pick_collider = other.gameObject.GetComponent<BoxCollider2D>();
                player.isPick = true;
                obj_pick = other.gameObject.GetComponent<this_is_mybody>().player_obj;
                player.type_pick = 1;
            }else if (other.gameObject.CompareTag("item1")) {
                obj_pick_collider = other.gameObject.GetComponent<BoxCollider2D>();
                player.isPick = true;
                obj_pick = other.gameObject;
                player.type_item_pick = other.gameObject.GetComponent<item_controller>().type;
                player.type_pick = 2;
                //.GetComponent<item_controller>().
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(isActive == false){
            // ตรวจสอบว่า Collider ที่ออกจากพื้นที่เป็น Collider ของมอนสเตอร์หรือไม่
            if (other.gameObject.CompareTag("Entity"))
            {
                if(obj_pick != null){
                    if(obj_pick.GetComponent<BotController>().isPick == true){
                        obj_pick.GetComponent<BotController>().setAniNumb();
                    }
                    obj_pick.GetComponent<BotController>().isPick = false;
                    obj_pick = null;
                }
                if(obj_pick_collider != null){
                    obj_pick_collider = null;
                }
                player.isPick = false;
                player.isClick = false;
                player.type_pick = 0;
            }else if (other.gameObject.CompareTag("item1")) {
                if(obj_pick_collider != null){
                    obj_pick_collider = null;
                }
                player.isPick = false;
                player.isClick = false;
                obj_pick = null;
                player.type_pick = 0;
                player.skill_type = 0;
                //.GetComponent<item_controller>().
            }
        }
    }
}
