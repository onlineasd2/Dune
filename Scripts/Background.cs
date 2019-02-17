using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    public Camera camera;
    public Sprite spriteBg;
    
	void Start () {
        GetComponent<Transform>().localScale = new Vector2(camera.orthographicSize / 2f, camera.orthographicSize / 2.5f);
        GetComponent<SpriteRenderer>().sprite = spriteBg;

    }
	
	void Update () {
        GetComponent<Transform>().localScale = new Vector2(camera.orthographicSize / 2f, camera.orthographicSize / 2.5f);
        GetComponent<SpriteRenderer>().sprite = spriteBg;
    }
}
