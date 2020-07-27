using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "bullet")
        {
            Destroy(c.gameObject);
            //Debug.Log("碰到牆壁");
        }
        if (c.gameObject.tag == "YAGOO_bullet")
        {
            Destroy(c.gameObject);
            //Debug.Log("碰到牆壁");
        }
    }
}
