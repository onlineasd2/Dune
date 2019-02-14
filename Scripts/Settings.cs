using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public Button setting;
    public Button shop;
    public Button vibration;
    public bool vibrate = false;
    public Sprite vibrateSpriteEnable;
    public Sprite vibrateSpriteDisable;
    public ScoreManager scoreManager;
    public PersonController player;
    bool change = true;
    Sprite btn;

    Animation anim;

    private void Start ()
    {
        anim = GetComponent<Animation>();

        if (!scoreManager.GetComponent<ScoreManager>().startGame)
        {
            setting.gameObject.SetActive(true);
            shop.gameObject.SetActive(true);
            vibration.gameObject.SetActive(false);
        }
    }

    private void Update ()
    {
        if (scoreManager.GetComponent<ScoreManager>().startGame)
        {
            setting.gameObject.SetActive(false);
            shop.gameObject.SetActive(false);
            vibration.gameObject.SetActive(false);
        }

        player.GetComponent<PersonController>().vibrate = vibrate;
    }

    public void OnClickSettings ()
    {
        if (change)
            anim.Play("SettingsLeft");
        else
            anim.Play("SettingsRight");

        vibration.gameObject.SetActive(change);
        
        change = !change;
    }
    
    public void OnClickVibrate ()
    {
        anim.Play("Vibrate");

        if (vibrate)
        {
            vibration.GetComponent<Image>().sprite = vibrateSpriteEnable;
            Handheld.Vibrate();
        }
        else
        {
            vibration.GetComponent<Image>().sprite = vibrateSpriteDisable;
        }

        vibrate = !vibrate;
    }
}
