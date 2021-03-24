using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//the camera for the main scene is not working
public class TestSpawn : MonoBehaviour
{

    public GameObject[] spawns;
    public Vector3 spawnValues;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;

    //public int maxSpawns;

    int randPoke;

    public GameObject player;
    private Transform playerPos;
    private float distance;
    public int maxSpawnDistance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("FPS");
        StartCoroutine(waitSpawner());
        StartCoroutine(toggleSpawner());
        playerPos = player.transform;

        stop = true;
    }


    void Update()
    {

    }

    IEnumerator waitSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(startWait);

            while (!stop)
            {
                randPoke = Random.Range(0, spawns.Length);

                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1, Random.Range(-spawnValues.z, spawnValues.z));

                Instantiate(spawns[randPoke], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation); //gameObject.transorm.rotation

                spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
                yield return new WaitForSeconds(spawnWait);

            }
        }

    }

    IEnumerator toggleSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);
            if (playerPos)
            {
                distance = Vector3.Distance(playerPos.position, transform.position);
                if (distance < maxSpawnDistance)
                {
                    stop = false;
                }
                else
                {
                    stop = true;
                }
            }
        }
    }


}

