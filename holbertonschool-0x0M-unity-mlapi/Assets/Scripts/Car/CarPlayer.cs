using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CarPlayer : NetworkBehaviour{

	public MeshRenderer carMesh;
	public MeshRenderer mapMarkerMesh;
	public float jumpForce = 2500f;
	public float boostForce = 2500f;
	public bool canJump = false;
	public bool canBoost = false;
	public GameObject playerCamera;

    public override void OnNetworkSpawn()
    {
        //base.OnNetworkSpawn();

		GameObject.Find("Scene Camera").GetComponent<Camera>().enabled = false;
		GameObject.Find("Scene Camera").GetComponent<AudioSource>().enabled = false;
		GameObject.Find("Scene Camera").GetComponent<AudioListener>().enabled = false;

		GameObject playerCamera = gameObject.transform.Find("Player Camera").gameObject;
		playerCamera.SetActive(true);

		//Find car body transform nested in CarPlayer my searching full heirarchy
		Transform[] children = GetComponentsInChildren<Transform>();
		foreach (Transform child in children)
		{
			if (child.name == "car_body")
			{
				carMesh = child.gameObject.GetComponent<MeshRenderer>();
			}
			if (child.name == "Map-Marker")
			{
				mapMarkerMesh = child.gameObject.GetComponent<MeshRenderer>();
			}

			if (!IsLocalPlayer)
			{
				if (child.name == "CarMusicPlayer")
				{
					child.gameObject.GetComponent<AudioSource>().enabled = false;
				}
			}
		}

		Color randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
		carMesh.material.color = randomColor;
		if (mapMarkerMesh)
		{
			mapMarkerMesh.material.color = randomColor;
		}

		if (!IsLocalPlayer)
		{
			if (gameObject.transform.Find("Player Camera"))
			{
				Destroy(gameObject.transform.Find("Player Camera").gameObject);
			}
			return;
		}

	}

	// Update is called once per frame
	void Update () {

	}

    public override void OnDestroy(){
		if (IsLocalPlayer) {
			GameObject.Find("Scene Camera").GetComponent<Camera>().enabled = true;
			GameObject.Find("Scene Camera").GetComponent<AudioSource>().enabled = true;
			GameObject.Find("Scene Camera").GetComponent<AudioListener>().enabled = true;
			if (playerCamera != null)
			{
				Debug.Log("Destroyed " + transform.name + "'s camera! Muhahahahaha!");
				Destroy(playerCamera);
			}
		}
	}
		
}
