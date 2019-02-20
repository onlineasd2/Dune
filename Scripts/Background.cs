using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    public new Camera camera;
    public Sprite spriteBg;
    
	void Start () {

        GetComponent<Transform>().localScale = new Vector2(camera.orthographicSize / 2f, camera.orthographicSize / 2.5f);
        GetComponent<SpriteRenderer>().sprite = spriteBg;

        List<GameObject> backgroundList = GameObject.Find("Buttons").GetComponent<Shop>().itemBackground;

        int id = PlayerPrefs.GetInt("LastItemBackground");

        spriteBg = backgroundList[id].GetComponent<ItemBackground>().texturePrefab;

    }
	
	void Update () {
        GetComponent<Transform>().localScale = new Vector2(camera.orthographicSize / 2f, camera.orthographicSize / 2.5f);
        GetComponent<SpriteRenderer>().sprite = spriteBg;
    }
}
