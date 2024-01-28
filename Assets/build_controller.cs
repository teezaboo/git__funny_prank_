using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build_controller : MonoBehaviour
{
    public bool isfire;
    public GameObject fireOBJ;
    public GameObject Org_tree;
    public GameObject die_tree;
    public Coroutine Todie;
    public bool isDie;
    public bool isRuningFix;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void _isfire(){
        Debug.Log("this is bug");
            isfire = true;
            fireOBJ.SetActive(true);
            Todie = StartCoroutine(_delayTodie());
    }
    public void iswater(){
        if(isfire == true){
            if(Todie != null){
                StopCoroutine(Todie);
            }
            isDie = false;
            isfire = false;
        }
        fireOBJ.SetActive(false);
    }
    public void fix(){
        if(isDie == true){
            if(Todie != null){
                StopCoroutine(Todie);
            }
            isDie = false;
            isfire = false;
            Org_tree.SetActive(true);
            die_tree.SetActive(false);
            Debug.Log("fixed");
            isRuningFix = false;
        }
        fireOBJ.SetActive(false);
    }
    public void die(){
        if(isDie != true){
            isDie = true;
            isfire = false;
            Org_tree.SetActive(false);
            die_tree.SetActive(true);
            if(Todie != null){
                StopCoroutine(Todie);
            }
        }
        fireOBJ.SetActive(false);
    }
    IEnumerator _delayTodie()
    {
        yield return new WaitForSeconds(5f);
        die();
    }
}
