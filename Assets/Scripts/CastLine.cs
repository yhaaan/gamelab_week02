using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CastLine : MonoBehaviour
{
    private Rigidbody2D zzi;
    [SerializeField]
    private float power;
    private float maxPower = 1f;
    private GameObject castingBar;
    private SpriteRenderer castingBarFill;
    public bool isCharging =  false;
    
    private void Start()
    {
        zzi = transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
        castingBar = transform.GetChild(1).gameObject;
        castingBarFill = castingBar.transform.GetChild(0).GetComponent<SpriteRenderer>();
        InitZzi();

    }

    
    private void Update()
    {
        Charging();
        ChargingBarSet();
    }


    public void InitZzi()
    {
        castingBar.SetActive(false);
        zzi.transform.position = new Vector3(5, -2, 0);
    }


    private bool right;
    private void Charging()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return; // UI 클릭시 리턴
            if (!(GameManager.instance.currentState == FishingState.Idle ||
                GameManager.instance.currentState == FishingState.Wait )) return; // Idle,Wait 상태가 아니면 리턴
            isCharging = true;
            zzi.transform.position = new Vector3(5,-2,0);
            zzi.linearVelocity = Vector3.zero;
            power = 0;
            GameManager.instance.ChangeCastLineState();
            castingBar.SetActive(true);
        }
        else if(Input.GetKey(KeyCode.Mouse0))
        {
            
            if (isCharging)
            {
                if (power >= maxPower)
                {
                    right = false;
                }
                else if (power <= 0)
                {
                    right = true;
                }
                
                if (right)
                {
                    power += Time.deltaTime;
                }
                else
                {
                    power -= Time.deltaTime;
                }
                
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (GameManager.instance.currentState == FishingState.CastLine)
            {
                isCharging = false;
                Casting();
            }
        }
    }
    private void Casting()
    {
        zzi.AddForce(new Vector2(-(5 + power*16), 20 +  power*10), ForceMode2D.Impulse);
        //Debug.Log(zzi.linearVelocity);
        castingBar.SetActive(false);
        GameManager.instance.ChangeWaitState();
        Invoke("PlayCastSound",1.2f);
        
    }

    private void ChargingBarSet()
    {
        castingBarFill.size = new Vector2(power,0.25f);
    }

    private void PlayCastSound()
    {
        SoundManager.instance.PlaySFX("SFX5");
        
    }
}


