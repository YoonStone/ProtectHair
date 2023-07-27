using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour 
{
    public Text count, count_End, comment_End;
    public GameObject background, gameStart, gameOver, end, player, pause, canvas;
    bool isNext;
    float second, curTime, minute;

    void Start()
    {
        Destroy(gameStart.gameObject, 1f);
        StartCoroutine("NextBackGround");
        curTime = Time.time;
    }

    void Update()
    {
        second = (int)(Time.time - curTime);

        if (second > 59)
        {
            curTime = Time.time;
            second = 0;
            minute++;

            if (minute > 59)
            {
                minute = 0;
            }
        }

        count.text = string.Format(minute.ToString("00") + ":" + second.ToString("00"));

        //시간에 따라 레벨 전환(1분 후, 1분  후, 1분 30초 후)
        switch (count.text)
        {
            case "01:00":
                this.gameObject.GetComponent<Level1>().enabled = false;
                this.gameObject.GetComponent<Level2>().enabled = true;
                count.color = Color.white;
                break;
            case "02:00":
                this.gameObject.GetComponent<Level2>().enabled = false;
                this.gameObject.GetComponent<Level3>().enabled = true;
                break;
            case "03:30":
                this.gameObject.GetComponent<Level3>().enabled = false;
                this.gameObject.GetComponent<Level4>().enabled = true;
                break;
        }

        //배경전환
        if (isNext)
        {
            background.GetComponent<Transform>().transform.Translate(0, 2 * Time.deltaTime, 0);
        }
    }

    //배경 전환(1분 후, 1분 후, 1분 30초 후)
    IEnumerator NextBackGround()
    {
        yield return new WaitForSeconds(59f);
        isNext = true;
        yield return new WaitForSeconds(1f);
        isNext = false;
        yield return new WaitForSeconds(59f);
        isNext = true;
        yield return new WaitForSeconds(1f);
        isNext = false;
        yield return new WaitForSeconds(89f);
        isNext = true;
        yield return new WaitForSeconds(1f);
        isNext = false;
    }

    //GameOver
    void GameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
        StartCoroutine("End");
    }

    //엔딩 화면
    IEnumerator End()
    {
        yield return WaitForUnscaledSeconds(1.5f);
        canvas.SetActive(false);
        end.SetActive(true);
        count_End.text = count.text;

        if (minute < 1)
        {
            comment_End.text = "?? 너 손가락 다쳤어?";

        }
        else if (minute < 2)
        {
            comment_End.text = "고작 다섯개도 못 지켜주는거야..?";
        }
        else if (minute < 3)
        {
            comment_End.text = "그래도 지켜주려고 해줘서 고마워..";
        }
        else if (minute < 5)
        {
            comment_End.text = "이렇게 많이 버텨줘서 고마워!!";
        }
        else
        {
            comment_End.text = "설마 이렇게 오래 버틴거야??";
        }
    }

    //일시정지
    public void OnClickPasue()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
    }

    //끝내기
    public void OnClickEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
    }

    //다시 시작
    public void OnClickReplay()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    //이어 하기
    public void OnClickCountinue()
    {
        pause.SetActive(false);
        StartCoroutine(Countdown(1, 1, 1));
        Instantiate(Resources.Load("Prefabs/2D/3"));
    }

    //메인으로
    public void OnClickTitle()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    //3초 카운트
    IEnumerator Countdown(int dur1, int dur2, int dur3)
    {
        while (dur1 > 0)
        {
            dur1--;
            yield return WaitForUnscaledSeconds(1f);
        }
        Instantiate(Resources.Load("Prefabs/2D/2"));

        while (dur2 > 0)
        {
            dur2--;
            yield return WaitForUnscaledSeconds(1f);
        }
        Instantiate(Resources.Load("Prefabs/2D/1"));

        while (dur3 > 0)
        {
            dur3--;
            yield return WaitForUnscaledSeconds(1f);
            Time.timeScale = 1;
        }
    }

    IEnumerator WaitForUnscaledSeconds(float dur)
    {
        var cur = 0f;
        while (cur < dur)
        {
            yield return null;
            cur += Time.unscaledDeltaTime;
        }
    }
}
