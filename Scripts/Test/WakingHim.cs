using UnityEngine;
using System.Collections;

public class WakingHim : MonoBehaviour {

	EnemyMovementController wakeScript;
	public GameObject destinationEnemy;

	void Awake() {
		wakeScript = destinationEnemy.gameObject.GetComponentInChildren<EnemyMovementController> ();
		Debug.Log ("wakeStatus: " + wakeScript.enemyWakedStatus);
		wakeScript.awakeEnemy ();
		Debug.Log ("wakeStatus: " + wakeScript.enemyWakedStatus);
	}
}
