using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieauto : MonoBehaviour
{
    public float timetoDie = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(die());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator die(){
        yield return new WaitForSeconds(timetoDie);
        Destroy(gameObject);
    }
}
