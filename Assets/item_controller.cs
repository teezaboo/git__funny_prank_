using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_controller : MonoBehaviour
{
    public int type;
    public GameObject my_mom_obj;
    public GameObject my_obj;
    public Animator item_ani;
    public GameObject myTree;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void die(){
        GetComponent<BoxCollider2D>().enabled = false;
        my_obj.SetActive(false);
        item_ani.Play("beforpick_pick");
        if(myTree != null){
                myTree.GetComponent<back_tree_box_controller>().count--;
        }
        Destroy(my_mom_obj, 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("tree_detax")){
            myTree = other.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("tree_detax")){
            myTree = null;
        }
    }
}
