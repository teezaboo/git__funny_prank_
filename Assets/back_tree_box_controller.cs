using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class back_tree_box_controller : MonoBehaviour
{
    public GameObject Tree1;
    public GameObject Tree2;
    public int count = 0;
   // private SpriteRenderer spriteRenderer;
   // public Color initialColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(count > 0){
            Tree1.SetActive(false);
            Tree2.SetActive(false);
        }else{
            Tree1.SetActive(true);
            Tree2.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Entity")){
            count++;
        }else if (other.gameObject.CompareTag("item1")){
            count++;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Entity")){
            count--;
        }else if (other.gameObject.CompareTag("item1")){
            count--;
        }
    }
}
