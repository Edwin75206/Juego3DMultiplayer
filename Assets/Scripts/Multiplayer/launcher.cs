using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class launcher : MonoBehaviourPunCallbacks
{
    public PhotonView playerPrefab;
    public Transform spawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado al servidor master");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
    }
}