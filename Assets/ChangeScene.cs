using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // เพิ่มนี้เพื่อใช้งานคลาส Image

public class ChangeScene : MonoBehaviour
{
    float i = 255f;
    
    float j = 0f;
    public GameObject virus;
    // Start is called before the first frame update
    void Start()
    {
        virus.gameObject.SetActive(true);
        StartCoroutine(animationVirus());
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAniGotoMenu(){
        virus.gameObject.SetActive(true);
        StartCoroutine(animationToMenu());
    }
  /*  public void StartAniGotoPlay(){
        virus.gameObject.SetActive(true);
        StartCoroutine(animationVirus());
    }
    public void StartAniGotoStory(){
        virus.gameObject.SetActive(true);
        StartCoroutine(animationToStory());
    }*/
    public void StartAniGotoGame(){
        virus.gameObject.SetActive(true);
        StartCoroutine(animationToGame());
    }
   /* public void StartAniGotoLobby(){
        virus.gameObject.SetActive(true);
        StartCoroutine(animationToLobby());
    }*/
    IEnumerator animationToMenu(){
        virus.GetComponent<Image>().color = new Color(0f, 0f/255f, 0f/255f, j/255f);
        if(j < 180f){
            j = j+4f;
        }else{
            j = j+12f;
        }
        yield return new WaitForSeconds(0.025f);
        if(j <255f){
            StartCoroutine(animationToMenu());
        }else{
            virus.GetComponent<Image>().color = new Color(0f, 0f/255f, 0f/255f, 255f);
            GotoMenu();
            j = 0;
        }
    }
    IEnumerator animationToLobby(){
        virus.GetComponent<Image>().color = new Color(0f, 0f/255f, 0f/255f, j/255f);
        if(j < 180f){
            j = j+4f;
        }else{
            j = j+12f;
        }
        yield return new WaitForSeconds(0.025f);
        if(j <255f){
            StartCoroutine(animationToLobby());
        }else{
            virus.GetComponent<Image>().color = new Color(0f, 0f/255f, 0f/255f, 255f);
            GotoLobby();
            j = 0;
        }
    }

    IEnumerator animationToStory(){
        virus.GetComponent<Image>().color = new Color(0f, 0f/255f, 0f/255f, j/255f);
        if(j < 180f){
            j = j+4f;
        }else{
            j = j+12f;
        }
        yield return new WaitForSeconds(0.025f);
        if(j <255f){
            StartCoroutine(animationToStory());
        }else{
            virus.GetComponent<Image>().color = new Color(0f, 0f/255f, 0f/255f, 255f);
            GotoStory();
            j = 0;
        }
    }

    IEnumerator animationToGame(){
        virus.GetComponent<Image>().color = new Color(0f, 0f/255f, 0f/255f, j/255f);
        if(j < 180f){
            j = j+4f;
        }else{
            j = j+12f;
        }
        yield return new WaitForSeconds(0.025f);
        if(j <255f){
            StartCoroutine(animationToGame());
        }else{
            virus.GetComponent<Image>().color = new Color(0f, 0f/255f, 0/255f, 255f);
            GotoGamePlay();
            j = 0;
        }
    }


    IEnumerator animationVirus(){
        virus.GetComponent<Image>().color = new Color(virus.GetComponent<Image>().color.r, virus.GetComponent<Image>().color.g, virus.GetComponent<Image>().color.b, i/255f);
        if(i > 80f){
            i = i-4f;
        }else{
            i = i-12f;
        }
        yield return new WaitForSeconds(0.025f);
        if(i >0f){
            StartCoroutine(animationVirus());
        }else{
            virus.GetComponent<Image>().color = new Color(virus.GetComponent<Image>().color.r, virus.GetComponent<Image>().color.g, virus.GetComponent<Image>().color.b, 0f);
          //  GotoStory();
            i = 0f;
            
        virus.gameObject.SetActive(false);
        }
    }
    public void GotoLobby(){
        SceneManager.LoadScene("lobby");
    }
    public void GotoMenu(){
        SceneManager.LoadScene("myMANU");
    }
    public void GotoStory(){
        SceneManager.LoadScene("Story");
    }
    public void GotoGamePlay(){
        SceneManager.LoadScene("gameplay");
    }
}
