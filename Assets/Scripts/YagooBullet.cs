using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YagooBullet : MonoBehaviour
{
    [Header("子彈速度")]
    public float speed = 0.5f;
    [Header("發射者")]
    public GameObject shooter;
    [Header("射擊方向")]
    public Vector2 direction;

    public Rigidbody2D rb;

    void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody2D>();
        shooter = GameObject.Find("YAGOO");
        direction = transform.position - shooter.transform.position;
    }
    void Update()
    {
        if (!CharacterController.stop)
        {
            rb.AddForce(direction * speed);//往上跑
            //transform.Translate(new Vector3(0, 1*speed, 0));
        }

    }
}
