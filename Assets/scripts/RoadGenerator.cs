using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{

    public static bool spawn = true;

    public static int maxR = 200;

    public bool canSPawn1,canSPawn2;

    public float xMax, xMin;
    public float zMax, zMin;

    public Transform spawn1, spawn2;
    public GameObject[] road;


    public Transform[] buildingsSpawn;
    public GameObject[] buildings;


    public float val;



    public float pBig, pMed, pSm;

    public int maxPassi;
    public float amount = 10;
    // Start is called before the first frame update
    void Start()
    {

        if (maxR>0)
        {
           
            maxR--;
            /*Vector3 v = findPoint(spawn1.position,spawn2.position);

            int p = (int)Random.Range(0f, 4f);
            Quaternion angle = transform.rotation;
            switch (p)
            {

                case 0:
                    angle = Quaternion.Euler(0, 180, 0); break;
                case 1: angle = Quaternion.Euler(0, 0, 0); break;
                case 2: angle = Quaternion.Euler(0, 90, 0); break;
                case 3: angle = Quaternion.Euler(0, 270, 0); break;
                    //case 2: angle = Quaternion.Euler(0, transform.rotation.y - 45f, 0); break;

            }

            angle = Quaternion.Euler(0, transform.localRotation.eulerAngles.y+90f, 0);
            spawn = false;
            GameObject va=Instantiate(road[0], v, angle);
            va.GetComponent<RoadGenerator>().val = val;
            va.GetComponent<RoadGenerator>().Spawna();*/

            Quaternion angle  = Quaternion.Euler(0, transform.localRotation.eulerAngles.y + 90f, 0);
            GameObject va = Instantiate(road[1], spawn1.position, angle);

            angle = Quaternion.Euler(0, transform.localRotation.eulerAngles.y + 90f, 0);
            GameObject va2 = Instantiate(road[1], spawn2.position, angle);


            spawnBuildings();


        }

    }

    public void spawnBuildings()
    {
        for (int i = 0; i <= 2; i += 2)
        {
            Vector3 start = buildingsSpawn[i].position;
            Vector3 end = buildingsSpawn[i + 1].position;
            Quaternion rot = buildingsSpawn[i].rotation;

            while (start != end)
            {
                start = Vector3.MoveTowards(start, end, amount);
                int b = Random.Range(0, buildings.Length);
                GameObject obj = Instantiate<GameObject>(buildings[b], start, rot);

                obj.GetComponent<Buildings>().id = maxR;
            }


        }
    }

    public Vector3 findPoint(Vector3 c,Vector3 s)
    {
        float x1 = c.x;
        float z1 = c.z;
        float x2 = s.x;
        float z2 = s.z;

        float b = 0;
        if (spawn)
            b = Random.Range(0f, 1f);
        else {
            
            b = Random.Range(0f, 1f);
           
            while (Mathf.Abs(b - 0.5f) < 0.35f)
            {
                b = Random.Range(0f, 1f);
               
            }
        }

        val = b;
        float a = (z2 - z1) / (x2 - x1);

        float x = ((x2 - x1) * b) + x1;

        float z = a*(x - x1) + z1;

        return new Vector3(x, c.y, z);


    }

    public void Spawna()
    {
        if (maxR > 0)
        {
            
            maxR--;
            Vector3 v = findPoint(spawn1.position, spawn2.position);

            int p = (int)Random.Range(0f, 4f);
            Quaternion angle = transform.rotation;
            /*switch (p)
            {

                case 0:
                    angle = Quaternion.Euler(0, 180, 0); break;
                case 1: angle = Quaternion.Euler(0, 0, 0); break;
                case 2: angle = Quaternion.Euler(0, 90, 0); break;
                case 3: angle = Quaternion.Euler(0, 270, 0); break;
                    //case 2: angle = Quaternion.Euler(0, transform.rotation.y - 45f, 0); break;

            }*/

            float an = Random.Range(45f, 135f);

            

            float per = Random.Range(0f, 100f);
            int ind=0;
            if (per < pBig)
                ind = 0;
            else if (per < pMed)
                ind = 1;
            else
                ind = 2;

            if (ind == 2)
                an = 90;
            angle = angle = Quaternion.Euler(0, transform.localRotation.eulerAngles.y + 90f, 0);
            GameObject va = Instantiate(road[ind], v, angle);
            va.GetComponent<RoadGenerator>().val = val;
            va.GetComponent<RoadGenerator>().Spawna();

            if (transform.position.x < xMin || transform.position.x > xMax || transform.position.z > zMax || transform.position.z < zMin )
            {
                Destroy(gameObject);
                return;
           
            }

            spawnBuildings();



        }
    }
}
  
