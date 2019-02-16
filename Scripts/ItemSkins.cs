﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSkins : MonoBehaviour
{

    public Image image;
    public bool isEnable;
    public ScoreManager manager;
    public Shop shop;
    public GameObject prefab;

    public int price;

    public void Start ()
    {
        manager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        shop = GameObject.Find("Buttons").GetComponent<Shop>();
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
        } else { // Spawn player

            GameObject playerLast = GameObject.FindGameObjectWithTag("Player");
            GameObject[] cloudList = GameObject.FindGameObjectsWithTag("Cloud");
            GenerateMap generateMap = GameObject.Find("Generator").GetComponent<GenerateMap>();
            ScoreManager scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
            BackgroundCreator backgroundCreator = GameObject.Find("BackgroundCreator").GetComponent<BackgroundCreator>();
            Settings settings = GameObject.Find("Buttons").GetComponent<Settings>();
            Destroy(playerLast);

            GameObject player = Instantiate(prefab, shop.SpawnPoint.position, Quaternion.identity);

            settings.GetComponent<Settings>().player = player.GetComponent<PersonController>();
            generateMap.GetComponent<GenerateMap>().player = player.GetComponent<Transform>();
            scoreManager.GetComponent<ScoreManager>().player = player.GetComponent<Transform>();
            backgroundCreator.GetComponent<BackgroundCreator>().player = player.GetComponent<Transform>();

            for (int i = 0; i < cloudList.Length; i++)
                cloudList[i].GetComponent<Cloud>().player = player;

            Debug.Log("Spawned");
        }
    }
}
