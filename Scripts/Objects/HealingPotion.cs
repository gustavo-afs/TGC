using UnityEngine;
using System.Collections;

public class HealingPotion : MonoBehaviour {

	//Variavel que controla a quantidade de pontos de vida que o ataque causara
	public int healthAmount = 20;

	//Variavel que recebera o valor verdadeiro se o jogador estiver dentro do campo de ataque deste personagem
	bool playerInRange;

	GameObject playerObject;

	//Funçao executada logo apos a inicializacao de todos objetos
	void Awake() {

	}

	//Metodo executado a partir do momento que o objeto entra em colisao com outro, fornecendo ao metodo o nome do objeto colidido
	void OnTriggerEnter(Collider collidedObject) {
		//Verifica se o objeto colidido corresponde a um Player, verificando se a tag corresponde
		if(collidedObject.tag == "Player") {//Se sim
			playerObject = collidedObject.gameObject;
			//Altera o valor para verdadeiro, definindo que um jogador esta dentro da distancia permitida de ataque
			HealIt();
			Destroy(gameObject);
		}
	}

	//Metodo relacionado ao ataque ao jogador
	void HealIt() {

		PlayerHealthController playerHealth = playerObject.gameObject.GetComponent<PlayerHealthController>();
		playerHealth.GiveHealth (healthAmount);
	}
}