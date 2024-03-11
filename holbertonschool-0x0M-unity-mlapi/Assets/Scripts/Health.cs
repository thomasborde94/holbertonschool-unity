using UnityEngine;
using Unity.Netcode;

public class Health : NetworkBehaviour {

	public const int maxHealth = 100;

	public bool destroyOnDeath;

	//[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;

	public RectTransform healthBar;

	public void TakeDamage(int amount){
		//if (!isServer)
		//	return;

		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			if (destroyOnDeath)
			{
				Destroy(gameObject);
			} 
			else
			{
				currentHealth = maxHealth;

				// called on the Server, will be invoked on the Clients
				Respawn();
			}
		}
	}

	void OnChangeHealth (int currentHealth){
		healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Respawn") {
			GetComponent<Rigidbody> ().isKinematic = true;
			Respawn ();
		}
	}

	void Respawn(){
        if (IsLocalPlayer)
        {
            // Set the player’s position to origin
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

}