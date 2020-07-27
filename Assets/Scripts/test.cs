using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public List<Transform> gameOjbet_tran = new List<Transform>();
    private List<Vector3> point = new List<Vector3>();


    public GameObject ball;
    public float Speed = 1;
    public float Time1 = 2f;
    public float Timer = 0;
    public bool run = false;

    int i = 1;

    // Use this for initialization
    void Init()
    {

        point = new List<Vector3>();
        for (int i = 0; i < 200; i++)
        {
            //一
            Vector3 pos1 = Vector3.Lerp(gameOjbet_tran[0].position, gameOjbet_tran[1].position, i / 100f);
            Vector3 pos2 = Vector3.Lerp(gameOjbet_tran[1].position, gameOjbet_tran[2].position, i / 100f);
            Vector3 pos3 = Vector3.Lerp(gameOjbet_tran[2].position, gameOjbet_tran[3].position, i / 100f);
            //Vector3 pos4 = Vector3.Lerp(gameOjbet_tran[3].position, gameOjbet_tran[4].position, i / 100f);


            //二
            var pos1_0 = Vector3.Lerp(pos1, pos2, i / 100f);
            var pos1_1 = Vector3.Lerp(pos2, pos3, i / 100f);
            //var pos1_2 = Vector3.Lerp(pos3, pos4, i / 100f);

            //三
            var pos2_0 = Vector3.Lerp(pos1_0, pos1_1, i / 100f);
            //var pos2_1 = Vector3.Lerp(pos1_1, pos1_2, i / 100f);

            //四
            //Vector3 find = Vector3.Lerp(pos2_0, pos2_1, i / 100f);
            Vector3 find = pos2_0;

            point.Add(find);
        }

    }

    void OnDrawGizmos()//畫線
    {
        Init();
        Gizmos.color = Color.yellow;
        for (int i = 0; i < point.Count - 1; i++)
        {
            Gizmos.DrawLine(point[i], point[i + 1]);

        }
    }

    void Awake()
    {
        Init();
    }


    void Update()
    {
        Timer += Time.deltaTime;
        if (run)
        {
            //Timer = 0;
            ball.transform.localPosition = Vector3.Lerp(point[i - 1], point[i], 1f);
            i++;
            if (i >= point.Count) i = 1;

            if (ball.transform.position == gameOjbet_tran[3].position)
            {
                run = false;
            }
        }

    }
}
