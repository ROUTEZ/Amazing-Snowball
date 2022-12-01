using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class BallControl : MonoBehaviour
{
    // ball movement
    public float ballSpeed;
    public float automoveRate = 0.1f;
    public float jumpSpeed = 5.0f;
    public float scaleRate = 0.1f;
    public float hraccel = 2.0f;

    public float collrange = 10.0f;

    private Vector3 jumpAmount = new Vector3(0, 20, 0);

    private Transform transform;
    private Rigidbody rb;

    public GameManager gm;

    // snow particle trail
    public ParticleSystem ps1;
    public ParticleSystem ps2;
    public float particlescaleRate = 0.1f;



    public bool canJump;

    // getbiggerbyspeed
    private float maxSpeed = 1.0f;

    Vector3 startPos, endPos, direction;
    public float swipeLimit = 30.0f;


    //public float x_min = -10;
    //public float x_max = 10;




    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();


        ps1 = ps1.GetComponent<ParticleSystem>();
        ps2 = ps2.GetComponent<ParticleSystem>();

        canJump = false;


    }

    void FixedUpdate()
    {



    }
    // Update is called once per frame
    void Update()
    {
        AutoMove();


        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            TouchJump();
            TouchMove();
        }
        KeyMove();
        KeyJump();
        GetBiggerbySpeed();
    }

    void LateUpdate()
    {

    }

    public void AutoMove()
    {
        rb.AddTorque(new Vector3(automoveRate, 0, 0) * Time.deltaTime);
    }
    void TouchMove()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            Vector3 touchPos = Input.GetTouch(0).position;
            double halfScreen = Screen.width / 2.0;

            if (touchPos.x < halfScreen)
            {
                rb.AddTorque(new Vector3(0, 0, 1) * ballSpeed * hraccel * transform.localScale.x);
            }
            else if (touchPos.x > halfScreen)
            {
                rb.AddTorque(new Vector3(0, 0, -1) * ballSpeed * hraccel * transform.localScale.x);
            }

        }
    }

    void TouchJump()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            startPos = Input.GetTouch(0).position;

        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && canJump == true)
        {

            endPos = Input.GetTouch(0).position;

            direction = endPos - startPos;

            print(direction.y);

            if (direction.y >= swipeLimit)
            {
                rb.AddForce(jumpAmount * jumpSpeed, ForceMode.Impulse);
                canJump = false;

                ps1.Stop();
                ps2.Stop();
            }
        }
    }

    void KeyMove()
    {
        float hr = Input.GetAxis("Horizontal");


        rb.AddTorque(new Vector3(0, 0, -hr) * ballSpeed * hraccel * Time.deltaTime); // 좌우 컨트롤 
    }

    void KeyJump()
    {
        if (Input.GetButtonDown("Jump") && canJump == true)
        {

            rb.AddForce(jumpAmount * jumpSpeed, ForceMode.Impulse);
            canJump = false;

            ps1.Stop();
            ps2.Stop();
        }

    }

    void GetBiggerbySpeed()
    {

        //maxSpeed = Mathf.Clamp(maxSpeed, 1.0f, 50.0f);
        float speed = rb.velocity.magnitude;

        if (canJump == true)
        {
            if (maxSpeed < speed)
                maxSpeed = speed;

            transform.localScale = new Vector3(transform.localScale.x + maxSpeed * scaleRate * Time.deltaTime,
                                            transform.localScale.y + maxSpeed * scaleRate * Time.deltaTime,
                                            transform.localScale.z + maxSpeed * scaleRate * Time.deltaTime);

            //print("MAX SPEED = " + maxSpeed);
            //print("speed = " + speed);


            ps1.transform.localScale = new Vector3(transform.localScale.x + maxSpeed * scaleRate * Time.deltaTime * particlescaleRate,
                                                transform.localScale.y + maxSpeed * scaleRate * Time.deltaTime * particlescaleRate,
                                                transform.localScale.z + maxSpeed * scaleRate * Time.deltaTime * particlescaleRate);
            ps2.transform.localScale = new Vector3(transform.localScale.x + maxSpeed * scaleRate * Time.deltaTime * particlescaleRate,
                                                transform.localScale.y + maxSpeed * scaleRate * Time.deltaTime * particlescaleRate,
                                                transform.localScale.z + maxSpeed * scaleRate * Time.deltaTime * particlescaleRate);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            canJump = true;

            ps1.Play();
            ps2.Play();
        }
        else
        {
            canJump = false;

            ps1.Pause();
            ps2.Pause();
        }

        if (collision.transform.tag == "Obstacle")
        {
            if (collision.transform.lossyScale.y > transform.lossyScale.y)
            {
                OnDamaged();

            }
        }
    }

    void OnDamaged()
    {

        rb.AddForce(new Vector3(0, collrange, -collrange), ForceMode.Impulse);
        transform.localScale = (new Vector3(transform.localScale.x * 0.5f, transform.localScale.y * 0.5f, transform.localScale.z * 0.5f));

    }

    void OnDrain()
    {

    }
    public void SetActive(bool active)
    {
        // rigidbody ON,OFF
        rb.isKinematic = !active;
    }
}
