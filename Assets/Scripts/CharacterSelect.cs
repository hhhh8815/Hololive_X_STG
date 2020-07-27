using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [Header("角色按鈕")]
    public Button btnCharacter1;
    public Button btnCharacter2;
    public Button btnCharacter3;
    [Header("角色立繪物件")]
    public GameObject CharacterImgObj;
    [Header("角色名字物件")]
    public Text CharacterNameObj;
    [Header("角色介紹物件")]
    public Text CharacterInfoObj;
    [Header("角色名字")]
    public string CharacterName1 = "test1";
    public string CharacterName2 = "test2";
    public string CharacterName3 = "test3";
    [Header("角色介紹")]
    public string CharacterInfo1 = "test1 info";
    public string CharacterInfo2 = "test2 info";
    public string CharacterInfo3 = "test3 info";
    [Header("目前選擇")]
    public int nowSelect = 1;
    [Header("音效")]
    public AudioSource fx;

    private Color full = new Color(1, 1, 1, 1);//純白
    private Color half = new Color(1, 1, 1, 0.5f);//半透明白

    private void Start()
    {
        NowSelect();
        //設定選擇角色按鈕文字
        btnCharacter1.GetComponentInChildren<Text>().text = CharacterName1;
        btnCharacter2.GetComponentInChildren<Text>().text = CharacterName2;
        btnCharacter3.GetComponentInChildren<Text>().text = CharacterName3;
    }
    private void Update()
    {
        if(nowSelect < 3)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                nowSelect++;
                fx.Play();
                NowSelect();
            }
        }
        if(nowSelect > 1)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                nowSelect--;
                fx.Play();
                NowSelect();
            }
        }

        
    }

    //選擇角色1時
    public void Character1()
    {
        nowSelect = 1;
        CharacterNameObj.text = CharacterName1;
        CharacterInfoObj.text = CharacterInfo1;
        btnCharacter1.image.color = full;
        btnCharacter2.image.color = half;
        btnCharacter3.image.color = half;
        
    }
    //選擇角色2時
    public void Character2()
    {
        nowSelect = 2;
        CharacterNameObj.text = CharacterName2;
        CharacterInfoObj.text = CharacterInfo2;
        btnCharacter1.image.color = half;
        btnCharacter2.image.color = full;
        btnCharacter3.image.color = half;
        
    }
    //選擇角色3時
    public void Character3()
    {
        nowSelect = 3;
        CharacterNameObj.text = CharacterName3;
        CharacterInfoObj.text = CharacterInfo3;
        btnCharacter1.image.color = half;
        btnCharacter2.image.color = half;
        btnCharacter3.image.color = full;
        
    }

    void NowSelect()
    {
        switch (nowSelect)
        {
            case 1:
                Character1();
                break;
            case 2:
                Character2();
                break;
            case 3:
                Character3();
                break;
        }
    }
    
}
