using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class Yagoo : MonoBehaviour
{
    [Header("移動位置")]
    public Transform start;
    public Transform end;
    public Transform left;
    public Transform leftDown;
    public Transform leftUp;
    public Transform right;
    public Transform rightDown;
    public Transform rightUp;
    [Header("HP")]
    public int HP = 100;
    public GameObject HpBar;
    [Header("移動速度")]
    public float Speed = 0.2f;
    [Header("停頓時間")]
    public float StopTime = 5f;
    [Header("Flowchart")]
    public Flowchart flowchart;
    [Header("死亡")]
    public bool Dead = false;
    [Header("彈幕")]
    public GameObject BulletObject;
    [Header("彈幕生成點")]
    public GameObject BulletInstantiatePoint1;
    public GameObject BulletInstantiatePoint2;
    public GameObject BulletInstantiatePoint3;
    public GameObject BulletInstantiatePoint4;
    [Header("CD")]
    public float CD = 0.1f;
    [Header("移動開關")]
    public bool moveD = false;//下來
    public bool moveL = false;//往左
    public bool moveLB = false;//往左後回來
    public bool moveR = false;//往右
    public bool moveRB = false;//往右後回來
    public bool moveTest = false;
    public bool shot = false;
    [Header("進入場景時間")]
    public static float startTime;

    private List<Vector3> pointLD = new List<Vector3>();
    private List<Vector3> pointLU = new List<Vector3>();
    private List<Vector3> pointRD = new List<Vector3>();
    private List<Vector3> pointRU = new List<Vector3>();
    private int i = 1;//計算移動點用
    private int x = 0;//計算死亡次數用

    //public int moveX = 0;

    private float timer = 0;//計時器

    private void Start()
    {
        startTime = Time.time;
        moveD = true;
        HpBar.SetActive(true);
        HpBar.GetComponent<Image>().fillAmount = HP * 0.01f;

        moveLeftDown();
        moveLeftUp();
        moveRightDown();
        moveRightUp();
    }

    public void Update()
    {
        if (moveD)
        {
            inScene();
        }
        if (moveTest && !CharacterController.stop && !CharacterController.isDead)
        {
            if (moveL)
            {
                goLeft();
            }
            if (moveLB)
            {
                goLeftBack();
            }
            if (moveR)
            {
                goRight();
            }
            if (moveRB)
            {
                goRightBack();
            }
            if (shot)
            {
                timer += Time.deltaTime;
                if (timer >= CD)
                {
                    Shot();
                    timer = 0;//重置計時器
                }
            }
        }
        
        if(HP < 1)
        {
            Dead = true;
            
        }
        if (Dead && x < 1)
        {
            dead();
        }
        flowchart.SetBooleanVariable("Boss", !Dead);

    }

    public void Hit(Collider2D collision)
    {
        HP--;
        HpBar.GetComponent<Image>().fillAmount = HP * 0.01f;
        Destroy(collision.gameObject);
    }

    public void dead()
    {
        moveL = false;
        moveLB = false;
        moveR = false;
        moveRB = false;

        //flowchart.SetBooleanVariable("chatOn", true);
        flowchart.SendFungusMessage("dead");
        x++;
        Destroy(gameObject);

    }

    public void inScene()
    {
        
        transform.position = new Vector2(0, 6);
        transform.position = Vector3.Slerp(start.position, end.position, (Time.time - startTime) * Speed);
        if (transform.position == end.position)
        {


            //StartCoroutine("stop");
            
            //flowchart.SetBooleanVariable("chatOn", true);
            flowchart.SendFungusMessage("test");
            //print(flowchart.GetBooleanVariable("chatOn"));
            if (!flowchart.GetBooleanVariable("chatOn"))
            {
                moveD = false;
                moveL = true;
                moveTest = true;
                //flowchart.SetBooleanVariable("chatOn", true);
            }


        }
        
    }

    public void goLeft()
    {
        
        transform.localPosition = Vector3.Lerp(pointLD[i - 1], pointLD[i], Speed);
        i++;
        if (i >= pointLD.Count) i = 1;
        
        if (transform.position == left.position)
        {
            moveL = false;
            moveLB = true;
            i = 1;
            
        }

    }

    public void goLeftBack()
    {
        transform.localPosition = Vector3.Lerp(pointLU[i - 1], pointLU[i], Speed);
        i++;
        if (i >= pointLU.Count) i = 1;
        
        if (transform.position == end.position)
        {
            moveLB = false;
            moveR = true;
            i = 1;
        }


    }

    public void goRight()
    {
        transform.localPosition = Vector3.Lerp(pointRD[i - 1], pointRD[i], Speed);
        i++;
        if (i >= pointRD.Count) i = 1;
        
        if (transform.position == right.position)
        {
            moveR = false;
            moveRB = true;
            i = 1;
        }

    }

    public void goRightBack()
    {

        transform.localPosition = Vector3.Lerp(pointRU[i - 1], pointRU[i], Speed);
        i++;
        if (i >= pointRU.Count) i = 1;
        
        if (transform.position == end.position)
        {
            moveRB = false;
            moveL = true;
            i = 1;
        }

    }

    public void moveLeftDown()
    {
        for (int i = 0; i < 200; i++)
        {
            //一
            Vector3 pos1 = Vector3.Lerp(end.position, leftDown.position, i / 100f);
            Vector3 pos2 = Vector3.Lerp(leftDown.position, left.position, i / 100f);
            //Vector3 pos3 = Vector3.Lerp(gameOjbet_tran[2].position, left.position, i / 100f);
            //Vector3 pos4 = Vector3.Lerp(gameOjbet_tran[3].position, gameOjbet_tran[4].position, i / 100f);


            //二
            var pos1_0 = Vector3.Lerp(pos1, pos2, i / 100f);
            //var pos1_1 = Vector3.Lerp(pos2, pos3, i / 100f);
            //var pos1_2 = Vector3.Lerp(pos3, pos4, i / 100f);

            //三
            //var pos2_0 = Vector3.Lerp(pos1_0, pos1_1, i / 100f);
            //var pos2_1 = Vector3.Lerp(pos1_1, pos1_2, i / 100f);

            //四
            //Vector3 find = Vector3.Lerp(pos2_0, pos2_1, i / 100f);
            Vector3 find = pos1_0;

            pointLD.Add(find);
        }
    }

    public void moveLeftUp()
    {
        for (int i = 0; i < 200; i++)
        {
            //一
            Vector3 pos1 = Vector3.Lerp(left.position, leftUp.position, i / 100f);
            Vector3 pos2 = Vector3.Lerp(leftUp.position, end.position, i / 100f);


            //二
            var pos1_0 = Vector3.Lerp(pos1, pos2, i / 100f);
            
            Vector3 find = pos1_0;

            pointLU.Add(find);
        }
    }

    public void moveRightDown()
    {
        for (int i = 0; i < 200; i++)
        {
            //一
            Vector3 pos1 = Vector3.Lerp(end.position, rightDown.position, i / 100f);
            Vector3 pos2 = Vector3.Lerp(rightDown.position, right.position, i / 100f);


            //二
            var pos1_0 = Vector3.Lerp(pos1, pos2, i / 100f);

            Vector3 find = pos1_0;

            pointRD.Add(find);
        }
    }

    public void moveRightUp()
    {
        for (int i = 0; i < 200; i++)
        {
            //一
            Vector3 pos1 = Vector3.Lerp(right.position, rightUp.position, i / 100f);
            Vector3 pos2 = Vector3.Lerp(rightUp.position, end.position, i / 100f);

            //二
            var pos1_0 = Vector3.Lerp(pos1, pos2, i / 100f);

            Vector3 find = pos1_0;

            pointRU.Add(find);
        }
    }

    void OnDrawGizmos()//畫線
    {
        moveLeftDown();
        moveLeftUp();
        moveRightDown();
        moveRightUp();
        Gizmos.color = Color.yellow;
        for (int i = 0; i < pointLD.Count - 1; i++)
        {
            Gizmos.DrawLine(pointLD[i], pointLD[i + 1]);
        }
        for (int i = 0; i < pointLU.Count - 1; i++)
        {
            Gizmos.DrawLine(pointLU[i], pointLU[i + 1]);
        }
        for (int i = 0; i < pointRD.Count - 1; i++)
        {
            Gizmos.DrawLine(pointRD[i], pointRD[i + 1]);
        }
        for (int i = 0; i < pointRU.Count - 1; i++)
        {
            Gizmos.DrawLine(pointRU[i], pointRU[i + 1]);
        }
    }

    public void Shot()
    {
        Instantiate(BulletObject, BulletInstantiatePoint1.transform.position, BulletInstantiatePoint1.transform.rotation);
        Instantiate(BulletObject, BulletInstantiatePoint2.transform.position, BulletInstantiatePoint2.transform.rotation);
        Instantiate(BulletObject, BulletInstantiatePoint3.transform.position, BulletInstantiatePoint3.transform.rotation);
        Instantiate(BulletObject, BulletInstantiatePoint4.transform.position, BulletInstantiatePoint4.transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet")
        {
            Hit(collision);
        }
    }

}
