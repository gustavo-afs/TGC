using UnityEngine;
using System.Collections;

public class CastAttackControllerSurvival : MonoBehaviour {
	
	//Variavel que controla a velocidade de movimentaçao do feitiço
	public float castSpeed = 30;
	
	//Variavel que controla a quantidade de pontos de vida que o feitiço causara
	public int castDamage = 20;

	//Variavel que controlara se este ataque ja atingiu algum inimigo
	bool AlreadyAttacked = false;

	//Funçao executada quando o objeto pai deste script foi habilitado
	void Start() {
		
		//altera a velocidade do corpo rigido do feitiço multiplicando a movimentaçao do vetor Z que define a direçao para frente ou para tras (transform.forward que significa Vector3(0,0,1)) pela velocidade definida (castSpeed)
		rigidbody.velocity = transform.forward * castSpeed;
		
		//Destroi o feitiço apos 0.4 de um segundo
		Destroy(gameObject, 0.4f);
	}
	
	//Funçao executada toda vez que o objeto colidir com algum objeto, fornecendo o objeto colidido
	void OnTriggerEnter(Collider collidedObject) {

		//Verifica se o objeto colidido esta na camada "Collidable"
		if (collidedObject.tag == "Collidable") {//Se sim
			//Relaciona o componente EnemyHealthController a variavel enemyHealth
			EnemyHealthControllerSurvival enemyHealthController = collidedObject.GetComponent <EnemyHealthControllerSurvival> ();

			//Verifica se o objeto colidido possui o script necessario para retirar quantidade de vida e se este feitiço nao foi usado antes
			if (enemyHealthController != null && AlreadyAttacked == false) {//Se possuir o script e o ataque nao tiver acertado ninguem
				//Informa que o ataque ja foi utilizado atribuindo verdadeiro a variavel
				AlreadyAttacked = true;

				//Envia a quantidade de vida a ser retirada do personagem
				enemyHealthController.TakeDamage(castDamage, 1);
			}

			//Destroi o proprio feitiço apos a colisao
			Destroy(gameObject);
		}
	}
}