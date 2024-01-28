using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class this_is_mybody : MonoBehaviour
{
    public GameObject player_obj;
    public GameObject myWater;
    public GameObject myFire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("water"))
        {
            myWater.SetActive(true);
            myFire.SetActive(false);
        player_obj.GetComponent<BotController>().isWater = true;
         //   player_obj = other.gameObject;
            
           // player_obj.GetComponent<BotController>().setWater();
            //collidedObjects_tree.Add(other.gameObject);
            StartCoroutine(_delayWater());
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.CompareTag("water"))
        {
            myWater.SetActive(false);
        player_obj.GetComponent<BotController>().isWater = false;
        }
    }
    IEnumerator _delayWater(){
        yield return new WaitForSeconds(1f);
        player_obj.GetComponent<BotController>().setWater();
    }
}
