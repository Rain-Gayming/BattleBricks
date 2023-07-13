﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;
    using Photon.Realtime;

public class ServerManager : MonoBehaviourPunCallbacks
{
	public static ServerManager Instance;
	public List<GameObject> playerControllers;
	public bool isPrivate;
	public bool isServer;

	void Awake()
	{
		if(Instance)
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		Instance = this;
	}

	private void Update() {
		
	}

	public override void OnEnable()
	{
		base.OnEnable();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
		if(scene.buildIndex == 2) // We're in the game scene
		{
			PhotonNetwork.Instantiate("ServerPlayerManager", Vector3.zero, Quaternion.identity);
		}
	}
        
	public override void OnPlayerEnteredRoom(Player newPlayer)
    {
		if(isPrivate && !newPlayer.IsMasterClient){
			PhotonNetwork.Disconnect();
			Debug.Log("Disconecting " + newPlayer.NickName);
		}
    }

	public void ChangePrivate()
	{
		isPrivate = !isPrivate;
	}

	public void ChangeServer()
	{
		isServer = !isServer;
	}
	
}