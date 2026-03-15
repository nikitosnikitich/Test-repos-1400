using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;
public class RoomItem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text roomName;
    RoomInfo info;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUp(RoomInfo info)
    {
        this.info = info;
        roomName.text = info.Name;
    }
    public void OnClick()
    {
        ConnectionToServer.Instance.JoinRoom(this.info);
    }
}
