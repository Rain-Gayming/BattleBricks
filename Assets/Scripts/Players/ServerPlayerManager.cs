using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.IO;
using Sirenix.OdinInspector;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ServerPlayerManager : MonoBehaviour
{
	PhotonView pv;

	public GameObject playerPrefab;
	string savePath;
	public GameObject serverPrefab;
	public PlayerController playerController;

	int kills;
	int deaths;
	Transform lastSpawnpoint;
	Transform spawnpoint;


	void Awake()
	{
		pv = GetComponent<PhotonView>();
	}

	void Start()
	{
		if(pv.IsMine)
		{
			CreateController();
		}
		
	}

	void CreateController()
	{
		if(!ServerManager.Instance.isServer || PhotonNetwork.IsMasterClient == false){

			if(lastSpawnpoint == null){
				
				spawnpoint = SpawnManager.Instance.GetSpawnpoint();
				lastSpawnpoint = spawnpoint;
			}else{
				spawnpoint = SpawnManager.Instance.GetSpawnpoint();
				if(spawnpoint == lastSpawnpoint){
					Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
				lastSpawnpoint = spawnpoint;
				}
				lastSpawnpoint = spawnpoint;
			}
			playerPrefab = PhotonNetwork.Instantiate("Player", spawnpoint.position, spawnpoint.rotation, 0, new object[] { pv.ViewID });
			playerController = playerPrefab.GetComponent<PlayerController>();
			ServerManager.Instance.playerControllers.Add(playerPrefab);
		}else if(ServerManager.Instance.isServer){
			Screen.fullScreen = false;
			PhotonNetwork.Instantiate(Path.Combine("ServerController"), Vector3.zero, Quaternion.identity, 0, new object[] { pv.ViewID });		
			
		}
		
	}
	void Respawn()
	{
		if(lastSpawnpoint == null){
			
			spawnpoint = SpawnManager.Instance.GetSpawnpoint();
			lastSpawnpoint = spawnpoint;
		}else{
			spawnpoint = SpawnManager.Instance.GetSpawnpoint();
			if(spawnpoint == lastSpawnpoint){
				Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
			lastSpawnpoint = spawnpoint;
			}
			lastSpawnpoint = spawnpoint;
		}
		playerPrefab.transform.position = spawnpoint.position;
		playerPrefab.transform.rotation = spawnpoint.rotation;
	}


	public void Die()
	{
		//PhotonNetwork.Destroy(controller);
		//CreateController();
		Respawn();

		deaths++;

		Hashtable hash = new Hashtable();
		hash.Add("deaths", deaths);
		PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
	}

	public void GetKill()
	{
		pv.RPC(nameof(RPC_GetKill), pv.Owner);
	}

	[PunRPC]
	void RPC_GetKill()
	{
		kills++;

		Hashtable hash = new Hashtable();
		hash.Add("kills", kills);
		PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
	}

	public static ServerPlayerManager Find(Player player)
	{
		return FindObjectsOfType<ServerPlayerManager>().SingleOrDefault(x => x.pv.Owner == player);
	}
}