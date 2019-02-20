using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGround : MonoBehaviour {


    public int id;
    public Image image;
    public bool isEnable;
    public ScoreManager manager;
    public GenerateMap generator;
    public Shop shop;
    public Material mainMaterial;
    public Material additionalMaterial;

    public int price;

    public void Start ()
    {
        manager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        shop = GameObject.Find("Buttons").GetComponent<Shop>();

        int butItemSkins = PlayerPrefs.GetInt("BuyItemGround" + id);

        if (butItemSkins > 0)
            isEnable = true;
        else
            isEnable = false;

        if (price <= 0)
            isEnable = true;

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

                PlayerPrefs.SetInt("BuyItemGround" + id, 1);
                PlayerPrefs.Save();
            }
            ReloadItem();
        }
        else
        { // Change ground
            generator = GameObject.Find("Generator").GetComponent<GenerateMap>();

            GameObject[] grounds = GameObject.FindGameObjectsWithTag("Ground");

            // Start material
            for (int i = 0; i < grounds.Length; i++)
                GameObject.FindGameObjectsWithTag("Ground")[i].GetComponentInChildren<Renderer>().material = mainMaterial;

            for (int i = 0; i < grounds.Length; i += 2)
                GameObject.FindGameObjectsWithTag("Ground")[i].GetComponentInChildren<Renderer>().material = additionalMaterial;

            // Update material
            generator.GetComponent<GenerateMap>().mainMaterial = mainMaterial;
            generator.GetComponent<GenerateMap>().additionalMaterial = additionalMaterial;

            generator.GetComponent<GenerateMap>().ReMesh(generator.GetComponent<GenerateMap>().firstGround);
            
            PlayerPrefs.SetInt("LastItemGround", id);
            PlayerPrefs.Save();
        }
    }
}
