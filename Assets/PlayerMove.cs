using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSwipeTime;
    public float minSwipeDistance;
    public float Power;
    public float MaxSwipeLength;
    public GameObject HitPrefab;

    private float swipeStartTime;
    private float swipeEndTime;
    private float swipeTime;
    private float dist;
    private Vector2 startSwipePosition;
    private Vector2 endSwipePostion;
    Vector2 dir;
    private float swipeLength;
    bool swiping;
    float timer;
    Rigidbody2D rb;
    [HideInInspector] public GameObject StopObject;
    Animator anim;
    GameObject GameOverScreen;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameOverScreen = GameObject.FindGameObjectWithTag("GameOver").transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        SwipeTest();
        MovePlayer();
        PlayerDirection();
    }
    void PlayerDirection()
    {
        if(transform.position.x >= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    void SwipeTest()
    {
        if(Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                dist = Vector2.Distance(Camera.main.ScreenToWorldPoint(touch.position), transform.position);
            }
            if (dist < 2)
            {
                Debug.Log(dist);
                if (touch.phase == TouchPhase.Began)
                {
                    swipeStartTime = Time.time;
                    startSwipePosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    swipeEndTime = Time.time;
                    endSwipePostion = touch.position;
                    swipeTime = swipeEndTime - swipeStartTime;
                    swipeLength = (endSwipePostion - startSwipePosition).magnitude;
                    if (swipeTime < maxSwipeTime && swipeLength > minSwipeDistance)
                    {
                        MoveDirection(startSwipePosition, endSwipePostion);
                    }
                }
            }
            Debug.Log(dist);
        } 
    }
    void MoveDirection(Vector2 StartPosition, Vector2 EndPosition)
    {
        dir = (EndPosition - StartPosition).normalized;
        swiping = true;
        anim.SetBool("isDashing", true);
    }
    private void MovePlayer()
    {
        if (swiping == true)
        {
            float SwiperPower = swipeLength;
            if (SwiperPower > MaxSwipeLength)
            {
                SwiperPower = MaxSwipeLength;
            }
            rb.AddForce(dir * SwiperPower * Power);
            timer += Time.deltaTime;
        }
        float distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(endSwipePostion), transform.position);
        if (timer > 0.2 || distance < 2)
        {
            rb.velocity = Vector3.zero;
            swiping = false;
            anim.SetBool("isDashing", false);
            timer = 0f;
            Destroy(StopObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Hit"))
        {
            rb.velocity = Vector3.zero;
            swiping = false;
            anim.SetBool("isDashing", false);
            timer = 0f;
            Destroy(StopObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hole"))
        {
            if (!swiping)
            {
                Time.timeScale = 0;
                GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnControl>().MovementSpeed = 0;
                GameOverScreen.SetActive(true);
            }
        }
        if (collision.gameObject.CompareTag("Police"))
        {
            Time.timeScale = 0;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnControl>().MovementSpeed = 0;
            GameOverScreen.SetActive(true);
        }
    }
}
