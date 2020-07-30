using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLocation : MonoBehaviour
{
    [Header("起點")]
    public GameObject Start1;//起點
    public GameObject Start2;//起點
    public GameObject Start3;//起點
    public GameObject Start4;//起點
    [Header("終點")]
    public GameObject End1;//終點
    public GameObject End2;//終點
    public GameObject End3;//終點
    public GameObject End4;//終點
    [Header("要移動的物件")]
    public GameObject Obj;//要移動的物件
    [Header("速度")]
    public float speed = 0.2f;//移動速度
    [Header("行動模式")]
    public bool doMove1 = false;
    public bool doMove2 = false;
    public bool doMove3 = false;
    public bool doMove4 = false;
    public bool doRotate1 = false;
    public bool doRotate2 = false;
    public bool doRotate3 = false;
    public bool doRotate4 = false;
    [Header("選轉角度")]
    public Quaternion targetAngels;
    [Header("計時器")]
    public float timeF = 0;
    public float timeStop = 0;
    [Header("上一次的位置")]
    public Transform lastLocation;

    private void Start()
    {
        //print(timeF);
        doMove1 = true;
        // Quaternion.Slerp()第二个参数需要的是四元数,所以这里需要将目标的角度转成四元数去计算
        targetAngels = Quaternion.Euler(0, 90f, 0);
    }

    private void Update()
    {
        timeF = Time.time - timeStop;

        if (doMove1)
        {
            MoveRoad(Start1, End1);
        }
        if (doMove2)
        {
            MoveRoad(Start2, End2);
        }
        if (doMove3)
        {
            MoveRoad(Start3, End3);
        }
        if (doMove4)
        {
            MoveRoad(Start4, End4);
        }

        if (doRotate1)
        {
            MoveCurve(Start2);
        }
        if (doRotate2)
        {
            MoveCurve(Start3);
        }
        if (doRotate3)
        {
            MoveCurve(Start4);
        }
        if (doRotate4)
        {
            MoveCurve(Start1);
        }

        //當目標物件到達第一段路徑終點
        if (Obj.transform.position == End1.transform.position)
        {
            doMove1 = false;
            timeStop += timeF;
            lastLocation = Obj.transform;
            targetAngels = Quaternion.Euler(0, 90f, 0);//設置旋轉角度
            doRotate1 = true;
        }
        //當目標物件到達第二段路徑起點
        if (Obj.transform.position == Start2.transform.position)
        {
            doRotate1 = false;
            timeStop += timeF;
            lastLocation = Obj.transform;
            doMove2 = true;
        }
        //當目標物件到達第二段路徑終點
        if (Obj.transform.position == End2.transform.position)
        {
            doMove2 = false;
            timeStop += timeF;
            lastLocation = Obj.transform;
            targetAngels = Quaternion.Euler(0, 180f, 0);//設置旋轉角度
            doRotate2 = true;
        }
        //當目標物件到達第三段路徑起點
        if (Obj.transform.position == Start3.transform.position)
        {
            doRotate2 = false;
            timeStop += timeF;
            lastLocation = Obj.transform;
            doMove3 = true;
        }
        //當目標物件到達第三段路徑終點
        if (Obj.transform.position == End3.transform.position)
        {
            doMove3 = false;
            timeStop += timeF;
            lastLocation = Obj.transform;
            targetAngels = Quaternion.Euler(0, 270f, 0);//設置旋轉角度
            doRotate3 = true;
        }
        //當目標物件到達第四段路徑起點
        if (Obj.transform.position == Start4.transform.position)
        {
            doRotate3 = false;
            timeStop += timeF;
            lastLocation = Obj.transform;
            doMove4 = true;
        }
        //當目標物件到達第四段路徑終點
        if (Obj.transform.position == End4.transform.position)
        {
            doMove4 = false;
            timeStop += timeF;
            lastLocation = Obj.transform;
            targetAngels = Quaternion.Euler(0, 0f, 0);//設置旋轉角度
            doRotate4 = true;
        }
        //當目標物件到達第一段路徑起點
        if (Obj.transform.position == Start1.transform.position)
        {
            doRotate4 = false;
            timeStop += timeF;
            lastLocation = Obj.transform;
            doMove1 = true;
        }
    }
    //直線移動
    void MoveRoad(GameObject startPositionGameObject , GameObject endPositionGameObject)
    {
        Obj.transform.position = Vector3.Lerp(startPositionGameObject.transform.position, endPositionGameObject.transform.position, timeF * speed);
    }
    //彎道運動
    void MoveCurve(GameObject targetPosition)
    {
        //轉鏡頭
        Obj.transform.rotation = Quaternion.Slerp(lastLocation.transform.rotation, targetAngels, speed * timeF);
        //物件移動
        Obj.transform.position = Vector3.Lerp(lastLocation.transform.position, targetPosition.transform.position, timeF * speed);
    }
}
