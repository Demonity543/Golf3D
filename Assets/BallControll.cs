using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallControll : MonoBehaviour
{
    public float zForce = 50;
    public float zScale = 1;

    public bool isMoving = false;
    public bool rotationReset = false;
    public bool count = false;

    public Transform aimArrow;

    public Camera MainCam;

    public GameObject arrowSign;

    PlayerStats playerStats = new PlayerStats();

    public Text power;

    public AudioSource Hit;
    public AudioSource End;
    public AudioSource Reset;

    void Update()
    {

        power.text = "FORCE: "+(zForce );

        if (transform.position.y < 0.05f)
        {

            StartCoroutine(ExampleCoroutine());
         
        }

        if (Input.GetKeyDown("w") && zForce <= 900)
        {
            zForce += 100;
            zScale += 0.5f;
            arrowSign.transform.localScale = new Vector3(zScale, 1, 1);
        }
        if (Input.GetKeyDown("s") && zForce >25)
        {
            zForce -= 50;
            zScale -= 0.25f;
            aimArrow.GetComponent<Transform>().localScale = new Vector3(zScale, 1, 1);

        }
        if (Input.GetKey("d"))
        {
            transform.Rotate(0, 5, 0);
        }
        if (Input.GetKey("a"))
        {
            transform.Rotate(0, -5, 0);
        }
        if ((GetComponent<Rigidbody>().velocity == Vector3.zero) && (rotationReset == false))
        {
            

            GameObject.Find("arrow").transform.localScale = new Vector3(1, 1, 1);
            transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            rotationReset = true;


          

        }

        if ((isMoving == true) && (GetComponent<Rigidbody>().velocity == Vector3.zero))
        {
            playReset();
            rotationReset = false;
            isMoving = false;
            count = true;
        }


        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        MainCam.transform.LookAt(targetPosition);


        MainCam.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity; 



        if (Input.GetKey("space"))
        {
            shoot();
        }
    }



    IEnumerator ExampleCoroutine()
    {
      

        Debug.Log("Touched The Ground!");


        yield return new WaitForSeconds(2); 
        
        GameObject.Find("arrow").transform.localScale = new Vector3(0, 0, 0); 

        gameObject.transform.position = new Vector3(-0.7f, 0.27f, -6.2f); 
        MainCam.transform.position = new Vector3(-0f, 1.25f, -6f);
         Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        MainCam.transform.LookAt(targetPosition);
        zForce = 25;
        zScale = 1;
        arrowSign.transform.localScale = new Vector3(zScale, 1, 1);

        GameObject.Find("arrow").transform.localScale = new Vector3(1, 1, 1);
    }

    void shoot()
    {
        if (GetComponent<Rigidbody>().velocity == Vector3.zero)
        {

            Hit.Play();


            GameObject.Find("arrow").transform.localScale = new Vector3(0, 0, 0); 


            GetComponent<Rigidbody>().AddRelativeForce(0, 0, zForce); 


            isMoving = true;
            zForce = 25;
            zScale = 1;

            if(count == true)
            {
                playerStats.addShot();
             
                count = false;

            }
        }



        void OnTriggerEnter(Collider other)
        {
            playEnd();
            Debug.Log("BALL COLIDED WITH TRIGGER");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }


    }



    public void playHit()
    {
        Hit.Play();
    }
    public void playEnd()
    {
        Reset.Play();
    }
    public void playReset()
    {
        Reset.Play();
    }


}
