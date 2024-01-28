using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colider_detex_npc : MonoBehaviour
{
    public GameObject player_obj;
    public BotController contro_player_obj;
    // Start is called before the first frame update
    public List<GameObject> collidedObjects_tree = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("tree"))
        {
            collidedObjects_tree.Add(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.CompareTag("tree"))
        {
            collidedObjects_tree.Remove(other.gameObject);
        }
    }
}
