using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBackground : MonoBehaviour {


    public int id;
    public Image image;
    public bool isEnable;
    public ScoreManager manager;
    public Background imageOnBackground;
    public Shop shop;
    public Sprite texturePrefab;

    public int price;

    public void Start ()
    {
        manager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        shop = GameObject.Find("Buttons").GetComponent<Shop>();
        imageOnBackground = GameObject.Find("Background").GetComponent<Background>();
        ReloadItem();
    }

    void ReloadItem ()
    {
        if (!isEnable)
        {
            foreach (Image item in GetComponentsInChildren<Image>())
            {
                if (item.name == "Image")
                    item.GetComponent<Image>().color = Color.Lerp(Color.gray, Color.black, .8f);

                if (item.name == "Button")
                    item.GetComponentInChildren<Text>().text = price.ToString();
            }
        }
        else
            foreach (Image item in GetComponentsInChildren<Image>())
            {
                if (item.name == "Image")
                    item.GetComponent<Image>().color = Color.white;

                if (item.name == "Button")
                    item.GetComponentInChildren<Text>().text = "Select";
            }

    }

    public void ClickBuy ()
    {
        if (!isEnable)
        {
            int money = manager.GetComponent<ScoreManager>().money;

            if (money >= price)
            {
                money -= price;

                PlayerPrefs.SetInt("SaveMoney", money);
                PlayerPrefs.Save();

                isEnable = true;

            }
            ReloadItem();
        }
        else
        { // Change ground
            imageOnBackground.spriteBg = texturePrefab;
        }
    }
}
