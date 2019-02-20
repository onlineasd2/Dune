using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour {

    public AudioClip clickBtn;

    public void ClickBtn()
    {
        if (PlayerPrefs.GetInt("Mute") > 0)
        {
            GetComponent<AudioSource>().clip = clickBtn;
            GetComponent<AudioSource>().Play();
        }
    }
}
