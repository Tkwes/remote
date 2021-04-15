using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public Animator anim;
    bool isisprinting = false;
    bool Escalando = false;
    bool Pulo = false;

    //enemy
    public float combate_enemy;
    Enemy enemy;

    //camera
    public Camera Camera;
    public int PowerUpCamera = 0;

    //roupa
    bool roupaAgua = false;
    int Life_Roupa_Agua = 10;


    //movement
    Rigidbody rb;
    bool jumpB = false;
    int jump;
    public float jumpForce;
    public float speed;
    public float forceScale = 1;
    public float _NEWforceScale = 1;

    //timers 
    public float timer_Roupa = 3, Timer_Speed = 5, Timer_Combat = 3, Timer_Camera = 5;

    //arrastavel
    public Transform Arrastavel;

    //hp
    public float Life_Player = 100f;

    bool isGrounded = false;
    bool escalavel = false;

    //Roupa Invisivel
    public static bool Roupa = false;
    public static bool invisivel = false;

    public GameObject Cvictory;
    public GameObject Arco;


    float Mat = 1000f;
    bool speedB = false;
    bool cameraA = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cvictory.SetActive(false);        
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D)&& isGrounded == true)
        {
            isisprinting = true;
            anim.SetBool("isSprinting", isisprinting);
        }
        else if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            isisprinting = false;
            anim.SetBool("isSprinting", isisprinting);
        }

       
      Movem();
      Jump();
      Powerspeed();
      PowerCamera();
                


    }
    void Movem()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true || Input.GetKeyDown(KeyCode.Space) && jumpB == true && jump>0 )
        {
            Pulo = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            jump = jump - 1;
        }
        else
        {
            Pulo = false;
        }
    }
    void roupa()
    {
        if(Roupa == true)
        {
            timer_Roupa -= Time.deltaTime;
            invisivel = true; 
            if (timer_Roupa <= 0)
            {
                timer_Roupa = 3;
                Roupa = false;
                invisivel = false;              
            }
        }
    }
    void Powerspeed()
    {
        if(speedB == true && Input.GetKey(KeyCode.Alpha1))
        {
            speed = 10;
            jumpForce = 7;
            Timer_Speed -= Time.deltaTime;
            if (Timer_Speed<=0)
            {
                speedB = false;
            }
        }
    }
    void PowerCamera()
    {
        
        if (Input.GetKey(KeyCode.Alpha2) && cameraA == true)
        {
            Timer_Camera -= Time.deltaTime;
            Camera.fieldOfView = PowerUpCamera;

        }
        if (Timer_Camera <= 0)
        {
            cameraA = false;
            Camera.fieldOfView = 60;
        }
        if (Input.GetKeyUp(KeyCode.Alpha2) || cameraA == false)
        {
            Camera.fieldOfView = 60;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp_Jump"))
        {
            jumpB = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("PowerUp_Speed"))
        {          
            speedB = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Roupa_Azul"))
        {
            roupaAgua = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("PowerUp_Camera"))
        {
            cameraA = true;
            Destroy(other.gameObject);
            Timer_Camera = 5;
        }
        if(other.gameObject.CompareTag("Bullet"))
        {
            Life_Player = Life_Player - 10;
            if (Life_Player<=0)
            {
                Destroy(gameObject);
                print("morreu");
            }
           
        }
        if(other.gameObject.CompareTag("Portal"))
        {
            print("colidiu");          
            Cvictory.SetActive(true);
            
        }
        if (other.gameObject.CompareTag("Arco"))
        {
            print("colidiu");
            Arco.SetActive(true);
            Destroy(other.gameObject);
        }


    }
    private void OnTriggerStay(Collider other)
    {
        

        if (other.gameObject.CompareTag("Enemy"))
        {
            Timer_Combat -= Time.deltaTime;
            if (Timer_Combat <= 0)
            {
                Life_Player = Life_Player - combate_enemy;
                Timer_Combat = 3;
              
                if (Life_Player <= 0)
                {
                    print("morreu");
                    Destroy(gameObject);
                }
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Escalavel"))
        {
            Escalando = false;
            anim.SetBool("Escalando", Escalando);
            escalavel = false;
            rb.useGravity = true;
            rb.drag = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Chao"))
        {
            isGrounded = true;
            jump = 2;
        }

        if (collision.gameObject.CompareTag("PowerUp_Roupa"))
        {
            Roupa = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("PowerUp_Escalada"))
        {           
            Destroy(collision.gameObject);
            forceScale = _NEWforceScale;
        }
    }
  
}


