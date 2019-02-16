using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

    public GameObject player;
    public float speed, distanceDie;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        speed = Random.Range(0.01f, 0.05f);

        gameObject.tag = "Cloud";
	}
	
	void Update () {
        Destroy(distanceDie);

        if (!player.activeSelf)
            player = GameObject.FindGameObjectWithTag("Player");

    }

    void Destroy (float distance)
    {
        if (player.transform.position.x >= (transform.position.x + distance))
            Destroy(gameObject);
    }
}
