using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public ParticleSystem particle;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            particle.Play();
            Destroy(gameObject, 0.1f);
        }
    }
}
