using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class enemy : MonoBehaviourPun
{
    public int maxHP = 100;
    public PhotonView viewPhoton;
    int currentHP;
    public int viewID;
    
    // Start is called before the first frame update
    [PunRPC]void Start()
    {
        currentHP = maxHP;
        viewID = GetComponent<PhotonView>().ViewID;
    }

    [PunRPC]public void TakeDamage(int damage)
    {
        
            currentHP -= damage;

            //animasi kena damage
        
            if(currentHP <= 0)
            {
                Die(viewID);
                this.photonView.RPC("Die", RpcTarget.AllViaServer, viewID);
            }
        
        
        
        
    }

    [PunRPC]void Die(int viewID)
    {
        Debug.Log("Enemy down");

        // animasi mati

        
        PhotonNetwork.Destroy(PhotonView.Find(viewID).gameObject);

        
        
    }
    // Update is called once per frame
    void Update()
    {
          
    }

}
