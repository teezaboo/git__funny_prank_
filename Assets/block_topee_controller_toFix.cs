using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block_topee_controller_toFix : MonoBehaviour
{
    public GameObject obj_pick;
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
        if (other.gameObject.CompareTag("Entity")){
            obj_pick = other.gameObject.GetComponent<this_is_mybody>().player_obj;
            if(obj_pick.GetComponent<BotController>().isfire != true){
                if(obj_pick.GetComponent<BotController>().isFix == true){
                    obj_pick.GetComponent<BotController>().setAniOnFix();
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Entity")){
            obj_pick = null;
        }
    }
}
