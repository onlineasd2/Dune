using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSkins : MonoBehaviour
{

    public Image image;
    public bool isEnable;
    public ScoreManager manager;

    public int price;

    public void Start ()
    {
        ReloadItem();
    }

    void ReloadItem ()
    {
        if (!isEnable)
        {
            foreach (var item in GetComponentsInChildren<Image>())
            {
                if (item.name == "Image")
                    item.GetComponent<Image>().color = Color.Lerp(Color.gray, Color.black, .8f);

                if (item.name == "Button")
                {
                    item.GetComponentInChildren<Text>().text = price.ToString();
                }
            }
        }
        else
            foreach (var item in GetComponentsInChildren<Image>())
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
    }
}
