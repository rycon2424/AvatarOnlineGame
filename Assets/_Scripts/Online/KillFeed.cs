using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class KillFeed : MonoBehaviourPun
{
    public static KillFeed killfeedInstance;

    public Text killFeed;
    public PhotonView pv;
    public string killFeedString;

    void Awake()
    {
        if (killfeedInstance == null)
        {
            killfeedInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    public void UpdateBattleLog(string weapon, string killer, string playerWhoDied)
    {
        killFeedString += killer + " " + weapon + " " + playerWhoDied + "\n";
        Debug.Log("killFeedString");
        pv.RPC("SyncChatToMaster", RpcTarget.MasterClient, killFeedString);
    }

    [PunRPC]
    void SyncChatToMaster(string stringtoSync)
    {
        pv.RPC("SyncChatToClients", RpcTarget.AllViaServer, stringtoSync);
    }

    [PunRPC]
    void SyncChatToClients(string stringtoSync)
    {
        killFeed.text += stringtoSync;
        killFeedString = killFeed.text;
    }

}
