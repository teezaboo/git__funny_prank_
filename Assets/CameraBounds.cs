using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public Transform target; // เลือก GameObject ที่ต้องการให้กล้องติดตาม

    public float minX; // ขอบเขตต่ำสุดในแกน X
    public float maxX; // ขอบเขตสูงสุดในแกน X
    public float minY; // ขอบเขตต่ำสุดในแกน Y
    public float maxY; // ขอบเขตสูงสุดในแกน Y

    public float smoothTime = 0.3f; // เวลาที่ใช้ในการเคลื่อนที่ที่เรียกว่า SmoothDamp

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target != null)
        {
            // หาตำแหน่งใหม่ของกล้อง
            float targetX = Mathf.Clamp(target.position.x, minX, maxX);
            float targetY = Mathf.Clamp(target.position.y, minY, maxY);

            // สร้าง Vector3 ใหม่
            Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

            // เคลื่อนที่กล้องไปยังตำแหน่งใหม่
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
