using UnityEngine;
using System.Collections;

public class TakeDamageTrigger : MonoBehaviour {

	public GameObject sceneController;
	public string damageTypeScene;
	
	public void SetDamageType (string damageID) {
		damageTypeScene = damageID;

	}

	void OnTriggerEnter(Collider collidedObject) {
		//Verifica se o objeto colidido esta na camada "Collidable"
		string advance = collidedObject.tag;
		VerifyAndAdvance(advance);

	}
	public void VerifyAndAdvance(string collidedObject) {
		if (collidedObject == damageTypeScene) {//Se sim
			sceneController.gameObject.GetComponent<Level01>().AdvancePart();
		}
	}
}
