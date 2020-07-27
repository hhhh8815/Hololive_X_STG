using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("子彈速度")]
    public float speed = 0.5f;
    

    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!CharacterController.stop)
        {
            rb.AddForce(new Vector2(0, speed * 1));//往上跑
            //transform.Translate(new Vector3(0, 1*speed, 0));
        }

    }
}
