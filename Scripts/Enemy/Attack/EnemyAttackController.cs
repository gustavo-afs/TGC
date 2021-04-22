using UnityEngine;
using System.Collections;

public class EnemyAttackController : MonoBehaviour {

	//Objeto controlador de animaçoes, neste caso controlador das animaçoes do personagem
	Animator enemyAnimator;

	//variavel que especifica o tempo minimo entre os ataques
	public float enemyAttackTime = 1f;

	//Variavel que controla quando sera possivel atacar de acordo com o tempo definido em enemyAttackTime
	float enemyStopWatch;

	//Variavel que controla a quantidade de pontos de vida que o ataque causara
	public int enemyAttackDamage = 20;

	//Objeto relacionado ao jogador, que este personagem seguira
	GameObject playerToAttack;

	//Variavel que recebera o valor verdadeiro se o jogador estiver dentro do campo de ataque deste personagem
	bool playerInRange;

	//Funçao executada logo apos a inicializacao de todos objetos
	void Awake() {

		//Buscara dentro da cena o objeto definido com a tag Player.
		playerToAttack = GameObject.FindGameObjectWithTag("Player");

		//Carrega na variavel enemyAnimator o componente Animator do personagem
		enemyAnimator = GetComponent<Animator>();
	}

	//Metodo executado a partir do momento que o objeto entra em colisao com outro, fornecendo ao metodo o nome do objeto colidido
	void OnTriggerEnter(Collider collidedObject) {

		//Verifica se o objeto colidido corresponde a um Player, verificando se a tag corresponde
		if(collidedObject.tag == "Player") {//Se sim

			//Altera o valor para verdadeiro, definindo que um jogador esta dentro da distancia permitida de ataque
			playerInRange = true;
		}
	}

	//Metodo executado a partir do momento que o objeto sai da colisao com outro, fornecendo ao metodo o nome do objeto que deixou a colisao
	void OnTriggerExit(Collider collidedObject) {
		//Verifica se o objeto que deixou a colisao corresponde a um Player, verificando se a tag corresponde
		if(collidedObject.tag == "Player") {//Se sim
			
			//Altera o valor para falso, definindo que um jogador deixou a distancia permitida de ataque
			playerInRange = false;
		}
	}


	//Funcao executada em uma taxa fixa em cada frame
	void FixedUpdate() {
		
		//Acrescenta a quantidade de tempo que passou entre um frame e outro a variavel
		enemyStopWatch += Time.deltaTime;

		//Verifica se um jogador esta dentro dos limites do ataque (playerInRange) e se o cronometro do ataque (enemyStopWatch) e menor do que a quantidade minima para o lançamento (>=enemyAttackTime)
		if(playerInRange && enemyStopWatch >= enemyAttackTime) {//Se sim

			//Inicia o ataque ao jogador
			HitIt();
		}
	}

	//Metodo relacionado ao ataque ao jogador
	void HitIt() {

		//define o valor do cronometro do ataque igual a 0, fazendo com que o inimigo nao possa atacar enquanto nao ultrapassar o tempo minimo
		enemyStopWatch = 0f;

		//Altera a animaçao para a de ataque
		enemyAnimator.SetTrigger("Attack");

		//Retira os pontos de vida do jogador
		playerToAttack.gameObject.GetComponent<PlayerHealthController>().TakeDamage(enemyAttackDamage);
	}
}