using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class KillFeed : MonoBehaviourPun, IPunObservable
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(killFeedString);
        }
        else
        {
            killFeedString = (string)stream.ReceiveNext();
        }
    }

    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    public void UpdateBattleLog(string weapon, string killer, string playerWhoDied)
    {
        killFeedString += killer + " " + weapon + " " + playerWhoDied + "\n";
        pv.RPC("SyncScoreboard", RpcTarget.All, killFeedString);
    }

    [PunRPC]
    void SyncScoreboard(string Sync)
    {
        killFeed.text = Sync;
    }

}