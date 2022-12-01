using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Game State
    enum State
    {
        Ready,
        Play,
        GameOver
    }

    State state;

    public BallControl ballControl;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI sizeLabel;
    public Button option;
    public SceneManager mainGame;
    public ParticleSystem snowFlake;

    public int score = 0;
    private bool isDead = false;

    // bool isReady = false;
    // Start is called before the first frame update
    void Start()
    {

        snowFlake = snowFlake.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Panel_Option") != null) // 옵션패널이 열렸을 때 (null값이 아니란 말은 패널이 활성화 되었다는 뜻)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }


        int size = CalSize();
        sizeLabel.text = "SIZE " + size;

        score = CalScore();
        scoreLabel.text = "SCORE " + score;

        if (score < 0)
        {
            isDead = true;
            if (isDead == true)
            {
                SceneManager.LoadScene("MainGame");

            }
            score = 0;

        }

    }

    //void LateUpdate()
    //{
    //    switch (state)
    //    {
    //        case State.Ready:
    //            break;
    //        case State.Play:
    //            break;
    //        case State.GameOver:
    //            break;
    //    }
    //}
    //void Ready()
    //{
    //    state = State.Ready;
    //    ballControl.SetActive(false);
    //    snowFlake.Pause();
    //}

    //void GameStart()
    //{
    //    state = State.Play;

    //    ballControl.SetActive(true);
    //    snowFlake.Play();

    //    ballControl.AutoMove();
    //}

    //void GameOver()
    //{
    //    state = State.GameOver;

    //}

    //void ReloadScene()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 로딩하고 있는 씬을 리로드
    //}
    int CalSize()
    {
        return (int)ballControl.transform.localScale.x;
    }

    int CalScore()
    {

        return (int)(-100 + (ballControl.transform.localScale.x * 100));
    }


}
