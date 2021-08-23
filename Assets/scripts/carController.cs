using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    public float fowardAccel, maxSpeed, turnStrenght, gravityForce;
    public Rigidbody theRB;


    public float turnInput;

    public float accInput;
   

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody>();
    }




    // Update is called once per frame
    void Update()
    {

        turnInput = Input.GetAxis("Horizontal");
        accInput = Input.GetAxis("Vertical");
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput*turnStrenght *Time.deltaTime,0f));


        if (Input.GetKeyDown("space"))
        {

            transform.position = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
            transform.rotation = Quaternion.identity;
        }


    }


    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * fowardAccel * Time.deltaTime*accInput);
    }


}
