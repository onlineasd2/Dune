using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCreator : MonoBehaviour {
    
    public Transform cloudPoint;
    public ScoreManager scoreManager;
    public Transform player;
    public float distance, distanceDie;

    [Space]
    public GameObject cloud;

    float lastStep;
    float t = 0;

    void Start () {
        MakeCloudInStart();
    }
	
	void Update () {
        MakePosition();
    }

    void MakeCloudInStart ()
    {
        cloudPoint.GetComponent<Transform>().position = new Vector2(player.transform.position.x, 0);

        // Create first three prefab
        for (float i = player.position.x - 50; i <= player.position.x + 100; i += 20)
        {
            cloudPoint.position = new Vector3(i + player.position.x, 20 + Random.Range(-1f, 1f), 5);
            //Debug.Log(cloudPoint.position.x);

            GameObject cloudCopy = Create(cloud, cloudPoint);

            float rand = Random.Range(2f, 4f);

            cloudCopy.transform.localScale = new Vector2(rand, rand);
            cloudCopy.GetComponent<Cloud>().distanceDie = distanceDie;
        }
    }

    void MakePosition ()
    {
        if (scoreManager.startGame)
            if (player.position.x + distance >= cloudPoint.position.x)
            {

                for (float i = 100; i < player.position.x + 100; i += 20)
                {
                    if ((lastStep != i) && (i > lastStep))
                    {
                        lastStep = i;

                        cloudPoint.position = new Vector3(i, 20 + Random.Range(-2f, 2f), 5);

                        GameObject cloudCopy = Create(cloud, cloudPoint);

                        float rand = Random.Range(2f, 4f);

                        cloudCopy.transform.localScale = new Vector2(rand, rand);
                        cloudCopy.GetComponent<Cloud>().distanceDie = distanceDie;
                    }
                }
            }
    }

    GameObject Create (GameObject obj, Transform point)
    {
        GameObject objCopy = Instantiate(obj, point.position, Quaternion.identity);
        return objCopy;
    }


}
