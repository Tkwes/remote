using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class Network : MonoBehaviourPunCallbacks
{
    public Vector3[] SpawnPlayer;
    GameObject player;
    int i = 0;

    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount != i && !player)
        {
            Spawn();
            i++;
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount < i)
        {
            i--;
        }
    }

    void Spawn()
    {
        //Instantiate(Cam, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

        player = PhotonNetwork.Instantiate("Player", SpawnPlayer[0], new Quaternion(0, 0, 0, 0));

        player.name = "Player";
    }
}

