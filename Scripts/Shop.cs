using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public Button buttonSettings;
    public bool shopView = false;

    [Space]
    public GameObject content;

    [Space]
    public Button btnSkins;
    public Button btnGround;
    public Button btnBackground;

    [Space]
    public List<GameObject> itemSkins;

    void Start () {

	}
	
	void Update () {
		
	}

    public void OnClickSkins ()
    {
        if (ClearItems(content))
        {
            foreach (var item in itemSkins)
            {
                GameObject instance = Instantiate(item, content.transform);
            }
        }
    }

    public void OnClickGround ()
    {

    }

    public void OnClickBackground ()
    {

    }

    public void OnClickShop ()
    {
        shopView = !shopView;
    }

    bool ClearItems (GameObject content)
    {
        if (content.GetComponentsInChildren<MonoBehaviour>().Length != 0)
            foreach (var item in content.GetComponentsInChildren<MonoBehaviour>())
                if (item.name != "Content")
                    Destroy(item.gameObject);

        return true;
    }

}
