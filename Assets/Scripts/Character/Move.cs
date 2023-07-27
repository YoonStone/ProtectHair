using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Move : MonoBehaviour {

    int hp = 5;
    int gauge = 3;

    bool isGame, isUmbrella_o, isUmbrella_x, isGround,
        reHam, reUm_o, reUm_x, reSoju, reSpark;

    float directionX, speed = 20, dir = 1, speed_t, time;

    public GameObject GM, sand, soju_part, fire;
    public Transform hamburger, soju, um, spark;
    public ParticleSystem gauge_part, hp_part;

    Rigidbody2D rb;
    Transform trans;
    UISprite sprite;
    AudioSource ads;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ads = GetComponent<AudioSource>();
        trans = GetComponent<Transform>();
        sprite = GetComponent<UISprite>();
        Invoke("StartDelay", 1f);
    }

    void Update()
    {
        directionX = CrossPlatformInputManager.GetAxis("Horizontal");
        if(directionX < 0)
        {
            if (isUmbrella_o)
            {
                sprite.spriteName = "player_o_l";
            }
            else if (isUmbrella_x)
            {
                sprite.spriteName = "player_x_l";
            }
            else if (dir == 0)
            {
                sprite.spriteName = "player_t";

            }
            else
            {
                sprite.spriteName = "player_l";
            }
        }else if(directionX > 0)
        {
            if (isUmbrella_o)
            {
                sprite.spriteName = "player_o_r";
            }
            else if (isUmbrella_x)
            {
                sprite.spriteName = "player_x_r";
            }
            else if (dir == 0)
            {
                sprite.spriteName = "player_t";

            }
            else
            {
                sprite.spriteName = "player_r";
            }
        }
        else
        {

        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(directionX * speed * dir, rb.velocity.y);
    }

    //1초 후 시작
    void StartDelay()
    {
        isGame = true;
    }

    //점프
    public void Jump()
    {
        if (isGround && dir != 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 2, 0);
            isGround = false;
        }
    }

    public void Ground()
    {
        isGround = true;
    }

    //비에 맞았을 때
    public void Damage(string touch)
    {
        if (!isUmbrella_o) //우산을 안 쓰고 있을 때만
        {
            switch (touch)
            {
                case "Black":
                    --hp; break;
                case "Blue":
                    --gauge; break;
                case "Yellow":
                    sand.SetActive(true);
                    Invoke("SandDelay", 3f);
                    break;
                case "Spark":
                    if (dir == 0)
                    {
                        Destroy(GameObject.FindWithTag("Spark_gauge") as GameObject);
                        Instantiate(Resources.Load("Prefabs/2D/Item_Spark"), spark);
                        reSpark = true;
                        Invoke("BackSpark", 5f);
                    }
                    else
                    {
                        this.gameObject.GetComponent<UISprite>().spriteName = "player_t";
                        Instantiate(Resources.Load("Prefabs/2D/Item_Spark"), spark);
                        dir = 0;
                        Invoke("BackSpark", 5f);
                    }
                    break;
            }
            if (gauge == 0)
            {
                --hp;
                gauge = 3;
            }

            GM.SendMessage("GaugeManager", gauge);
            GM.SendMessage("HPManager", hp);
        }
    }

    //아이템 획득(회복)
    public void Recovery(string touch)
    {
        ads.Play();
        switch (touch)
        {
            case "Umbrella_o":
                if (isUmbrella_o) //이미 우산을 쓰고있다면
                {
                    Destroy(GameObject.FindWithTag("Um_gauge") as GameObject);
                    Instantiate(Resources.Load("Prefabs/2D/Item_Um"), um);
                    reUm_o = true;
                    Invoke("BackUm_o", 5f);
                }
                else //우산을 안쓰고있다면
                {
                    if (sprite.spriteName == "player_r" || sprite.spriteName == "player_x_r") //서류가방이거나 찢어진 우산이면
                    {
                        sprite.spriteName = "player_o_r"; //우산으로
                    }
                    else if (sprite.spriteName == "player_l" || sprite.spriteName == "player_x_l")
                    {
                        sprite.spriteName = "player_o_l";
                    }
                    isUmbrella_o = true;
                    Instantiate(Resources.Load("Prefabs/2D/Item_Um"), um);
                    Invoke("BackUm_o", 5f);
                }
                break;

            case "Umbrella_x":
                if (isUmbrella_x) //이미 찢어진 우산을 들고있다면
                {
                    reUm_x = true;
                    Invoke("BackUm_x", 5f);
                }
                else if (isUmbrella_o) //그냥 우산을 들고있다면
                {
                    if (sprite.spriteName == "player_o_r")
                    {
                        sprite.spriteName = "player_x_r";
                    }
                    else if (sprite.spriteName == "player_o_l")
                    {
                        sprite.spriteName = "player_x_l";
                    }
                    Destroy(GameObject.FindWithTag("Um_gauge") as GameObject); //원래있던 게이지 지우고
                    GameObject umsp = Instantiate(Resources.Load("Prefabs/2D/Item_Um"), um) as GameObject; //찢어진 우산 게이지로
                    umsp.GetComponent<UISprite>().spriteName = "umbrella_x";
                    isUmbrella_o = false;
                    isUmbrella_x = true;
                    reUm_o = true;
                    Invoke("BackUm_x", 5f);

                }
                else //서류가방을 들고있다면
                {
                    if (sprite.spriteName == "player_r")
                    {
                        sprite.spriteName = "player_x_r";
                    }
                    else if (sprite.spriteName == "player_l")
                    {
                        sprite.spriteName = "player_x_l";
                    }
                    Instantiate(Resources.Load("Prefabs/2D/Item_Um"), um);
                    GameObject.FindWithTag("Um_gauge").GetComponent<UISprite>().spriteName = "umbrella_x";
                    isUmbrella_x = true;
                    Invoke("BackUm_x", 5f);
                }
                break;

            case "Hamburger":
                if(dir != 0)
                {
                    if (speed == 25)
                    {
                        Destroy(GameObject.FindWithTag("Ham_gauge") as GameObject);
                        Instantiate(Resources.Load("Prefabs/2D/Item_Hamburger"), hamburger);
                        reHam = true;
                        Invoke("BackHam", 5f);
                    }
                    else
                    {
                        speed = 25;
                        Instantiate(Resources.Load("Prefabs/2D/Item_Hamburger"), hamburger);
                        fire.SetActive(true);
                        Invoke("BackHam", 5f);
                    }
                }
                break;
            case "Almond":
                if (gauge < 3)
                {
                    ++gauge;
                    gauge_part.Play();
                    GM.SendMessage("GaugeManager", gauge);
                }
                break;
            case "Balmo":
                if (hp < 5)
                {
                    ++hp;
                    hp_part.Play();
                    GM.SendMessage("HPManager", hp);
                }
                break;
            case "Soju":
                if(dir != 0)
                {
                    if (dir == -1)
                    {
                        Destroy(GameObject.FindWithTag("Soju_gauge") as GameObject);
                        Instantiate(Resources.Load("Prefabs/2D/Item_Soju"), soju);
                        reSoju = true;
                        Invoke("BackSo", 5f);
                    }
                    else
                    {
                        dir = -1;
                        Instantiate(Resources.Load("Prefabs/2D/Item_Soju"), soju);
                        soju_part.SetActive(true);
                        Invoke("BackSo", 5f);
                    }
                }
                break;
        }
    }

    //황사 딜레이
    void SandDelay()
    {
        sand.SetActive(false);
    }

    //우산_x 후 원상복귀
    void BackUm_x()
    {
        if (reUm_x)
        {
            reUm_x = false;
        }
        else
        {
            isUmbrella_x = false;
            if (sprite.spriteName == "player_x_r")
            {
                sprite.spriteName = "player_r";
            }
            else if (sprite.spriteName == "player_x_l")
            {
                sprite.spriteName = "player_l";
            }

        }
    }

    //우산_o 후 원상복귀
    void BackUm_o()
    {
        if (reUm_o)
        {
            reUm_o = false;
        }
        else
        {
            isUmbrella_o = false;
            if (sprite.spriteName == "player_o_r")
            {
                sprite.spriteName = "player_r";
            }
            else if (sprite.spriteName == "player_o_l")
            {
                sprite.spriteName = "player_l";
            }
        }
    }

    //햄버거 후 원상복귀
    void BackHam()
    {
        if (reHam)
        {
            reHam = false;
        }
        else
        {
            speed = 20;
            fire.SetActive(false);
        }
    }

    //소주 후 원상복귀
    void BackSo()
    {
        if (reSoju)
        {
            reSoju = false;
        }
        else
        {
            dir = 1;
            soju_part.SetActive(false);
        }
    }

    //번개 후 원상복귀
    void BackSpark()
    {
        if (reSpark)
        {
            reSpark = false;
        }
        else
        {
            sprite.spriteName = "player_r";
            dir = 1;
        }
    }
}
