using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;
using Sirenix.OdinInspector;

public class Launcher : MonoBehaviourPunCallbacks
{
	public static Launcher Instance;

	[BoxGroup("Menus")]
	public UIUtilities menu;
	[BoxGroup("Menus")]
	public GameObject homeMenu;
	[BoxGroup("Menus")]
	public GameObject serverMenu;

	[BoxGroup("Create Server")]
	[SerializeField] TMP_InputField serverNameInputField;
	
	[BoxGroup("Create Server")]
	[SerializeField] TMP_Text errorText;

	[BoxGroup("Server Lists")]
	[SerializeField] Transform serverListContent;

	[BoxGroup("Server Lists")]
	[SerializeField] GameObject serverListItemPrefab;
	
	public TMP_Text timeText;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		PhotonNetwork.ConnectUsingSettings();
	}

	private void Update() {
		timeText.text = System.DateTime.Now.TimeOfDay.Hours.ToString() + " : " + System.DateTime.Now.TimeOfDay.Minutes.ToString();
	}

	public override void OnConnectedToMaster()
	{
		PhotonNetwork.JoinLobby();
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	public override void OnJoinedLobby()
	{
		// open home menu
		menu.ChangeMenu(homeMenu);
	}

	public void CreateRoom()
	{
		if(string.IsNullOrEmpty(serverNameInputField.text))
		{
			return;
		}
		PhotonNetwork.CreateRoom(serverNameInputField.text);
		//MenuManager.instance.OpenMenu("loading");
	}

	public override void OnJoinedRoom()
	{
		menu.ChangeMenu(serverMenu);
	}

	public override void OnMasterClientSwitched(Player newMasterClient)
	{
		
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		errorText.text = "Room Creation Failed: " + message;
		Debug.LogError("Room Creation Failed: " + message);
		menu.ChangeMenu(homeMenu);
	}

	public void StartGame()
	{
		PhotonNetwork.LoadLevel(1);
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
		MenuManager.instance.OpenMenu("loading");
	}

	public void JoinRoom(RoomInfo info)
	{
		PhotonNetwork.JoinRoom(info.Name);
		MenuManager.instance.OpenMenu("loading");
	}

	public override void OnLeftRoom()
	{
		menu.ChangeMenu(homeMenu);
	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		foreach(Transform trans in serverListContent)
		{
			Destroy(trans.gameObject);
		}

		for(int i = 0; i < roomList.Count; i++)
		{
			if(roomList[i].RemovedFromList)
				continue;
			Instantiate(serverListItemPrefab, serverListContent).GetComponent<ServerListItem>().SetUp(roomList[i]);
		}
	}
}