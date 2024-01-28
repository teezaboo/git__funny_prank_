using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onHitLight_box3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่า Collider ที่ชนกันเป็น Collider ของมอนสเตอร์หรือไม่
        if (other.gameObject.CompareTag("Entity"))
            {
                Debug.Log("Entity");
                GameObject obj_pick = other.gameObject.GetComponent<this_is_mybody>().player_obj;
            BotController botController = obj_pick.GetComponent<BotController>();
            if (botController != null)
            {
                if(botController.isDie != true){
                 //   if(botController.isWater != true){
                        botController.setWater();
                 //   }
                }
            }
        }
        
        if (other.gameObject.CompareTag("tree"))
        {
            Debug.Log("tree");
            if(other.gameObject.GetComponent<tree_controller>().isDie != true){
                other.gameObject.GetComponent<tree_controller>().iswater();
            }
        }
        if (other.gameObject.CompareTag("building"))
        {
            Debug.Log("building");
            if(other.gameObject.GetComponent<build_controller>().isDie != true){
                other.gameObject.GetComponent<build_controller>().iswater();
            }
        }
    }
}
