using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;


public class mainController : MonoBehaviour
{
    public main_controller_laugh your_score;
    public float time = 0f;
    string formattedTime;
    TimeSpan timeSpan;
    public player_Controller player_Controller_my;
    public bool stopGameMenu = false;
    public GameObject pauseMenuUI;
    public GameObject dieMenuUI;
    private bool isPaused = false;    
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Texture2D cursorTexture_menu;
    private Vector2 cursorHotspot;
    public TextMeshProUGUI score_text;
    public TextMeshProUGUI end_time_text;
    public TextMeshProUGUI text_time;
    public TextMeshProUGUI text_time2;
    public int item_deee;
    void Start()
    {
        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }
    public void TimeRun(){
        Time.timeScale = 1f; // เริ่มเวลาในเกมใหม่
    }

    void Update()
    {
        // นับเวลาที่ผ่านไป
        time += Time.deltaTime ;

        // แปลงเวลาที่นับได้เป็นรูปแบบของ string "HH:mm:ss" (ชั่วโมง:นาที:วินาที)
        timeSpan = TimeSpan.FromSeconds(time);
        formattedTime = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        text_time.text = formattedTime;
        text_time2.text = formattedTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (timeSpan.TotalMinutes == 1)
        {
            item_deee = 5;
        }else if (timeSpan.TotalMinutes == 2)
        {
            item_deee = 8;
        }else if (timeSpan.TotalMinutes == 3)
        {
            item_deee = 14;
        }else if (timeSpan.TotalMinutes >= 4)
        {
            item_deee = 20;
        }else if (timeSpan.TotalMinutes >= 5)
        {
            item_deee = 25;
        }else if (timeSpan.TotalMinutes >= 6)
        {
            item_deee = 40;
        }else if (timeSpan.TotalMinutes >= 7)
        {
            item_deee = 50;
        }else if (timeSpan.TotalMinutes >= 8)
        {
            item_deee = 60;
        }else if (timeSpan.TotalMinutes >= 9)
        {
            item_deee = 80;
        }
    }

    public void Pause()
    {
        player_Controller_my.isMENUIN();
        Time.timeScale = 0f; // หยุดเวลาในเกม
        pauseMenuUI.SetActive(true);
        isPaused = true;
        Cursor.SetCursor(cursorTexture_menu, cursorHotspot, CursorMode.Auto);
    }
    public void dieMenu()
    {
        player_Controller_my.isMENUIN();
        dieMenuUI.SetActive(true);
        isPaused = true;
        Cursor.SetCursor(cursorTexture_menu, cursorHotspot, CursorMode.Auto);
        timeSpan = TimeSpan.FromSeconds(time);
        formattedTime = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        end_time_text.text = formattedTime;

        if(your_score.laugh_point < 0){
            your_score.isNev = true;
        }
        if(your_score.isNev){
            score_text.text = "-"+AddCommasToNumber2(your_score.laugh_point*-1).ToString();
        }else{
            
            score_text.text = AddCommasToNumber2(your_score.laugh_point).ToString();
        }


        Time.timeScale = 0f; // หยุดเวลาในเกม
    }

    public void Resume()
    {
        player_Controller_my.isMENUOUT();
        Time.timeScale = 1f; // เริ่มเวลาในเกมใหม่
        pauseMenuUI.SetActive(false);
        isPaused = false;
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }
    
string AddCommasToNumber2(int number)
{
    string numberString = number.ToString();
    
    // ตรวจสอบว่าตัวเลขมีมากกว่า 3 หลักหรือไม่
    if (numberString.Length > 3)
    {
        StringBuilder result = new StringBuilder();

        // หากมีทศนิยม
        int decimalIndex = numberString.IndexOf('.');
        string decimalPart = "";
        if (decimalIndex != -1)
        {
            decimalPart = numberString.Substring(decimalIndex);
            numberString = numberString.Substring(0, decimalIndex);
        }

        // ลูปทุกรอบ 3 หลัก
        int count = 0;
        for (int i = numberString.Length - 1; i >= 0; i--)
        {
            result.Insert(0, numberString[i]);
            count++;

            if (count == 3 && i > 0)
            {
                result.Insert(0, ",");
                count = 0;
            }
        }

        // เพิ่มทศนิยมกลับ
        result.Append(decimalPart);

        numberString = result.ToString();
    }

    return numberString;
}
}