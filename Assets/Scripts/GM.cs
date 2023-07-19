using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public GameObject head, gauge;
    public Text count;

    UISprite playerHead, gaugeSprite;

    void Start()
    {
        playerHead = head.GetComponent<UISprite>();
        gaugeSprite = gauge.GetComponent<UISprite>();

        Screen.SetResolution(720, 1280, true);

    }

    //HP매니저
    void HPManager(int hp)
    {
        switch (hp)
        {
            case 0:
                this.SendMessage("GameOver");
                break;
            case 1:
                playerHead.spriteName = "player1"; break;
            case 2:
                playerHead.spriteName = "player2"; break;
            case 3:
                playerHead.spriteName = "player3"; break;
            case 4:
                playerHead.spriteName = "player4"; break;
            case 5:
                playerHead.spriteName = "player5"; break;

        }
    }

    //게이지매니저
    void GaugeManager(int gauge)
    {
        switch (gauge)
        {
            case 1:
                gaugeSprite.spriteName = "gauge1"; break;
            case 2:
                gaugeSprite.spriteName = "gauge2"; break;
            case 3:
                gaugeSprite.spriteName = "gauge3"; break;
        }
    }
}