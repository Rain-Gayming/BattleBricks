using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class ServerListItem : MonoBehaviour
{
	[SerializeField] TMP_Text nameText;
	public TMP_Text pingText;
	public TMP_Text playersText;

	public RoomInfo info;

	private void Update() {
		playersText.text = info.PlayerCount.ToString() + "/" + info.MaxPlayers.ToString(); 
	}

	public void SetUp(RoomInfo _info)
	{
		info = _info;
		nameText.text = _info.Name;
	}

	public void SetInfo()
	{
		Launcher.Instance.selectedRoom = info;
	}
}