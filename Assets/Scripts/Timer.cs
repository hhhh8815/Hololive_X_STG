using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float timer = 0;//計時器
    
    private int timeSec = 0;//秒
    private int timeMin = 0;//時
    private int timeHr = 0;//分

    private string Sec;
    private string Min;
    private string Hr;

    public Text TimerText;//文字物件

    
    void Update()
    {
        timer += Time.deltaTime/2;//遊戲倍數是兩倍所以要除二
        
        

        timeSec = (int)timer;//秒

        if (timeSec > 59)
        {
            timer = 0;
            timeSec = 0;
            timeMin++;
        }
        if(timeMin > 59)
        {
            timeMin = 0;
            timeHr++;
        }

        Zero();
        
    }

    private void OnGUI()
    {
        TimerText.text = "Time : " + Hr + ":" + Min + ":" + Sec;
    }

    //補0
    void Zero()
    {
        if (timeSec < 10)
        {
            Sec = "0" + timeSec.ToString();
        }
        else
        {
            Sec = timeSec.ToString();
        }
        if (timeMin < 10)
        {
            Min = "0" + timeMin.ToString();
        }
        else
        {
            Min = timeMin.ToString();
        }
        if (timeHr < 10)
        {
            Hr = "0" + timeHr.ToString();
        }
        else
        {
            Min = timeHr.ToString();
        }
    }
}
