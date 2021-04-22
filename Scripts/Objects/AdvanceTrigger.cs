using UnityEngine;
using System.Collections;

public class AdvanceTrigger : MonoBehaviour {

	public GameObject sceneController;
	
	//Metodo executado a partir do momento que o objeto entra em colisao com outro, fornecendo ao metodo o nome do objeto colidido
	void OnTriggerEnter(Collider collidedObject) {
		//Verifica se o objeto colidido corresponde a um Player, verificando se a tag corresponde
		if(collidedObject.tag == "Player") {//Se sim
			
			sceneController.gameObject.GetComponent<Level01>().AdvancePart();
		}
	}
}
