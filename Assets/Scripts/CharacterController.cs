using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class CharacterController : MonoBehaviour
{
    [Header("移動速度")]
    public float speed = 0.025f;
    [Header("慢速速度")]
    public float slowSpeed = 0.01f;
    [Header("彈幕生成點")]
    public GameObject BulletInstantiatePoint1;
    public GameObject BulletInstantiatePoint2;
    [Header("彈幕物件")]
    public GameObject BulletObject;
    [Header("CD")]
    public float CD = 0.1f;
    [Header("暫停選單")]
    public GameObject escCanvas;
    [Header("過關選單")]
    public GameObject passCanvas;
    [Header("死亡選單")]
    public GameObject deadCanvas;
    [Header("暫停")]
    public static bool stop = false;
    [Header("慢速模式")]
    public bool slowMod = false;
    [Header("Flowchart")]
    public Flowchart flowchart;
    [Header("對話框運行中")]
    public bool flowchatUse = false;
    [Header("HP")]
    public int HP = 3;
    public List<GameObject> hpList = new List<GameObject>();
    [Header("死亡")]
    public static bool isDead = false;
    [Header("AudioSource")]
    public AudioSource aud;
    [Header("受傷音效")]
    public AudioClip hitFX;

    private float timer = 0;//計時器
    private Rigidbody2D rb;

    void Start()
    {
        isDead = false;
        stop = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        Time.timeScale = 2;
    }

    void Update()
    {
        print("暫停狀態"+stop);
        print("死亡狀態"+isDead);
        //取得flowchat變數值
        flowchatUse = flowchart.GetBooleanVariable("chatOn");
        //當沒有在進行對話時才能動作
        if (!stop && !flowchatUse && flowchart.GetBooleanVariable("Boss") && !isDead)
        {
            if (Input.GetKey("up"))
            {
                Move(1, 0);
            }
            if (Input.GetKey("down"))
            {
                Move(-1, 0);
            }
            if (Input.GetKey("left"))
            {
                Move(0, -1);
            }
            if (Input.GetKey("right"))
            {
                Move(0, 1);
            }
            if (Input.GetKey("left shift"))
            {
                slowMod = true;
                shotMod();
            }
            else
            {
                slowMod = false;
                shotMod();
            }

            //射擊
            if (Input.GetKey("z"))
            {
                timer += Time.deltaTime;
                if (timer >= CD)
                {
                    Shot();
                    timer = 0;//重置計時器
                }
            }
        }




        //暫停選單
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!stop)
            {
                stop = true;
                Stop(stop);
                Time.timeScale = 0;
            }
            else
            {
                stop = false;
                Stop(stop);
                Time.timeScale = 2f;
            }
            
        }

        //結束選單
        if (flowchart.GetBooleanVariable("Pass"))
        {
            Invoke("Pass", 0.5f);
        }

        if (flowchart.GetBooleanVariable("PlayerDead"))
        {
            Invoke("Dead", 0.5f);
        }




    }

    //控制主角移動
    void Move(float front, float right)
    {
        if (!stop)
        {
            if (slowMod)//慢速模式
            {
                rb.AddForce(new Vector2(slowSpeed * right, slowSpeed * front));
            }
            else //正常速度
            {
                rb.AddForce(new Vector2(speed * right, speed * front));
            }
        }

    }

    void Shot()
    {
        //Transform temp = Instantiate(BulletObject, BulletInstantiatePoint.transform.position, BulletInstantiatePoint.transform.rotation).transform;
        //temp.SetParent(transform);
        //生出子彈
        Instantiate(BulletObject, BulletInstantiatePoint1.transform.position, BulletInstantiatePoint1.transform.rotation);
        Instantiate(BulletObject, BulletInstantiatePoint2.transform.position, BulletInstantiatePoint2.transform.rotation);

    }

    void Stop(bool s)
    {
        escCanvas.SetActive(s);
    }

    void Pass()
    {
        passCanvas.SetActive(true);
        stop = true;
        Time.timeScale = 0;
    }

    //繼續按鈕
    public void Continue()
    {
        escCanvas.SetActive(false);
        Time.timeScale = 2f;
        stop = false;
    }

    //更改彈幕發射位置
    void shotMod()
    {
        if (slowMod)
        {
            BulletInstantiatePoint1.transform.localPosition = Vector3.MoveTowards(BulletInstantiatePoint1.transform.localPosition, new Vector3(-0.25f, 1, 0), 0.1f);
            BulletInstantiatePoint2.transform.localPosition = Vector3.MoveTowards(BulletInstantiatePoint2.transform.localPosition, new Vector3(0.25f, 1, 0), 0.1f);

        }
        else
        {
            BulletInstantiatePoint1.transform.localPosition = Vector3.MoveTowards(BulletInstantiatePoint1.transform.localPosition, new Vector3(-0.4f, 1, 0), 0.1f);
            BulletInstantiatePoint2.transform.localPosition = Vector3.MoveTowards(BulletInstantiatePoint2.transform.localPosition, new Vector3(0.4f, 1, 0), 0.1f);
        }
    }

    void hit()
    {
        if (HP > 0)
        {
            aud.PlayOneShot(hitFX);
            HP--;
            hpList[HP].SetActive(false);
        }
        else
        {
            isDead = true;
            flowchart.SendFungusMessage("Character_dead");
        }

    }

    void Dead()
    {
        deadCanvas.SetActive(true);
        //stop = true;
        Time.timeScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "YAGOO_bullet")
        {
            Destroy(collision.gameObject);
            hit();
        }
    }
}
