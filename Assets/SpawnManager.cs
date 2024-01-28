using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] itemPrefabs; // สร้าง public array สำหรับเก็บ monsterPrefab
    private Camera mainCamera;
    private float minX, maxX, minY, maxY;

    private void Start()
    {
        mainCamera = Camera.main;

        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

    }

    public void SpawnMonster(int typeItem)
    {
        float minDistance = 4.0f;

        Vector3 randomPosition;
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        randomPosition = new Vector3(randomX, randomY, 0);

        // สุ่ม monsterPrefab จาก itemPrefabs array
        Debug.Log("OGG ");
        Debug.Log(typeItem);
        GameObject selectedMonsterPrefab = itemPrefabs[typeItem];

        // ตรวจสอบว่า selectedMonsterPrefab ไม่เป็น null ก่อนที่จะสร้าง
        if (selectedMonsterPrefab != null)
        {
            // สร้างมอนสเตอร์ที่ตำแหน่งที่สุ่มและไม่ใกล้เกินระยะห่างขั้นต่ำ
            GameObject mon = Instantiate(selectedMonsterPrefab, randomPosition, Quaternion.identity);

            // ตั้งค่าแกน Z เป็น 0
            mon.transform.position = new Vector3(mon.transform.position.x, mon.transform.position.y, 0);

            // ไม่มีการหมุน
            mon.transform.rotation = Quaternion.identity;

            mon.SetActive(true);
        }
        else
        {
            Debug.LogWarning("selectedMonsterPrefab is null. Make sure to assign valid prefabs to the itemPrefabs array.");
        }
    }

}