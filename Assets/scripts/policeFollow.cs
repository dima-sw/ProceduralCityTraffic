using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class policeFollow : MonoBehaviour
{
    
    public float moveSpeed = 5; //move speed


    [SerializeField]
    private Transform m_Target;
    [SerializeField]
    private float m_Speed;

   // private NavMeshAgent agent;

    //private Rigidbody rb;
    //private Vector3 mytransform;

    private void Start()
    {
       // agent = GetComponent<NavMeshAgent>();
    }


    void FixedUpdate()
    {
        //rotate to look at the player
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed * Time.deltaTime);


        Vector3 lTargetDir = m_Target.position - transform.position;
        lTargetDir.y = 0.0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.deltaTime * m_Speed);

        //move towards the player
        transform.position += transform.forward * Time.deltaTime * moveSpeed;


        //agent.Warp(m_Target.position);


    }
}
