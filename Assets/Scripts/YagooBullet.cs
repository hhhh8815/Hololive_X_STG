using UnityEngine;
using Fungus;

public class YagooBullet : MonoBehaviour
{
    [Header("子彈速度")]
    public float speed = 0.5f;
    [Header("發射者")]
    public GameObject shooter;
    [Header("射擊方向")]
    public Vector2 direction;
    [Header("Flowchart")]
    public Flowchart flowchart;

    public Rigidbody2D rb;

    void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody2D>();
        flowchart = GameObject.Find("Flowchart").GetComponent<Flowchart>();//獲取Flowchart物件
        shooter = GameObject.Find("YAGOO");//獲取射手物件
        direction = transform.position - shooter.transform.position;//計算移動方向向量
    }
    void Update()
    {
        if (!CharacterController.stop)
        {
            rb.AddForce(direction * speed);//移動
            //transform.Translate(new Vector3(0, 1*speed, 0));
        }
        //如果Boss掛了要刪除場上所有Boss彈幕以免被打到
        if (!flowchart.GetBooleanVariable("Boss"))
        {
            Destroy(gameObject);
        }

    }
}
