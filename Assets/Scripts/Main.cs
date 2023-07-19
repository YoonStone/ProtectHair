using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

    private void Start()
    {
        Screen.SetResolution(720, 1280, true);
    }
    //게임 시작
    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
    }

    //게임 설명
    public void OnClickHelp()
    {
        SceneManager.LoadScene(2);
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
}
