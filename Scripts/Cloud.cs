using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

    GameObject player;
    public float speed, distanceDie;

	void Start () {
        player = GameObject.Find("Player");
        speed = Random.Range(0.01f, 0.05f);
	}
	
	void Update () {
        //Move();
        Destroy(distanceDie);
	}

    void Move ()
    {
        transform.Translate(Vector2.left * speed);
    }

    void Destroy (float distance)
    {
        if (player.transform.position.x >= (transform.position.x + distance))
            Destroy(gameObject);
    }
}
