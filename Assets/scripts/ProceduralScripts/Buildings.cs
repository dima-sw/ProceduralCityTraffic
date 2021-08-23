using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{

    public bool d = false;
    public int id;
    private void OnCollisionEnter(Collision collision)
    {

        print("oo");
            
            if (collision.gameObject.transform.tag == "road")
            {
                
                Destroy(gameObject);
            }
            else if (collision.gameObject.transform.tag == "building")
            {
                
                if (collision.gameObject.GetComponent<Buildings>().id > id)
                    Destroy(gameObject);
                else
                    Destroy(collision.gameObject);
            }

  

        

    }


}
