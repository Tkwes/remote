using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FindPlayer : MonoBehaviour
{
    GameObject player;
    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        view = player.GetComponent<PhotonView>();
        if (!view.IsMine)
        {
            gameObject.SetActive(false);
        }
    }
}
