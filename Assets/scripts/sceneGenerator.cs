using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneGenerator : MonoBehaviour
{

    public int xDivision, yDivision;

    public GameObject[] buildings;
    public float roadWidth;
    public float buildingSpace;

    public float xGrandezza, zGrandezza;

    public Vector3 inizio;


    public GameObject terrain;
    public GameObject spawner;
    // Start is called before the first frame update
    Vector2 gridSize;
    void Start()
    {
        int numRectangles = xDivision * yDivision;
        gridSize= new Vector2((xDivision + 1), (yDivision + 1));
        GameObject[][] cross;

        cross = new GameObject[(int)gridSize.x][];
        for(int xx = 0; xx < gridSize.x; xx++)
        {
            cross[xx] = new GameObject[(int)gridSize.y];
            
        }

        
        int x=0, y = 0;

        for(int i = 0; i < xDivision; i++)
        {
            Vector3 pos = new Vector3(inizio.x-roadWidth/2,0.3f,inizio.z-roadWidth/2);
            for(int s = 0; s < yDivision + 1; s++)
            {
                
                GameObject c=Instantiate(spawner, pos, Quaternion.identity);
                pos = new Vector3(pos.x,pos.y,pos.z+zGrandezza+roadWidth);

                cross[x][y] = c;
                y++;
            }

            for(int j = 0; j < yDivision; j++)
            {
                    
                   StartCoroutine(createChunk(inizio, xGrandezza+inizio.x, zGrandezza+inizio.z, buildingSpace, 10));
                   inizio = new Vector3(inizio.x, 0, zGrandezza + inizio.z + roadWidth);

            }
            inizio = new Vector3(xGrandezza + inizio.x + roadWidth, inizio.y, 0);

            x++;
            y = 0;
            if (i + 1 >= xDivision)
            {
                pos = new Vector3(inizio.x - roadWidth / 2, 0.3f, inizio.z - roadWidth / 2);
                for (int s = 0; s < yDivision + 1; s++)
                {

                    GameObject c=Instantiate(spawner, pos, Quaternion.identity);
                    pos = new Vector3(pos.x, pos.y, pos.z + zGrandezza + roadWidth);
                    
                    cross[x][y] = c;
                    y++;
                }
            }
        }

        initGrid(cross);


    }


    public void initGrid(GameObject[][] cross)
    {

        for(int x = 0; x < cross.Length; x++)
        {
            for (int y = 0; y < cross[x].Length; y++)
            {

              


                int i = 0;
                int xx = x - 1, yy = y - 1;
                addAdiac(x, y, x, y - 1,cross,i);
                i++;
                addAdiac(x, y, x, y + 1, cross, i);
                i++;
                addAdiac(x, y, x-1, y, cross, i);
                i++;
                addAdiac(x, y, x+1, y, cross, i);

            }

        }



    }


    public void addAdiac(int x,int y,int xx,int yy,GameObject[][]cross,int i)
    {
        if (IsBetween(xx, 0, (int)cross.Length-1) && IsBetween(yy, 0, (int)cross[x].Length - 1))
        {
            
            //print();
            cross[x][y].GetComponent<roadManager>().adiac.Add(cross[xx][yy]);
        }
    }


    public bool IsBetween(int testValue, int bound1, int bound2)
    {
        if (bound1 > bound2)
            return testValue >= bound2 && testValue <= bound1;
        return testValue >= bound1 && testValue <= bound2;
    }

    IEnumerator createChunk(Vector3 initPos,float x, float z,float space,float noisePrecision)
    {
        float xInit = initPos.x;
        float zInit = initPos.z;

        float seed = Random.Range(0, 20000);

        Vector3 terPos = new Vector3((xInit + x) / 2 -1f, 0.2f, (zInit + z) / 2 -0.9f);
        GameObject t = Instantiate(terrain, terPos, Quaternion.identity);

        t.transform.localScale = new Vector3(xGrandezza+4, t.transform.localScale.y, zGrandezza+4);


        for(float xx=xInit; xx<x;xx+=space)





            for (float zz = zInit; zz < z; zz += space)
            {
                int result= (int) (Mathf.PerlinNoise(zz/noisePrecision+ seed,xx/noisePrecision +seed)*buildings.Length);

                Vector3 pos = new Vector3(xx, 5, zz);

                Quaternion angle = Quaternion.identity;

                if (zz == zInit)
                {
                    angle = Quaternion.Euler(0, 180, 0);

                }
                else if (xx == xInit)
                {
                    angle = Quaternion.Euler(0, -90, 0);
                }
                else if (xx + space >= x)
                {

                    angle = Quaternion.Euler(0, 90, 0);

                }
                
                if (result == buildings.Length)
                    result -= 1;
                Instantiate(buildings[result], pos, angle);


                /*if(xx==xInit && zz == zInit)
                {

                    Instantiate(spawner, new Vector3(pos.x - roadWidth / 2, 0.2f, pos.z - roadWidth / 2), Quaternion.identity);

                }
                else if (xx == xInit && zz == zInit)
                {

                    Instantiate(spawner, new Vector3(pos.x - roadWidth / 2, 0.2f, pos.z - roadWidth / 2), Quaternion.identity);

                }*/

                yield return new WaitForSeconds(0.01f);

            }

        yield return 0;

    }
   
}
