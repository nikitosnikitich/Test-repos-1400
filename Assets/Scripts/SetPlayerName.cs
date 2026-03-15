using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class SetPlayerName : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField nicknameField;
    //
    public override void OnConnectedToMaster()
    {
        LoadNickName();
    }
    private void LoadNickName()
    {
        string playerName = PlayerPrefs.GetString("SaveNickName");
        if(string.IsNullOrEmpty(playerName))
        {
            playerName = "Player " + Random.Range(0, 1000);
        }
        PhotonNetwork.NickName = playerName;
        nicknameField.text = playerName;
    }
    public void ChangeName()
    {
        PlayerPrefs.SetString("SaveNickName", nicknameField.text);
        LoadNickName();
    }
}
