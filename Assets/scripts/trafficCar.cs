using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class trafficCar : MonoBehaviour
{

    public Vector3 m_Target;
    [SerializeField]
    private float m_Speed;

    public bool canGo=false;

    public Vector3 nextPos;

    public GameObject from;

    public float rotateSpeed;

    public int coin;

    public float patience;
    public float losePatience;
    public Transform player;
    public bool revenge=false;
    public float tCattura;
    private float curCat;
    public bool cattura;


    public GameObject loadingBar;
    public Image lBar;



    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 100f);
        curCat = tCattura;

        loadingBar = GameObject.Find("GameManager").GetComponent<GameManager>().ProgressBar;
        lBar= GameObject.Find("GameManager").GetComponent<GameManager>().loadingBar.GetComponent<Image>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (canGo && !revenge)
        {

            cammina(m_Target);

        }

        if (revenge)
        {

            cammina(player.position);

        }

    

    }


    private void Update()
    {
        if (cattura)
        {

            curCat -= Time.deltaTime;

            lBar.fillAmount = (tCattura - curCat)/tCattura;

            if (curCat <= 0)
            {
                loadingBar.SetActive(false);
                print("catturata");
                Destroy(gameObject);
            }

        }
    }


    public void cammina(Vector3 m_Target)
    {

        Vector3 lTargetDir = m_Target - transform.position;
        lTargetDir.y = 0.0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.deltaTime * rotateSpeed);

        //move towards the player
        transform.position += transform.forward * Time.deltaTime * m_Speed;

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.transform.tag == "spawner" && canGo)
        {
            m_Target = other.gameObject.GetComponent<roadManager>().dir(from, this.gameObject);
           
        }

        else if (other.gameObject.transform.tag == "Player")
        {
            cattura = true;
            loadingBar.SetActive(true);
        }


    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.transform.tag == "Player")
        {
            loadingBar.SetActive(false);
            
            cattura = false;
            curCat = tCattura;
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "Player" && patience>0f)
        {

            patience -= losePatience;




            if (patience <= 65f)
            {
                float r = Random.Range(0f, 100f);

                if (r <= 0)
                {
                   

                    m_Speed *= 2;
                    player = GameObject.FindWithTag("Player").transform;
                    revenge = true;
                }

                else
                {
                    m_Speed *= 1.8f;

                    Collider sphere = transform.GetComponent<SphereCollider>();
                    sphere.enabled = true;

                }

            }

        }

    }


    


}
