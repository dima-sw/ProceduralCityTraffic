using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadManager : MonoBehaviour
{


    public Transform[] uscite;
    public List<GameObject> adiac=new List<GameObject>();

    public GameObject[] cars;

    public float percSpawn;
    public float timeSpawn, leftTimeSpawn;

    private void Start()
    {
        leftTimeSpawn = timeSpawn;
    }

    private void Update()
    {

        
        leftTimeSpawn -= Time.deltaTime;

        if (leftTimeSpawn <= 0f)
        {
            leftTimeSpawn = timeSpawn;

            float r = Random.Range(0f, 100f);

            if (r <= percSpawn)
            {

                int car = (int)Random.Range(0, cars.Length);

                if (car == cars.Length)
                {
                    car -= 1;
                }

                GameObject c=Instantiate(cars[car], transform.position, Quaternion.identity);

                c.GetComponent<trafficCar>().m_Target = dir(this.gameObject,c);
                c.GetComponent<trafficCar>().canGo = true;

                c.GetComponent<trafficCar>().from = this.gameObject;

            }


        }



    }


    public Vector3 dir(GameObject from,GameObject car)
    {

        int r = (int)Random.Range(0, adiac.Count);
        Vector3 to = Vector3.zero;
        while (adiac[r] == from)
        {
            
            r = (int)Random.Range(0, adiac.Count);
            
        }

        if (adiac[r].transform.position.x == transform.position.x)
        {
            if (adiac[r].transform.position.z > transform.position.z)
                to = adiac[r].GetComponent<roadManager>().uscite[3].position;
            else
                to= adiac[r].GetComponent<roadManager>().uscite[0].position;
        }
        else
        {

            if (adiac[r].transform.position.x > transform.position.x)
                to = adiac[r].GetComponent<roadManager>().uscite[2].position;
            else
                to = adiac[r].GetComponent<roadManager>().uscite[1].position;
        }

        
        
        car.GetComponent<trafficCar>().from = adiac[r];
        return to;

    }

        
    }


