using UnityEngine;
using System.Collections;

public class EnemyHealthControllerSurvival : MonoBehaviour {

	//Variavel que define a quantidade de vida inicial do personagem
	public int startingHealth = 100;
	
	//Variavel que define a quantidade de vida atual do personagem, somente acessavel atraves das funçoes da instancia
	int currentHealth;

	//Objeto de som que tocara quando o personagem nao tiver mais vida
	public AudioClip deathSound;
	
	//Objeto de som que tocara quando o personagem for atingido
	public AudioClip hurtSound;

	//Objeto relacionado ao componente de som do personagem
	AudioSource enemyAudioSource;
	
	//Objeto controlador de animaçoes, neste caso controlador das animaçoes do personagem
	Animator enemyAnimator;

	//Variavel booleana que controla se o personagem ja perdeu toda vida ou nao
	bool enemyIsDead;

	//Objeto da movimentaçao do personagem que sera desabilitado quando o personagem nao possuir mais vida
	EnemyMovementController enemyMovementController;

	//Objeto de ataque do personagem que sera desabilitado quando o personagem nao possuir mais vida
	EnemyAttackController enemyAttackController;

	LevelS countDeath;

	//Funçao executada logo apos a inicializacao de todos objetos
	void Awake() {
		GameObject levelS = GameObject.FindGameObjectWithTag ("GameController");
		countDeath = levelS.gameObject.GetComponent<LevelS> ();

		//Busca no objeto relacionado a esta classe a referencia Animator (GetComponent<Animator>), relacionando ao objeto
		enemyAnimator = GetComponent<Animator>();

		//Busca no objeto relacionado a esta classe a referencia AudioSource (GetComponent<AudioSource>), relacionando ao objeto
		enemyAudioSource = GetComponent<AudioSource>();

		//Relaciona a quantidade de vida inicial na quantidade de vida atual
		currentHealth = startingHealth;

		//Busca no objeto relacionado a esta classe a referencia EnemyMovementController (GetComponent<EnemyMovementController>), relacionando ao objeto
		enemyMovementController = GetComponent<EnemyMovementController>();

		//Busca no objeto relacionado a esta classe a referencia EnemyAttackController (GetComponent<EnemyAttackController>), relacionando ao objeto
		enemyAttackController = GetComponent<EnemyAttackController>();
	}

	//Funçao chamada quando o personagem recebe um ataque do jogador informando a quantidade a ser retirada
	public void TakeDamage(int damageAmount, int DamageType) {

		//Verifica se o personagem ainda permanece vivo
		if (!enemyIsDead) {//Se sim

			//Variavel para verificar se a quantidade de dano a ser retirado ultrapassa a quantidade de vida do personagem
			int damageAmountCheck = currentHealth - damageAmount;
			
			//Verifica se a quantidade de vida ultrapassa a quantidade de vida do personagem
			if (damageAmountCheck < 0) {//Se sim
				
				//Altera a quantidade a ser retirada para retirar somente o restante
				damageAmount += damageAmountCheck;
			}
			
			//Debug.Log("currentHealth: "+ currentHealth + " | damageAmount: " + damageAmount);//debug para verificar a quantidade atual de vida do personagem e o quanto sera retirado
			
			//Retira da quantidade atual de vida a quantidade de dano recebido
			currentHealth -= damageAmount;

			//Define o clipe de audio para o audio de dano do personagem
			enemyAudioSource.clip = hurtSound;
			
			//Toca o clipe de audio relacionado a esta fonte de audio
			enemyAudioSource.Play ();
			
			//Verifica se a quantidade de vida esta maior ou igual a zero (currentHealth <= 0) e (&&) se o personagem nao esta com a variavel sem vida igual a verdadeiro (!isDead)
			if (currentHealth <= 0 && !enemyIsDead) {//Se sim
				
				//Chama a funçao que finaliza as funçoes do personagem nesta sessao da cena
				Death ();
			}
			else {//Se nao

				//Aciona a animaçao de dano
				enemyAnimator.SetTrigger("Hit");
			}
		}
	}

	//Funçao para encerrar as funçoes do personagem, chamada quando o personagem nao possui mais quantidade de vida restante
	void Death() {

		//Altera a variavel para que esta funçao nao seja chamada novamente
		enemyIsDead = true;

		//Repassa ao controlador de animaçoes o valor "Die" para ativar a animaçao final
		enemyAnimator.SetBool ("Die", true);

		//Define o clipe de audio para o audio final do personagem
		enemyAudioSource.clip = deathSound;
		
		//Toca o clipe de audio relacionado a esta fonte de audio
		enemyAudioSource.Play();

		//Desabilita a funçao de movimentaçao do personagem
		enemyMovementController.enabled = false;
		
		//Desabilita a funçao de ataque do personagem
		enemyAttackController.enabled = false;

		countDeath.CountAEnemy ();
	}
	//metodo com retorno do status de vida do personagem, utilizado pelos inimigos para calcular a movimentaçao e os ataques
	public bool EnemyLifeStatus() {
		return !enemyIsDead;
	}
}
