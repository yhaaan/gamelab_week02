using System;
using UnityEngine;

public class FishingBar : MonoBehaviour
{
    private float moveForce = 35f; // 이동 힘
    private float maxSpeed = 100f; // 최대 속도
    private float fallAcceleration = -13f; //자연스러운 떨어짐을 위한 가속도
    private float threshold = 0.2f; //속도가 0이 되는 임계값
    private Rigidbody2D rb;
    private Collider2D coll;
    private Collider2D target;
    private FishingSystem fishingSystem;
    private FishingGage fishingGage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        fishingSystem = transform.parent.GetComponent<FishingSystem>();
        target = fishingSystem.target.GetComponent<Collider2D>();
        fishingGage = fishingSystem.fishingGage.GetComponent<FishingGage>();
        
    }

    private void Update()
    {
        TargetCheck();
    }

    private void FixedUpdate()
    {
        UpBar();
        DownBar();
    }


    private void UpBar() //Mouse0을 누르면 bar가 위로 올라감
    {
        if (!fishingSystem.isFishing) return;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            rb.AddForce(Vector2.up * moveForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }

    private void DownBar()//기본적으로 bar가 아래로 내려감
    {
        if (!fishingSystem.isFishing) return;
        rb.linearVelocityY += 1 * fallAcceleration * Time.fixedDeltaTime;
        rb.linearVelocityY = Mathf.Clamp(rb.linearVelocityY, -maxSpeed, maxSpeed);
        
        //속도가 임계값보다 작으면 0으로 만들어줌
        if (Mathf.Abs(rb.linearVelocityY) < threshold)
        {
            rb.linearVelocityY = 0f;
        }
    }

    private void TargetCheck()//bar가 target과 충돌했는지 확인
    {
        if (!fishingSystem.isFishing) return;
        
        if (coll.bounds.Intersects(target.bounds))
        {
            fishingGage.GageIncrease();
        }
        else
        {
            fishingGage.GageDecrease();
        }
    }
    
    public void InitBar()
    {
        rb.linearVelocityY = 0;
    }
}
