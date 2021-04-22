using UnityEngine;
using System.Collections;

public class EnemyMovementController : MonoBehaviour {

	//Objeto do personagem do jogador que sera seguido pelo inimigo
	GameObject playerGameObject;

	NavMeshAgent enemyNavMesh;

	//Objeto controlador de animaçoes, neste caso controlador das animaçoes do personagem
	Animator enemyAnimator;

	//Variavel que controla o Status de ativaçao do inimigo
	public bool enemyWakedStatus;

	//Objeto de ataque do personagem que sera desabilitado quando o personagem nao possuir mais vida
	EnemyAttackController enemyAttackController;

	//Funçao executada logo apos a inicializacao de todos objetos
	void Awake() {

		//localiza um objeto relacionado com a tag de identificaçao Player
		playerGameObject = GameObject.FindGameObjectWithTag("Player");

		//Busca no objeto relacionado a esta classe a referencia NavMeshAgent (GetComponent<NavMeshAgent>), relacionando ao objeto
		enemyNavMesh = GetComponent<NavMeshAgent>();

		//Variavel definida como falso, para definir o inimigo inicialmente sem estar acordado
		enemyWakedStatus = false;

		//Busca no objeto relacionado a esta classe a referencia Animator (GetComponent<Animator>), relacionando ao objeto
		enemyAnimator = GetComponent<Animator>();

		//Busca no objeto relacionado a esta classe a referencia EnemyAttackController (GetComponent<EnemyAttackController>), relacionando ao objeto
		enemyAttackController = GetComponent<EnemyAttackController>();
	}

	//Metodo executado uma vez a cada frame, nem sempre em uma taxa fixa de tempo
	void Update () {

		//Variavel que recebe o retorno referente ao status de vida do jogador
		bool playerLifeStatus = playerGameObject.gameObject.GetComponent<PlayerHealthController>().PlayerLifeStatus();

		//relaciona ao objeto playerTransform a posiçao atual do jogador
		Transform playerTransform = playerGameObject.gameObject.transform;

		//Verifica o status de vida do jogador esta retornando o valor falso, mostrando que o jogador nao possui mais pontos de vida
		if (!playerLifeStatus || !enemyWakedStatus) {//Se nao possuir mais pontos de vida ou se o inimigo nao estiver acordado

			//Define o objeto de NavMesh como desativado, fazendo com que seja impossivel o inimigo seguir o jogador
			enemyNavMesh.enabled = false;

			//Desabilita a funçao de ataque do personagem
			enemyAttackController.enabled = false;

			//O personagem volta ao seu estado comum pois nao possui um jogador para atacar
			enemyAnimator.SetBool ("Wake", false);
		} else {//Se possuir pontos de vida ou se o inimigo estiver acordado

			//Define o estado da animaçao como acordado, fazendo com que o inimigo comece a andar
			enemyAnimator.SetBool ("Wake", true);

			//Ativa o NavMesh fazendo com que o inimigo possua o mapeamento do cenario necessario para seguir o jogador
			enemyNavMesh.enabled = true;

			//Habilita a funçao de ataque do personagem
			enemyAttackController.enabled = true;

			//Faz com que o inimigo tenha como ponto de destino a posiçao do jogador
			enemyNavMesh.SetDestination (playerTransform.position);
		}
	}
	public void awakeEnemy() {
		Debug.Log ("awakeEnemy EnemyMovementController");
		enemyWakedStatus = true;
	}
}
