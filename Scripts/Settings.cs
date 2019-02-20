using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public Button setting;
    public Button shop;
    public Button vibrationBtn;
    public Button muteBtn;
    public bool vibrate = false;
    public bool mute = false;
    public Sprite vibrateSpriteEnable;
    public Sprite vibrateSpriteDisable;
    public Sprite muteSpriteEnable;
    public Sprite muteSpriteDisable;
    public ScoreManager scoreManager;
    public PersonController player;
    bool change = true;
    Sprite btn;

    public AudioClip sound;

    Animation anim;

    private void Start ()
    {
        anim = GetComponent<Animation>();

        if (!vibrate)
            vibrationBtn.GetComponent<Image>().sprite = vibrateSpriteDisable;

        if (!scoreManager.GetComponent<ScoreManager>().startGame)
        {
            setting.gameObject.SetActive(true);
            shop.gameObject.SetActive(true);
            vibrationBtn.gameObject.SetActive(false);
            muteBtn.gameObject.SetActive(false);
        }
    }

    private void Update ()
    {
        if (scoreManager.GetComponent<ScoreManager>().startGame)
        {
            setting.gameObject.SetActive(false);
            shop.gameObject.SetActive(false);
            vibrationBtn.gameObject.SetActive(false);
            muteBtn.gameObject.SetActive(false);
        }

        player.GetComponent<PersonController>().vibrate = vibrate;
    }

    public void OnClickSettings ()
    {
        if (change)
            anim.Play("SettingsLeft");
        else
            anim.Play("SettingsRight");

        vibrationBtn.gameObject.SetActive(change);
        muteBtn.gameObject.SetActive(change);

        change = !change;
    }
    
    public void OnClickVibrate ()
    {
        anim.Play("Vibrate");

        vibrate = !vibrate;

        if (vibrate)
        {
            vibrationBtn.GetComponent<Image>().sprite = vibrateSpriteEnable;
            Handheld.Vibrate();
        }
        else
        {
            vibrationBtn.GetComponent<Image>().sprite = vibrateSpriteDisable;
        }
    }

    public void OnMute ()
    {
        anim.Play("Vibrate");

        mute = !mute;

        if (mute)
        {
            muteBtn.GetComponent<Image>().sprite = muteSpriteDisable;
            
            PlayerPrefs.SetInt("Mute", 1);
            PlayerPrefs.Save();
        }
        else
        {
            muteBtn.GetComponent<Image>().sprite = muteSpriteEnable;
            GetComponent<AudioSource>().PlayOneShot(sound);

            PlayerPrefs.SetInt("Mute", 0);
            PlayerPrefs.Save();
        }
    }
}
