using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour {

    public Transform player;
    public GameObject[] grounds;
    public Transform pointSpawn;

    [Space]
    public List<GameObject> ExistPrefabs;

    [Space]
    int farxL = 208, 
        farxR = 208;

    int i;
    int lastStep;

	void Start ()
    {
        // Create first three prefab
        for (i = 32; i < player.position.x + 132; i += 44)
        {
            pointSpawn.position = new Vector2(i, -17);
            //Debug.Log(pointSpawn.position.x);
            int ground = Random.Range(0, grounds.Length);
            GameObject groundClone = Instantiate(grounds[ground], pointSpawn.position, Quaternion.identity);

            ExistPrefabs.Add(groundClone);
        }

    }
	
	void Update ()
    {
        // Create new prefabs
        if (player.position.x + farxR >= pointSpawn.position.x)
        {
            for (i = 164; i < player.position.x + 120; i += 44)
            {
                if ((lastStep != i) && (i > lastStep))
                {
                    lastStep = i;

                    pointSpawn.position = new Vector2(i, -17);
                    //Debug.Log(pointSpawn.position.x);
                    int ground = Random.Range(0, grounds.Length);
                    GameObject groundClone = Instantiate(grounds[ground], pointSpawn.position, Quaternion.identity);
                    if (groundClone != null)
                        ExistPrefabs.Add(groundClone);
                }
            }
        }

        // Delete exist prefabs
        for (i = 0; i < ExistPrefabs.Count; i++)
        {
            if (ExistPrefabs[i] != null)
            {
                if (ExistPrefabs[i].transform.position.x <= player.position.x - farxL)
                {
                    Destroy(ExistPrefabs[i].gameObject);
                    ExistPrefabs.RemoveAt(i);
                }
            }
        }

    }

}