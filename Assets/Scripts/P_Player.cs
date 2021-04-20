using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class P_Player : MonoBehaviour
{
    [Header("Player")]
    // movimentação
    private float velocidade = 10f;
    private bool noChao = false;
    private float pulo = 10f;
    Rigidbody rb;
    PhotonView MyPhotonView;

    //sistema de troca
    [Header("Armas")]
    public GameObject arm;
    public GameObject arma1_O, arma2_O, arma3_O;
    bool arma1, arma2, arma3; // permite ativar ou nao as armas 
    LookAtMouse Script_Armas; //chama script do suporte arma

    //powerup Camera
    [Header("Poweup")]
    bool camera;
    float Timer_Camera = 0;
    float Valor_Camera = 0;

    [Header("Camera")]
    public Camera Camera_hud;

    void Start()
    {
        MyPhotonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();// movimentação

        //armas desativadas coleta
        arma1 = false;
        arma2 = false;
        arma3 = false;
        //armas desativadas obj
        arma1_O.SetActive(false);
        arma2_O.SetActive(false);
        arma3_O.SetActive(false);
        //powerUps
        camera = false;
        

        if (!MyPhotonView.IsMine)//Verificando se o player sou eu
        {
            Camera_hud.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (MyPhotonView.IsMine)
        {
            Movimentação();
        }
        else
        {
            Camera_hud.gameObject.SetActive(false);
        }
        TrocadeArmas();
        PowerCamera();
    }

    void Movimentação()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal") * velocidade * Time.deltaTime, 0, 0);

        noChao = Physics.Raycast(transform.position, -Vector3.up, 1f);

        if (Input.GetButtonDown("Jump"))
        {
            if (noChao == true)
            {
                rb.velocity = new Vector3(0, Input.GetAxis("Jump") * pulo, 0);
            }
        }
    }

    void TrocadeArmas()
    {
        //seleçao de armas pelo alphanumerico
        if(Input.GetKeyDown(KeyCode.Alpha1) && arma1 == true)//ativa e desativa armas ja pegadas pelo mapa
        {
            arma1_O.SetActive(true);
            arma2_O.SetActive(false);
            arma3_O.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && arma2 == true)
        {
            arma1_O.SetActive(false);
            arma2_O.SetActive(true);
            arma3_O.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && arma3 == true)
        {
            arma1_O.SetActive(false);
            arma2_O.SetActive(false);
            arma3_O.SetActive(true);
        }
    }

    void PowerCamera()
    {
        if (Input.GetKey(KeyCode.E) && camera == true)//verificaçao de camera
        {
            Timer_Camera -= Time.deltaTime; //tempo de uso de camera
            Camera_hud.fieldOfView = Valor_Camera; //distancia de camera

        }
        if (Timer_Camera <= 0)//verificaçao de tempo de camera
        {
            camera = false;//verificaçao powerup set or diseble
            Camera_hud.fieldOfView = 60;//distancia de camera
        }
        if (Input.GetKeyUp(KeyCode.E) || camera == false)//resert de camera
        {
            Camera_hud.fieldOfView = 60;//distancia de camera
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //ativaçao de armas
        if (other.gameObject.CompareTag("Arma1"))
        {
            arma1_O.SetActive(true);
            Script_Armas.firerate = 0; // fixa novo firehate
        }
        if (other.gameObject.CompareTag("Arma2"))
        {
            arma1_O.SetActive(true);
            Script_Armas.firerate = 0;// fixa novo firehate
        }
        if (other.gameObject.CompareTag("Arma3"))
        {
            arma1_O.SetActive(true);
            Script_Armas.firerate = 0;// fixa novo firehate
        }
        //ativaçao de armas

        //ativaçao powerUp
        if (other.gameObject.CompareTag("PowerUp_Camera"))
        {
            camera = true; //valida coleta do powerUp
            Destroy(other.gameObject);//destroy objeto  
            Timer_Camera = 5;//timer para limite de uso
        }
    }

    [PunRPC]
    public void MoveArm()
    {
        
    }
}
