using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIII : MonoBehaviour
{

    double x;
    double z;
    double xLowRadius;
    double xHighRadius;
    double zLowRadius;
    double zHighRadius;
    bool stop = true;
    double distance;
    private Transform playerPos;
    public GameObject player;

    private Scene currentScene;
    private string sceneName;

    // Use this for initialization
    void Start()
    {
        x = transform.position.x;
        z = transform.position.z;
        xLowRadius = (x + -10);
        xHighRadius = (x + 10);
        zLowRadius = (z + -10);
        zHighRadius = (z + 10);
       // Debug.Log(zHighRadius);
       // Debug.Log(zLowRadius);
        //playerPos = player.transform;

        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    // Update is called once per frame
    public void Update()
    {
        if (sceneName == "Scene")
        {
            //if (stop == true)
            //{
                movement();
            //}
            //else
            //{
              //  distance = Vector3.Distance(playerPos.position, transform.position);
                //if (distance > 5)
                //{
                  //  stop = true;
                //}
                //else
                //{
                  //  stop = false;
                //}

            //}
        }
    }

    void OnTriggerEnter(Collider other)
    {
        int num;
        num = Random.Range(0, 180);
        transform.localRotation *= Quaternion.Euler(0, num, 0);

    }

    void movement()
    {
        x = transform.position.x;
        z = transform.position.z;
        transform.Translate(Vector3.forward * Time.deltaTime);
        if (x > xHighRadius || x < xLowRadius || z > zHighRadius || z < zLowRadius)
        {
            transform.localRotation *= Quaternion.Euler(0, 145, 0);
            transform.Translate(Vector3.forward * Time.deltaTime);
            transform.Translate(Vector3.forward * Time.deltaTime);
            transform.Translate(Vector3.forward * Time.deltaTime);
            transform.Translate(Vector3.forward * Time.deltaTime);
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }

    public void check()
    {
        if (stop == true)
        {
            stop = false;
        }
        else
        {
            stop = true;
        }
    }
}

