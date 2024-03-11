using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	//[SyncVar(hook = "OnChangeTime")]
	public float gameTimeLeft = 0f;

	public float totalGameMinutes = 3f;
	//[SyncVar]
	public bool gameStarted = false;
	public Text gameTime; 
	//[SyncVar]
	public int totalSpots = 0;
	//[SyncVar(hook = "OnParkingSpotsReachedChanged")]
	public int parkingSpotsReached = 0;

	public Text parkingSpotsLeft;

	//[SyncVar(hook = "OnPowerPointsChanged")]
	public float powerPoints = 0f;

	public float powerPointsFactor = 2.5f;

	public Text powerPointsCollected;


	// Use this for initialization
	void Start () {
		gameTimeLeft = 60 * totalGameMinutes;
		gameStarted = true;
		//if (!isServer) {
		//	parkingSpotsLeft.text = "Parking Spots: " + parkingSpotsReached + "/" + totalSpots;
		//	powerPointsCollected.text = "Power: " + powerPoints.ToString("N0");
		//}
	}
	
	// Update is called once per frame
	void Update () {

		//if (isServer) {
		//	if (gameStarted) {
		//		gameTimeLeft -= Time.deltaTime;

		//		if (totalSpots <= 0) {
		//			//Set up game as host with starting stats
		//			totalSpots = GameObject.Find ("GoalSpawner").GetComponent<GoalSpawner> ().numberOfGoals;
		//			parkingSpotsLeft.text = "Parking Spots: " + parkingSpotsReached + "/" + totalSpots;
		//		}
		//	}
		//} 
		
	}

	void OnChangeTime (float timeLeft){
		gameTime.text = "Time Left: " + timeLeft.ToString("N0") + " Seconds";
	}

	void OnParkingSpotsReachedChanged(int parkingSpots){
		parkingSpotsLeft.text = "Parking Spots: " + parkingSpots + "/" + totalSpots;
	}

	void OnPowerPointsChanged(float powerPointsCol){
		powerPointsCollected.text = "Power: " + powerPointsCol.ToString("N0");
	}
}
