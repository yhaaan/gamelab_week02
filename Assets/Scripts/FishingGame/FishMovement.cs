using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;


public class FishMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float difficultyFactor = 1f;
    private float minY = -1.8f;
    private float maxY = 1.8f;
    private float timeOffset;
    
    public float noiseView;
    [Header("Legendary")]
    public bool isLegendary;
    private Rigidbody2D rb;
    public float jumpForce = 5f;
    public float minInterval = 0.5f;
    public float maxInterval = 1.5f;
    
    
    private FishingSystem fishingSystem;
    private void Start()
    {
        timeOffset = Random.Range(0f, 100f);
        rb = GetComponent<Rigidbody2D>();
        fishingSystem = transform.parent.GetComponent<FishingSystem>();
    }

    private void Update()
    {
        if (!fishingSystem.isFishing) return;
        if(isLegendary) return;
        float noiseValue = Mathf.PerlinNoise(Time.time * moveSpeed * difficultyFactor, timeOffset);
        float newY = Mathf.Lerp (minY*1.2f, maxY*1.2f, noiseValue);
        if (newY > maxY) newY = maxY;
        else if (newY < minY) newY = minY;
        
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        
        noiseView = noiseValue;
    }
    
    public void LegendaryJump()
    {
        isLegendary = true;
        StartCoroutine(JumpRoutine());
    }
    
    IEnumerator JumpRoutine()
    {
        Debug.Log("Jump");
        while (true) // 무한 루프 (게임 종료 시 종료 필요)
        {
            if (!fishingSystem.isFishing) break;
            float randomForce = Random.Range(-jumpForce, jumpForce);
            if(transform.localPosition.y < -1.6f) randomForce = Random.Range(0, jumpForce);
            else if(transform.localPosition.y > 1.6f) randomForce = Random.Range(-jumpForce, 0);
            rb.linearVelocityY = randomForce; // 위아래로 랜덤한 이동
            //Debug.Log("Jump! " + randomForce);
            float waitTime = Random.Range(minInterval, maxInterval); // 랜덤 이동 주기
            yield return new WaitForSeconds(waitTime); // 대기 후 다시 실행
        }
    }
}
