using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BotController : MonoBehaviour
{
    public bool isStop = false;
    public float moveSpeed = 2.0f; // ความเร็วในการเคลื่อนที่
    public float radius = 5.0f; // รัศมีวงกลม
    public float delayTime = 1.0f; // เวลาที่หยุดหลังจากถึงเป้าหมาย
    public Animator npc_ani;    
    public bool Runed;
    public bool isPick;
    public bool isAniPick = false;
    public bool isNumb = false;
    public float delayNumb = 1.5f;
    public Coroutine numbTime;
    private Vector2 center;
    private Vector2 targetPosition;
    private bool isMoving = false;
    public GameObject ObjFilp;
    public Collider2D my_Collider2D;
    public bool isPanic;
    public bool isDie;
    public bool isDieReal;
    public Coroutine numbTime2;
    public Coroutine numbTime3;
    public Coroutine loopPeeTime;
    public Coroutine loopFixTime;
    public main_controller_laugh controller_laugh;
    public bool isfire;
    public bool isWater;
    public List<GameObject> waterOBJ;
    public List<GameObject> treeOBJ;
    public List<GameObject> buildOBJ;
    float closestDistance = float.MaxValue;
    float closestDistance2 = float.MaxValue;
    float closestDistance3 = float.MaxValue;
    GameObject myWater = null;
    GameObject myPee = null;
    public GameObject myFix = null;
    public GameObject myfire;
    public bool isPee;
    public bool isOnPee;
    public Camera mainCamera;
    public Transform playerTransform;
    public bool isInCamera;
    public bool isFix;


    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("ไม่พบ Main Camera ในฉาก");
            return;
        }
     //   setAniFix();
       // setAniPee();
        loopPeeTime = StartCoroutine(loopPee());
        loopFixTime = StartCoroutine(loopFix());
        setR_inMove();
        SetNewRandomTarget();
    }

    void Update()
    {
        if(isDie != true){
            if(isNumb != true){
                if(isPick != true){
                    if (isMoving)
                    {
                        MoveToTarget();
                    }
                }
            }
            if(isPick){
                my_Collider2D.enabled = false;
            }else{
                my_Collider2D.enabled = true;
            }
        }else{
            if(isDieReal == true){
                npc_ani.Play("playedie");
            }
        }
        if (IsPlayerInsideCameraView())
        {
            isInCamera = true;
            // ทำสิ่งที่คุณต้องการทำเมื่อผู้เล่นอยู่ในมุมกล้อง
        }else{
            isInCamera = false;
        }
    }
    bool IsPlayerInsideCameraView()
    {
        if (mainCamera == null || playerTransform == null)
        {
            return false;
        }

        // ดึงตำแหน่งของผู้เล่นและตำแหน่งของมุมกล้อง
        Vector3 playerPosition = playerTransform.position;
        Vector3 cameraPosition = mainCamera.transform.position;

        // ตรวจสอบว่าตำแหน่งของผู้เล่นอยู่ภายในขอบเขตของ Main Camera หรือไม่
        return playerPosition.x >= cameraPosition.x - mainCamera.orthographicSize * mainCamera.aspect &&
               playerPosition.x <= cameraPosition.x + mainCamera.orthographicSize * mainCamera.aspect &&
               playerPosition.y >= cameraPosition.y - mainCamera.orthographicSize &&
               playerPosition.y <= cameraPosition.y + mainCamera.orthographicSize;
    }
    public void seeMyWater(){
        foreach (GameObject obj in waterOBJ)
        {
            if (obj != null)  // ตรวจสอบว่า obj ไม่ได้ถูกทำลาย
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    myWater = obj;
                }
            }
        }
    }
    public void seeMyPee(){
        foreach (GameObject obj in treeOBJ)
        {
            if (obj != null)  // ตรวจสอบว่า obj ไม่ได้ถูกทำลาย
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                if (distance < closestDistance2)
                {
                    closestDistance2 = distance;
                    myPee = obj;
                }
            }
        }
    }
    public void seeMyFix(){
        foreach (GameObject obj in buildOBJ)
        {
            if (obj != null)  // ตรวจสอบว่า obj ไม่ได้ถูกทำลาย
            {
                    Debug.Log("Hello check");
                    if(obj.GetComponent<state_build>().My_build_controller != null){
                        Debug.Log(obj.GetComponent<state_build>().My_build_controller.isDie);
                    }
                if(obj.GetComponent<state_build>().My_build_controller.isDie == true && obj.GetComponent<state_build>().My_build_controller.isRuningFix != true){
                    Debug.Log("Hello isDie");
                    float distance = Vector3.Distance(transform.position, obj.transform.position);
                    if (distance < closestDistance3)
                    {
                        closestDistance3 = distance;
                        isFix = true;
                        myFix = obj;
                   /*     myFix = obj;
                        isFix = true;
                        Runed = true;
                        isAniPick = false;
                        isNumb = false;
                        isPanic = true;
                        npc_ani.Play("panic_move");*/
                    }
                }
            }
        }
        if(myFix == null){
            loopFixTime = StartCoroutine(loopFix());
        }else{
            myFix.GetComponent<state_build>().My_build_controller.isRuningFix = true;
            isFix = true;
            isPanic = true;
            Runed = true;
            isAniPick = false;
            isNumb = false;
            npc_ani.Play("panic_move");
        }
    }

    public void SetNewRandomTarget()
    {
        // สุ่มตำแหน่งเป้าหมายภายในวงกลม
        float randomAngle = UnityEngine.Random.Range(0f, 2 * Mathf.PI);
        float x = center.x + Mathf.Cos(randomAngle) * radius;
        float y = center.y + Mathf.Sin(randomAngle) * radius;

        targetPosition = new Vector2(x, y);
        isMoving = true;
        if(isPanic != true){
            UpdateAnimator();
        }
    }
    void MoveToTarget()
    {
        // คำนวณทิศทางไปยังเป้าหมาย
        if(isfire != true){
            if(isFix != true){
                if(isPee != true){
                    Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

                    // เคลื่อนที่ไปยังเป้าหมาย
                    if(isPanic != true){
                        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
                    }else{
                        transform.position += (Vector3)(direction * moveSpeed*2 * Time.deltaTime);
                    }
                    // ตรวจสอบทิศทางเคลื่อนที่
                    if (direction.x > 0)
                    {
                        // ตัวละครเคลื่อนที่ไปทางขวา
                        if(transform.localScale.x < 0){
                            transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
                            
                            if(ObjFilp.transform.localScale.x < 0){
                                ObjFilp.transform.localScale =  new Vector3(ObjFilp.transform.localScale.x*-1, ObjFilp.transform.localScale.y, ObjFilp.transform.localScale.z);
                            }
                        }
                    }
                    else if (direction.x < 0)
                    {
                        // ตัวละครเคลื่อนที่ไปทางซ้าย
                        if(transform.localScale.x > 0){
                            transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
                            if(ObjFilp.transform.localScale.x > 0){
                                ObjFilp.transform.localScale =  new Vector3(ObjFilp.transform.localScale.x*-1, ObjFilp.transform.localScale.y, ObjFilp.transform.localScale.z);
                            }
                        }
                    }


                    // ตรวจสอบว่าเคลื่อนที่ถึงเป้าหมายแล้วหรือไม่
                    if(isfire != true){
                        if (Vector2.Distance(transform.position, targetPosition) < 0.1f && isPanic != true)
                        {
                            isMoving = false;
                            if(UnityEngine.Random.Range(0f, 100f) < 20f){
                                setAniSleep();
                            }else{
                                StartCoroutine(DelayBeforeNewTarget());
                                UpdateAnimator();
                            }
                        }else if(Vector2.Distance(transform.position, targetPosition) < 0.1f && isPanic == true){
                            SetNewRandomTarget();
                        }
                    }
                }else{
                    if(myPee.transform.position.x > transform.position.x){
                        if(transform.localScale.x < 0){
                            transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
                        }
                    /* if(myfire.transform.localScale.x < 0){
                            myfire.transform.localScale = new Vector3(myfire.transform.localScale.x*-1, myfire.transform.localScale.y, myfire.transform.localScale.z);
                        }*/
                    }else{
                        if(transform.localScale.x > 0){
                            transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
                        }
                    /* if(myfire.transform.localScale.x > 0){
                            myfire.transform.localScale = new Vector3(myfire.transform.localScale.x*-1, myfire.transform.localScale.y, myfire.transform.localScale.z);
                        }*/
                    }
                    Vector2 direction = ((Vector2)myPee.transform.position - (Vector2)transform.position).normalized;
                    transform.position += (Vector3)(direction * moveSpeed*2 * Time.deltaTime);
                }
            }else{
                if(myFix.transform.position.x > transform.position.x){
                   // Debug.Log("myFix");
                    if(transform.localScale.x < 0){
                        transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
                    }
                   /* if(myfire.transform.localScale.x < 0){
                        myfire.transform.localScale = new Vector3(myfire.transform.localScale.x*-1, myfire.transform.localScale.y, myfire.transform.localScale.z);
                    }*/
                }else{
                    if(transform.localScale.x > 0){
                        transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
                    }
                   /* if(myfire.transform.localScale.x > 0){
                        myfire.transform.localScale = new Vector3(myfire.transform.localScale.x*-1, myfire.transform.localScale.y, myfire.transform.localScale.z);
                    }*/
                }
                Vector2 direction = ((Vector2)myFix.transform.position - (Vector2)transform.position).normalized;
                transform.position += (Vector3)(direction * moveSpeed*2 * Time.deltaTime);
            }
        }else{
            if(myWater.transform.position.x > transform.position.x){
                if(transform.localScale.x < 0){
                    transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
                }
                if(myfire.transform.localScale.x < 0){
                    myfire.transform.localScale = new Vector3(myfire.transform.localScale.x*-1, myfire.transform.localScale.y, myfire.transform.localScale.z);
                }
            }else{
                if(transform.localScale.x > 0){
                    transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
                }
                if(myfire.transform.localScale.x > 0){
                    myfire.transform.localScale = new Vector3(myfire.transform.localScale.x*-1, myfire.transform.localScale.y, myfire.transform.localScale.z);
                }
            }
            Vector2 direction = ((Vector2)myWater.transform.position - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed*2 * Time.deltaTime);
        }
    }   
    public void setR_inMove(){
        center = transform.position;
    }


    IEnumerator DelayBeforeNewTarget()
    {
        yield return new WaitForSeconds(delayTime);
        SetNewRandomTarget();
    }
    IEnumerator loopPee()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f * Mathf.PI));
        if(UnityEngine.Random.Range(0f, 100f) < 5f && isfire != true){
            setAniPee();
        }else{
            loopPeeTime = StartCoroutine(loopPee());
        }
    }
    IEnumerator loopFix()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f * Mathf.PI));
        if(UnityEngine.Random.Range(0f, 100f) < 100f && isfire != true){
            setAniFix();
            //loopFixTime = StartCoroutine(loopFix());
        }else{
            loopFixTime = StartCoroutine(loopFix());
        }
    }
    
    public void UpdateAnimator()
    {
        if(isDie != true){
        if (isStop == false && isAniPick == false)
        {
            if (isMoving && Runed != true)
            {
                npc_ani.Play("move");
                Runed = true;
                isAniPick = false;
            }
            else if (isMoving == false && Runed == true)
            {
                npc_ani.Play("idle");
                Runed = false;
                isAniPick = false;
            }
        }
        }
    }
    public void setAniOnpick(){
        if(isDie != true){
            npc_ani.Play("onpick");
            isAniPick = true;
        }
    }
    public void setAniOnpickOnPee(){
        if(isDie != true){
            npc_ani.Play("onpick_onpee");
            isAniPick = true;
        }
    }

    public void setAniMove(){
        if(isDie != true){
            npc_ani.Play("move");
            Runed = true;
            isAniPick = false;
        }
    }
    public void setAniLight(){
            if(numbTime != null){
                StopCoroutine(numbTime);
            }
            if(numbTime2 != null){
                StopCoroutine(numbTime2);
            }
            if(numbTime3 != null){
                StopCoroutine(numbTime3);
            }
        if(isDie != true){
            Debug.Log("onlight");
            npc_ani.Play("onlight");
            Runed = true;
            isAniPick = false;
            numbTime = StartCoroutine(_delayAnilight());
        }
    }
    public void setAniNumb(){
            if(numbTime != null){
                StopCoroutine(numbTime);
            }
            if(numbTime2 != null){
                StopCoroutine(numbTime2);
            }
            if(numbTime3 != null){
                StopCoroutine(numbTime3);
            }
        if(isDie != true){
            numbTime = StartCoroutine(_delayNumb());
        }
    }
    IEnumerator _delayNumb()
    {
        if(isDie != true){
            isNumb = true;
            npc_ani.Play("numb");
            yield return new WaitForSeconds(delayNumb);
            isNumb = false;
            setAniMove();
            if(isfire){
                npc_ani.Play("panic_move");
            }else{
                npc_ani.Play("move");
            }
            if(isInCamera){
                controller_laugh.chagne_laugh(20);
            }
        }
    }
    public void setAniSleep(){
       // setR_inMove();
       isPick = false;
        SetNewRandomTarget();
            if(numbTime != null){
                StopCoroutine(numbTime);
            }
            if(numbTime2 != null){
                StopCoroutine(numbTime2);
            }
            if(numbTime3 != null){
                StopCoroutine(numbTime3);
            }
        if(isDie != true){
            numbTime = StartCoroutine(_delaySleep());
        }
    }
    IEnumerator _delaySleep()
    {
        if(isDie != true){
            isNumb = true;
            npc_ani.Play("sleep");
            yield return new WaitForSeconds(5f);
            isNumb = false;
            
         //   StartCoroutine(DelayBeforeNewTarget());
          //  UpdateAnimator();
            //SetNewRandomTarget();
            setAniMove();
            if(isfire){
                npc_ani.Play("panic_move");
            }else{
                npc_ani.Play("move");
            }
            if(isInCamera){
                controller_laugh.chagne_laugh(20);
            }
        }
    }
    public void setAniOnPee(){
            if(numbTime != null){
                StopCoroutine(numbTime);
            }
            if(numbTime2 != null){
                StopCoroutine(numbTime2);
            }
            if(numbTime3 != null){
                StopCoroutine(numbTime3);
            }
            if(loopPeeTime != null){
                StopCoroutine(loopPeeTime);
            }
            if(loopFixTime != null){
                StopCoroutine(loopFixTime);
            }
        if(isDie != true){
            numbTime = StartCoroutine(_delayOnPee());
        }
    }
    public void setAniOnFix(){
            if(numbTime != null){
                StopCoroutine(numbTime);
            }
            if(numbTime2 != null){
                StopCoroutine(numbTime2);
            }
            if(numbTime3 != null){
                StopCoroutine(numbTime3);
            }
            if(loopPeeTime != null){
                StopCoroutine(loopPeeTime);
            }
            if(loopFixTime != null){
                StopCoroutine(loopFixTime);
            }
        if(isDie != true){
            loopFixTime = StartCoroutine(_delayOnFix());
        }
    }
    public void setStopOnPee(){
            if(numbTime != null){
                StopCoroutine(numbTime);
            }
            if(numbTime2 != null){
                StopCoroutine(numbTime2);
            }
            if(numbTime3 != null){
                StopCoroutine(numbTime3);
            }
            isPee = false;
            isNumb = false;
            setAniMove();
            if(isfire){
                npc_ani.Play("panic_move");
            }else{
                npc_ani.Play("move");
            }
            if(isInCamera){
                controller_laugh.chagne_laugh(50);
            }

    }
    public void setAniDie(int typedie){
        if(isDie != true){
            if(numbTime != null){
                StopCoroutine(numbTime);
            }
            if(numbTime2 != null){
                StopCoroutine(numbTime2);
            }
            if(numbTime3 != null){
                StopCoroutine(numbTime3);
            }
            if(loopPeeTime != null){
                StopCoroutine(loopPeeTime);
            }
            if(loopFixTime != null){
                StopCoroutine(loopFixTime);
            }
            myfire.SetActive(false);
            isPanic = false;
            isPee = false;
            Runed = false;
            isAniPick = false;
            isNumb = true;
            isDie = true;
            if(typedie == 1){
                npc_ani.Play("onlight_die");
            }else if(typedie == 2){
                npc_ani.Play("onfire_die");
            }else if(typedie == 3){
                npc_ani.Play("onwater_die");
            }
            if(isInCamera){
                controller_laugh.chagne_laugh(-200);
            }
        }
    }
    IEnumerator _delayOnPee()
    {
        if(isDie != true){
            isOnPee = true;
            isNumb = true;
            npc_ani.Play("inpee");
            yield return new WaitForSeconds(3f);
            isOnPee = false;
            isPee = false;
            isNumb = false;
            isPanic = false;
            setAniMove();
            loopPeeTime = StartCoroutine(loopPee());
            loopFixTime = StartCoroutine(loopFix());
            if(isfire){
                npc_ani.Play("panic_move");
                Debug.Log("1231231231349848489489485");
            }else{
                npc_ani.Play("move");
            }
        }
    }
    IEnumerator _delayOnFix()
    {
        if(isDie != true){
            isFix = false;
            isOnPee = false;
            isNumb = true;
            npc_ani.Play("fix");
            yield return new WaitForSeconds(5f);
            Debug.Log("T888");
            isFix = false;
            isNumb = false;
            isPanic = false;
            setAniMove();
            myFix.GetComponent<state_build>().My_build_controller.fix();
            Debug.Log("T999");
            myFix = null;
            loopFixTime = StartCoroutine(loopFix());
            loopPeeTime = StartCoroutine(loopPee());
                //StopCoroutine(numbTime);
            if(isfire){
                npc_ani.Play("panic_move");
                Debug.Log("49848489489485");
            }else{
                npc_ani.Play("move");
            }
        }
    }
    IEnumerator _delaydieee()
    {
        yield return new WaitForSeconds(0.7f);
        isDieReal = true;

    }
    IEnumerator _delayAnilight()
    {
        if(isDie != true){
            if(numbTime != null){
                StopCoroutine(numbTime);
            }
            if(numbTime2 != null){
                StopCoroutine(numbTime2);
            }
            if(numbTime3 != null){
                StopCoroutine(numbTime3);
            }
            isNumb = true;
            yield return new WaitForSeconds(0.5f);
            
            isNumb = false;
            if(isInCamera){
                controller_laugh.chagne_laugh(50);
            }
            numbTime2 = StartCoroutine(_delayPanic());
        }
    }
    public void setAniFire(){
        seeMyWater();
        myfire.SetActive(true);
        isfire = true;
        Runed = true;
        isAniPick = false;
        isNumb = false;
        if(isInCamera){
            controller_laugh.chagne_laugh(50);
        }
        isPanic = true;
        npc_ani.Play("panic_move");
        //    controller_laugh.chagne_laugh(50);
    }
    public void setAniPee(){
        seeMyPee();
      //  isfire = true;
        isPee = true;
        Runed = true;
        isAniPick = false;
        isNumb = false;
        //controller_laugh.chagne_laugh(50);
        isPanic = true;
        npc_ani.Play("panic_move");
                Debug.Log("8888889789789879879");
        //    controller_laugh.chagne_laugh(50);
    }
    public void setAniFix(){
        seeMyFix();
      //  isfire = true;
        //isFix = true;
      //  isPanic = true;
      //  npc_ani.Play("panic_move");
            //    Debug.Log("7777776456456");
        //    controller_laugh.chagne_laugh(50);
    }
    public void setWater(){
        myfire.SetActive(false);
        isfire = false;
        Runed = false;
        isAniPick = false;
        isNumb = false;
        if(isInCamera){
            controller_laugh.chagne_laugh(20);
        }
        isPanic = false;
        npc_ani.Play("idle");
        numbTime3 = StartCoroutine(DelayBeforeNewTarget());
          //  controller_laugh.chagne_laugh(50);
    }
    
    IEnumerator _delayPanic(){
        if(isDie != true){
            isPanic = true;
            npc_ani.Play("panic_move");
            yield return new WaitForSeconds(4f);
            isPanic = false;
            npc_ani.Play("idle");
            Runed = false;
            isAniPick = false;
            numbTime3 = StartCoroutine(DelayBeforeNewTarget());
        }
    }
}

