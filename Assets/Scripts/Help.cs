using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Help : MonoBehaviour {

    public GameObject help1, help2, help3;

    private void Start()
    {
        Screen.SetResolution(720, 1280, true);
    }
    //게임 시작
    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
    }

    //메인으로
    public void OnClickTitle()
    {
        SceneManager.LoadScene(0);
    }

    //다음 페이지
    public void OnClickNextTo2()
    {
        help1.SetActive(false);
        help2.SetActive(true);
    }

    //다음 페이지
    public void OnClickNextTo3()
    {
        help2.SetActive(false);
        help3.SetActive(true);
    }

    //이전 페이지
    public void OnClickBackTo1()
    {
        help1.SetActive(true);
        help2.SetActive(false);
    }


    //이전 페이지
    public void OnClickBackTo2()
    {
        help2.SetActive(true);
        help3.SetActive(false);
    }
}
