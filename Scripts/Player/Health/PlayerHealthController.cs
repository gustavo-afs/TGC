using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour {

	//Variavel que define a quantidade de vida inicial do personagem
	public int startingHealth = 100;

	//Variavel que define a quantidade de vida atual do personagem, somente acessavel atraves das funçoes da instancia
	int currentHealth;

	//Objeto Slider que exibe na tela a quantidade de vida restante do personagem
	public Slider healthSlider;

	//Objeto de imagem que piscara quando o player for atingido
	public Image damageImage;

	//Variavel que define a velocidade em que a imagem do personagem atingido (DamageImage) desaparecera
	public float flashSpeed = 5f;

	//Objeto com a cor que a imagem do personagem atingido (DamageImage) tera
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	//Variavel booleana que controla se o personagem foi atingido para criar o efeito na tela
	bool damagedEffect;

	//Objeto de som que tocara quando o personagem nao tiver mais vida
	public AudioClip deathSound;

	//Objeto de som que tocara quando o personagem for atingido
	public AudioClip hurtSound;

	//Objeto relacionado ao componente de som do personagem
	AudioSource playerAudioSource;

	//Objeto controlador de animaçoes, neste caso controlador das animaçoes do personagem
	Animator playerAnimator;

	//Objeto da movimentaçao do personagem que sera desabilitado quando o personagem nao possuir mais vida
	PlayerMovementController playerMovementController;

	//Objeto do ataque de espada do personagem que sera desabilitado quando o personagem nao possuir mais vida
	PlayerSwordController playerSwordController;

	//Objeto do ataque de feitiço do personagem que sera desabilitado quando o personagem nao possuir mais vida
	PlayerCastController playerCastController;

	//Variavel booleana que controla se o personagem ja perdeu toda vida ou nao
	bool playerIsDead;

	//Funçao executada logo apos a inicializacao de todos objetos
	void Awake() {

		//Busca no objeto relacionado a esta classe a referencia Animator (GetComponent<Animator>), relacionando ao objeto
		playerAnimator = GetComponent<Animator>();

		//Busca no objeto relacionado a esta classe a referencia PlayerMovementController (GetComponent<PlayerMovementController>), relacionando ao objeto
		playerMovementController = GetComponent<PlayerMovementController>();

		//Busca no objeto relacionado a esta classe a referencia AudioSource (GetComponent<AudioSource>), relacionando ao objeto
		playerAudioSource = GetComponent<AudioSource>();

		//Busca no objeto relacionado a esta classe a referencia PlayerSwordController (GetComponent<PlayerSwordController>), relacionando ao objeto
		playerSwordController = GetComponent<PlayerSwordController>();

		//Busca no objeto relacionado a esta classe a referencia PlayerCastController (GetComponent<PlayerCastController>), relacionando ao objeto
		playerCastController = GetComponent<PlayerCastController>();

		//Relaciona a quantidade de vida inicial na quantidade de vida atual
		currentHealth = startingHealth;
	}
	
	//Funcao executada uma vez por frame
	void Update() {
		//Verifica se a variavel que identifica se o personagem foi atingido esta verdadeira 
		if (damagedEffect) { //Se sim

			//Define a cor da imagem na tela, dando o efeito do personagem atingido
			damageImage.color = flashColour;
		} else { //Se nao

			//Transforma a cor da imagem atual (damageImage.color) em transparente (Color.clear) de forma suave (.Lerp), na velocidade definida por uma taxa fixa de tempo de frame a frame (flashSpeed * Time.deltime)
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
	
		}

		//Apos o efeito aparecer (damagedEffect == true) ou nao (damagedEffect == false), a variavel recebe o valor falso
		damagedEffect = false;
	}

	//Funçao chamada quando o personagem recebe um ataque de um inimigo informando a quantidade a ser retirada
	public void TakeDamage(int damageAmount) {

		//Verifica se o personagem ainda permanece vivo
		if (!playerIsDead) {//se sim
			//Altera a variavel para verdadeiro, para que no proximo frame o efeito da imagem apareça
			damagedEffect = true;

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

			//Altera a barra de vida da interface grafica atualizando para o novo valor
			healthSlider.value = currentHealth;

			//Define o clipe de audio para o audio de dano do personagem
			playerAudioSource.clip = hurtSound;

			//Toca o clipe de audio relacionado a esta fonte de audio
			playerAudioSource.Play ();

			//Verifica se a quantidade de vida esta maior ou igual a zero (currentHealth <= 0) e (&&) se o personagem nao esta com a variavel sem vida igual a verdadeiro (!isDead)
			if (currentHealth <= 0 && !playerIsDead) {//Se sim

				//Chama a funçao que finaliza as funçoes do personagem nesta sessao da cena
				Death ();
			}
		}
	}
	public void GiveHealth(int healthAmount) {
		int healthAmountCheck = currentHealth + healthAmount;
		//Verifica se a quantidade de vida ultrapassa a quantidade de vida do personagem
		if (healthAmountCheck > startingHealth) {//Se sim
			//Altera a quantidade a ser retirada para retirar somente o restante
			healthAmount = startingHealth - currentHealth;
		}
		currentHealth += healthAmount;

		//Altera a barra de vida da interface grafica atualizando para o novo valor
		healthSlider.value = currentHealth;
	}

	//Funçao para encerrar as funçoes do personagem, chamada quando o personagem nao possui mais quantidade de vida restante
	void Death() {

		//Altera a variavel para que esta funçao nao seja chamada novamente
		playerIsDead = true;

		//Repassa ao controlador de animaçoes o valor "Die" para ativar a animaçao final
		playerAnimator.SetTrigger ("Die");

		//Define o clipe de audio para o audio final do personagem
		playerAudioSource.clip = deathSound;
		
		//Toca o clipe de audio relacionado a esta fonte de audio
		playerAudioSource.Play();

		//Desabilita a funçao de movimentaçao do personagem
		playerMovementController.enabled = false;

		//Desabilita a funçao de ataque com espada do personagem
		playerSwordController.enabled = false;

		//Desabilita a funçao de feitiço do personagem
		playerCastController.enabled = false;
	}

	//metodo com retorno do status de vida do personagem, utilizado pelos inimigos para calcular a movimentaçao e os ataques
	public bool PlayerLifeStatus() {
		return !playerIsDead;
	}
}