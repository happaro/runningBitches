using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gameplay : MonoBehaviour
{
	public GameObject playerPrefab;

	public Transform respawnPoints;
	public List<PlayerController> players;

	void Start () 
	{
		respawnPoints = GameObject.Find("RespawnPoints").transform;
		foreach (var playerSet in PlayerManager.Instance.playersSettings)
		{
			GameObject tmpObj = Instantiate(playerPrefab, respawnPoints.GetChild(playerSet.playerNumber).position, respawnPoints.GetChild(playerSet.playerNumber).rotation) as GameObject;
			players.Add(tmpObj.GetComponent<PlayerController>());
			tmpObj.GetComponent<PlayerController>().currentPlayerNum = playerSet.playerNumber;
			tmpObj.GetComponent<PlayerController>().currentJoyNum = playerSet.joystickNumber;
			tmpObj.GetComponent<SkinChanger>().ChangeMaterial(playerSet.colorNumber);
			tmpObj.GetComponent<SkinChanger>().ChangeModel(playerSet.characterNumber);
		}
	}
}
