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
    private Vector2 startSwipePosition;
    private Vector2 endSwipePostion;
    Vector2 dir;
    private float swipeLength;
    bool swiping;
    float timer;
    Rigidbody2D rb;
    public GameObject StopObject;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SwipeTest();
        MovePlayer();
    }
    void SwipeTest()
    {
        if(Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
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
                if(swipeTime<maxSwipeTime && swipeLength > minSwipeDistance)
                {
                    if (StopObject != null)
                    {
                        Destroy(StopObject);
                    }
                    StopObject = Instantiate(HitPrefab, Camera.main.ScreenToWorldPoint(endSwipePostion), Quaternion.identity);
                    MoveDirection(startSwipePosition, endSwipePostion);
                }
            }
        }
    }
    void MoveDirection(Vector2 StartPosition, Vector2 EndPosition)
    {
        dir = (EndPosition - StartPosition).normalized;
        swiping = true;
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

        if (timer > 0.2)
        {
            rb.velocity = Vector3.zero;
            swiping = false;
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
            timer = 0f;
            Destroy(StopObject);
        }
    }
}
