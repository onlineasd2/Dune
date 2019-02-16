using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour {

    public Transform player;
    public GameObject[] grounds;
    public Transform pointSpawn;
    public GameObject firstGround;

    public Material mainMaterial;
    public Material additionalMaterial;

    [Space]
    public List<GameObject> ExistPrefabs;

    [Space]
    int farxL = 208, 
        farxR = 208;

    int i;
    int lastStep;

	void Start ()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Debug.Log(firstGround.GetComponentsInChildren<CurvedGround>().Length);
        ReMesh(firstGround);

        // Create first three prefab
        for (i = 32; i < player.position.x + 132; i += 44)
        {
            pointSpawn.position = new Vector2(i, -17);
            //Debug.Log(pointSpawn.position.x);
            int ground = Random.Range(0, grounds.Length);
            GameObject groundClone = Instantiate(grounds[ground], pointSpawn.position, Quaternion.identity);

            groundClone.GetComponent<CurvedGround>().renderCurveMesh();
            groundClone.GetComponent<Renderer>().material = additionalMaterial;

            GameObject groundClone1 = Instantiate(groundClone, groundClone.transform);

            groundClone1.transform.localPosition = new Vector3(0, -1, -1);

            groundClone1.GetComponentsInChildren<CurvedGround>()[0].GetComponent<Renderer>().material = mainMaterial;
            groundClone1.GetComponent<CurvedGround>().renderCurveMesh();

            for (int i = 0; i < groundClone1.GetComponentsInChildren<Coin>().Length; i++)
                Destroy(groundClone1.GetComponentsInChildren<Coin>()[i].gameObject);

            ExistPrefabs.Add(groundClone);
        }

    }

    public void ReMesh (GameObject obj)
    {
        obj.GetComponent<Renderer>().material = additionalMaterial;
        obj.GetComponentsInChildren<CurvedGround>()[1].GetComponent<Renderer>().material = mainMaterial;

        obj.GetComponent<CurvedGround>().renderCurveMesh();
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

                    groundClone.GetComponent<CurvedGround>().renderCurveMesh();
                    groundClone.GetComponent<Renderer>().material = additionalMaterial;

                    GameObject groundClone1 = Instantiate(groundClone, groundClone.transform);

                    groundClone1.transform.localPosition = new Vector3(0, -1, -1);

                    groundClone1.GetComponent<Renderer>().material = mainMaterial;
                    groundClone1.GetComponent<CurvedGround>().renderCurveMesh();

                    for (int i = 0; i < groundClone1.GetComponentsInChildren<Coin>().Length; i++)
                        Destroy(groundClone1.GetComponentsInChildren<Coin>()[i].gameObject);

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