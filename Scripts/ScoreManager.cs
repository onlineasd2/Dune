using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    public int score;
    public int money;
    public int record;
    public int recordMoney;
    int initialValue = 1;

    [Space]
    public Transform player;

    [Space]
    public Transform line;
    public Transform highLine;

    [Space]
    public Text UIScore;
    public Text UIMoney;
    public Text UIPopUp;
    public Text UIRecord;
    public Button btnReload;

    [Space]
    public Animator popUpClip;

    bool once1 = false;
    bool once2 = false;

    bool playAnim;
    
    public float a = 0;

    [Space]
    public bool startGame;

    void Start ()
    {
        record = PlayerPrefs.GetInt("SaveRecord");
        money = PlayerPrefs.GetInt("SaveMoney");
        
        player = GameObject.FindGameObjectWithTag("Player").transform;

        line.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        highLine.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
    }

    void Update () {
        CheckHeight();
        UIUpdate();
        StartGame();
        BeginingStart();
        MoneyUpdate();
    }

    void BeginingStart ()
    {
        if (Input.GetKey(KeyCode.Space))
            startGame = true;
    }

    public void StartGameBtn ()
    {
        startGame = true;
    }

    void StartGame ()
    {
        if (startGame)
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            if (a <= 1)
                a += 0.05f;

            line.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, a);
            
            player = GameObject.FindGameObjectWithTag("Player").transform;
        } else
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;

    }


    void PopUpScore (int value, bool animEnable)
    {
        if (value > GetScore(initialValue, 4))
            UIPopUp.text = "+To The Moon\n" + value.ToString();
        else
            UIPopUp.text = "+" + value.ToString();

        UIPopUp.rectTransform.localPosition = new Vector2(Random.Range(-250, 250), Random.Range(-100, 100));
        popUpClip.SetBool("PopUp", animEnable);
    }

    void UIUpdate ()
    {
        UIScore.text = score.ToString();
        UIMoney.text = recordMoney.ToString();
        UIRecord.text = record.ToString();

        // If the player is dead, show button reload
        if (player.GetComponent<PersonController>().isDead)
        {

            btnReload.gameObject.SetActive(true);
            SetRecord();
            SetMoney();

        }
        else
            btnReload.gameObject.SetActive(false);
    }


    // Save money
    public void SetMoney ()
    {
        //Debug.Log(recordMoney);

        PlayerPrefs.SetInt("SaveMoney", recordMoney);
        PlayerPrefs.Save();
        //Debug.Log("Money Save");

    }

    // Save record
    public void SetRecord ()
    {
        record = PlayerPrefs.GetInt("SaveRecord");

        if (score > record)
        {
            PlayerPrefs.SetInt("SaveRecord", score);
            PlayerPrefs.Save();
            //Debug.Log("Record Save");
        }
    }

    // Get value in score, the player went through the line
    void CheckHeight ()
    {
        if (IsAnimationPlaying("PopUp"))
            popUpClip.SetBool("PopUp", false);

        if (player.position.y >= line.position.y)
        {
            if (!once1)
            {
                int v = 0;
                if (player.GetComponent<PersonController>().velocity <= 45)
                    v = GetScore(initialValue);
                else if ((player.GetComponent<PersonController>().velocity <= 65) && (player.GetComponent<PersonController>().velocity > 45))
                    v = GetScore(initialValue, 2);
                else
                    v = GetScore(initialValue, 4);

                score += v;
                PopUpScore(v, true);
                once1 = true;
            }
        }
        else
            once1 = false;

        if (player.position.y >= highLine.position.y)
        {
            if (!once2)
            {
                score += GetScore(initialValue * 8);
                PopUpScore(initialValue * 8, true);
                once2 = true;
            }
        }
        else
            once2 = false;

    }
    
    public void ReloadLevel ()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PersonController>().isDead)
            SceneManager.LoadScene(0);
    }

    void MoneyUpdate ()
    {
        recordMoney = (money + player.GetComponent<PersonController>().coins);
    }

    int GetScore (int value)
    {
        int score = 0;
        
        score += value;

        return score;
    }

    int GetScore (int value, int multiply)
    {
        int score = 0;

        score += value * multiply;

        return score;
    }
    
    public bool IsAnimationPlaying (string animationName)
    {
        // берем информацию о состоянии
        var animatorStateInfo = popUpClip.GetCurrentAnimatorStateInfo(0);
        // смотрим, есть ли в нем имя какой-то анимации, то возвращаем true
        if (animatorStateInfo.IsName(animationName))
            return true;

        return false;
    }

}
