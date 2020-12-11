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
    public GameObject Explosion;

    private float swipeStartTime;
    private float swipeEndTime;
    private float swipeTime;
    private float dist;
    private Vector2 startSwipePosition;
    private Vector2 endSwipePostion;
    Vector2 dir;
    private float swipeLength;
    public bool swiping;
    float timer;
    Rigidbody2D rb;
    Animator anim;
    [HideInInspector] public GameObject GameOverScreen;
    [HideInInspector] public bool wings, flamming, bubble, Immunity;
    [HideInInspector] public float wingsTimer, flammingTimer, bubbleTimer, ImmunityTimer;
    Skills skills;
    [HideInInspector] public Coroutine WingsCoroutine, BubbleCoroutine, FlammingCoroutine;
    [HideInInspector] public bool wingC, bubbleC, flameC;
    bool YetToExplode;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameOverScreen = GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).gameObject;
        skills = GetComponent<Skills>();
    }
   
    void Update()
    {
        SwipeTest();
        MovePlayer();
        PlayerDirection();
        Pickups();
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
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                dist = Vector2.Distance(Camera.main.ScreenToWorldPoint(touch.position), transform.position);
            }
            if (dist < 2.5f)
            {
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
                        MoveDirection(endSwipePostion);
                    }
                }
            }
        } 
    }
    void MoveDirection(Vector3 EndPosition)
    {
        dir = (Camera.main.ScreenToWorldPoint(EndPosition) - transform.position).normalized;
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
            if (Effects.explode && skills.OnFire || Effects.explode && flamming)
            {
                YetToExplode = true;
            }
        }
        float distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(endSwipePostion), transform.position);
        if (timer > 0.2 || distance < 0.5f)
        {
            rb.velocity = Vector3.zero;
            swiping = false;
            anim.SetBool("isDashing", false);
            timer = 0f;
            if(Effects.explode && skills.OnFire && YetToExplode || Effects.explode && flamming && YetToExplode)
            {
                Instantiate(Explosion, transform.position, Quaternion.identity);
                YetToExplode = false;
            }
        }
    }
    void Pickups()
    {
        if(!flamming && !skills.OnFire)
        {
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }

        if(flamming)
        {
            flammingTimer += Time.deltaTime;
        }
        if(flammingTimer >= 10)
        {
            flamming = false;
            flammingTimer = 0;
            flameC = false;
        }
        if(bubble)
        {
            transform.GetChild(3).gameObject.SetActive(true);
            bubbleTimer += Time.deltaTime;
            anim.SetBool("isFloating", true);
        }
        if(bubbleTimer >= 10)
        {
            transform.GetChild(3).gameObject.SetActive(false);
            bubble = false;
            bubbleC = false;
            bubbleTimer = 0;
            anim.SetBool("isFloating", false);
        }
        if(wings)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            transform.GetChild(1).gameObject.SetActive(true);
            wingsTimer += Time.deltaTime;
            anim.SetBool("isFloating", true);
        }
        if(wingsTimer >= 10)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            GetComponent<BoxCollider2D>().isTrigger = false;
            wings = false;
            wingC = false;
            wingsTimer = 0;
            anim.SetBool("isFloating", false);
        }
        if(Immunity)
        {
            transform.GetChild(5).gameObject.SetActive(true);
            ImmunityTimer += Time.deltaTime;
        }
        if(ImmunityTimer >= 5f)
        {
            transform.GetChild(5).gameObject.SetActive(false);
            Immunity = false;
        }
        if(wingsTimer >= 7 && !wingC)
        {
            WingsCoroutine = StartCoroutine(SwitchingOff(transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>()));
            wingC = true;
        }
        if (bubbleTimer >= 7 && !bubbleC)
        {
            BubbleCoroutine = StartCoroutine(SwitchingOff(transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>()));

            bubbleC = true;
        }
        if (flammingTimer >= 7 && !flameC)
        {
            FlammingCoroutine = StartCoroutine(SwitchingOff(transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>()));
            flameC = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hole") && !Immunity)
        {
            if (!swiping && !wings && !bubble)
            {
                if (skills.res)
                {
                    //Include Animation and Timer
                    bubble = true;
                    wings = true;
                    bubbleTimer += 5f;
                    wingsTimer += 5f;
                    skills.res = false;
                }
                else
                {
                    Time.timeScale = 0;
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnControl>().MovementSpeed = 0;
                    GameOverScreen.SetActive(true);
                }
            }

        }
        if (collision.gameObject.CompareTag("Police"))
        {
            if (!bubble)
            {
                if (skills.Freeze && !flamming && !skills.OnFire)
                {
                    Immunity = true;
                    wings = true;
                    wingsTimer += 5f;
                    skills.Freeze = false;
                }
                if(!flamming && !skills.OnFire && !skills.Freeze && !Immunity)
                {
                    Time.timeScale = 0;
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnControl>().MovementSpeed = 0;
                    GameOverScreen.SetActive(true);
                }
            }
            if(flamming || skills.OnFire)
            {
                Destroy(collision.gameObject);
                if(Effects.superCharge)
                {
                    skills.FireCharge += Effects.ChargeDownSpeed;
                }
            }
        }
        if(collision.gameObject.CompareTag("EndPolice"))
        {
            if (skills.Freeze)
            {
                Immunity = true;
                wings = true;
                wingsTimer += 5f;
                skills.Freeze = false;
            }
            else if (!Immunity)
            {
                Time.timeScale = 0;
                GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnControl>().MovementSpeed = 0;
                GameOverScreen.SetActive(true);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            if (flamming && swiping || skills.OnFire && swiping)
            {
                Destroy(collision.contacts[0].collider.gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            if (flamming && swiping || skills.OnFire && swiping)
            {
                Destroy(collision.contacts[0].collider.gameObject);
            }
        }
    }
    public void SwitchFireOff()
    {
        StopCoroutine(FlammingCoroutine);
        transform.GetChild(2).gameObject.SetActive(true);
    }
    public void SwitchBubbleOff()
    {
        StopCoroutine(BubbleCoroutine);
        transform.GetChild(3).gameObject.SetActive(true);
    }
    public void SwitchWingsOff()
    {
        StopCoroutine(WingsCoroutine);
        transform.GetChild(1).gameObject.SetActive(true);
    }
    IEnumerator SwitchingOff(SpriteRenderer sprite)
    {
        for (int i = 0; i < 3; i++)
        {
            Color a = sprite.material.color;
            a.a = 0f;
            sprite.material.color = a;
            yield return new WaitForSeconds(0.5f);
            Color b = sprite.material.color;
            b.a = 1f;
            sprite.material.color = b;
            yield return new WaitForSeconds(0.5f);
        }
        yield break;
    }
}
