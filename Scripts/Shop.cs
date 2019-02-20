using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public Button buttonSettings;
    public bool shopView = false;

    [Space]
    public GameObject content;
    public GameObject shopTable;
    public ScoreManager scoreManager;

    [Space]
    public Button btnSkins;
    public Button btnGround;
    public Button btnBackground;

    [Space]
    public Transform SpawnPoint;

    [Space]
    public List<GameObject> itemSkins;
    public List<GameObject> itemGround;
    public List<GameObject> itemBackground;

    void Start ()
    {
        GameObject player = Instantiate(itemSkins[LastSlected()].GetComponent<ItemSkins>().prefab, SpawnPoint.position, Quaternion.identity);

        Settings settings = GameObject.Find("Buttons").GetComponent<Settings>();

        settings.GetComponent<Settings>().player = player.GetComponent<PersonController>();

        player.GetComponent<PersonController>();


        shopTable.SetActive(shopView);
    }

    void Update () {
        if (scoreManager.startGame)
            shopTable.SetActive(false);
    }

    public int LastSlected ()
    {
        int lastSkins = PlayerPrefs.GetInt("LastItemSkins");

        return lastSkins;
    }

    public void OnClickSkins ()
    {
        if (ClearItems(content))
            SetItems(itemSkins, content.transform);
    }

    public void OnClickGround ()
    {
        if (ClearItems(content))
            SetItems(itemGround, content.transform);
    }

    public void OnClickBackground ()
    {
        if (ClearItems(content))
            SetItems(itemBackground, content.transform);
    }

    public void OnClickShop ()
    {
        shopView = !shopView;
        shopTable.SetActive(shopView);
    }

    // Set items in content
    bool SetItems (List<GameObject> array, Transform transform)
    {
        foreach (var item in array)
        {
            GameObject instance = Instantiate(item, transform);
        }

        return true;
    }
    // Clear content
    bool ClearItems (GameObject content)
    {
        if (content.GetComponentsInChildren<MonoBehaviour>().Length != 0)
            foreach (var item in content.GetComponentsInChildren<MonoBehaviour>())
                if (item.name != "Content")
                    Destroy(item.gameObject);

        return true;
    }

}
