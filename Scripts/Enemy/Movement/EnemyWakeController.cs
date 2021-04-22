using UnityEngine;
using System.Collections;

public class EnemyWakeController : MonoBehaviour {

	//Objeto relacionado a classe de movimentaçao do inimigo
	EnemyMovementController enemyMovementController;

	//Funçao executada logo apos a a inicializacao de todos objetos
	void Awake()
	{
		//Busca no objeto relacionado a esta classe a referencia EnemyMovementController (GetComponent<EnemyMovementController>), relacionando ao objeto
		enemyMovementController = gameObject.GetComponentInChildren<EnemyMovementController> ();
	}
	
	//Metodo executado a partir do momento que o objeto entra em colisao com outro, fornecendo ao metodo o nome do objeto colidido
	void OnTriggerEnter(Collider collidedObject) {
		//Verifica se o objeto colidido possui a tag de personagem (Player)
		if (collidedObject.CompareTag("Player")) {//Se sim
			WakeEnemy();
		}
	}

	public void WakeEnemy() {
		//Define a variavel que acorda o inimigo como verdadeiro
		enemyMovementController.enemyWakedStatus = true;
		//Desabilida o objeto do jogo com o colisor que verifica se o personagem ultrapassou a barreira limite, para nao agir como gatilho do script de EnemyAttackController
		gameObject.GetComponent<SphereCollider>().enabled = false;
	}
}
