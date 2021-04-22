using UnityEngine;
using System.Collections;

public class PlayerSwordController : MonoBehaviour {

	//Objeto que define o objeto de controles mobile
	public TouchController playerButtons;
	
	//Objeto controlador de animaçoes, neste caso controlador das animaçoes do personagem
	Animator playerAnimator;
	
	//Objeto relacionado a posiçao e a fonte de audio do ataque de espada alem das funçoes de linha e colisao
	public GameObject swordAtPoint;

	//variavel que especifica o tempo minimo de lançamento entre os ataques de espada
	public float swordTime = 1.2f;

	//Variavel que controla quando sera possivel lançar o ataque de espada de acordo com o tempo definido em swordTime
	float swordStopWatch;

	//Objeto relacionado ao componente de som do ataque de espada, diferente do PlayerHealth Controller esta fonte de audio ja sera populada pois so exite um som a tocar
	AudioSource swordAudioSource;

	//Variavel que controla a quantidade de pontos de vida que o ataque de espada causara
	public int swordDamage = 40;

	//Variavel que controla distancia que o ataque da espada pode alcançar
	public float swordRange = 1.5f;

	//Objeto que armazenara as informaçoes sobre o objeto atingido pelo ataque de espada
	RaycastHit swordCollided;

	//Variavel que armazenara o ID da camada correspondente aos objetos atingiveis
	int collidableMask;
	
	//Objeto que define a linha que sai do ponto inicial da espada infinitamente para frente, a partir dessa linha e da distancia definida no swordRange e encontrado um objeto que sera armazenado no swordCollided
	Ray swordRay;

	//Funçao executada logo apos a inicializacao de todos objetos
	void Awake() {

		//Carrega na variavel playerAnimator o componente Animator do personagem
		playerAnimator = GetComponent<Animator>();

		//Busca no objeto swordAtPoint a referencia AudioSource (GetComponent<AudioSource>), relacionando ao objeto
		swordAudioSource = swordAtPoint.GetComponent<AudioSource>();

		//Relacionando a variavel a ID relacionada aos objetos que podem sofrer colisao
		collidableMask = LayerMask.GetMask ("Collidable");
	}

	//Funcao executada em uma taxa fixa em cada frame
	void FixedUpdate() {

		//Acrescenta a quantidade de tempo que passou entre um frame e outro a variavel
		swordStopWatch += Time.deltaTime;

		//Relaciona ao objeto swordButton o botao relacionado ao ataque de espada definido pela ID 0, configurado no objeto de controles mobile
		TouchZone swordButton = playerButtons.GetZone(0);

		//Verifica se o botao de ataque de espada (swordButton) foi pressionado (.Unipressed()) e se o cronometro do lançamento de feitiços (swordStopWatch) e menor do que a quantidade minima para o lançamento (>=swordTime)
		//nao permitindo que multiplos ataques de espada sejam lançados de uma vez
		if (swordButton.UniPressed () && swordStopWatch >= swordTime) {

			//Chama o metodo SwordIt para realizar o ataque de espada
			SwordtIt();
		}
	}

	//Metodo SwordIt que realiza as funçoes relacionadas ao ataque de espada, chamado quando o botao puder ser pressionado
	void SwordtIt(){

		//define o valor do cronometro do ataque de espada igual a 0, fazendo com que o jogador nao possa lançar um ataque de espada enquanto nao ultrapassar o tempo necessario
		swordStopWatch = 0f;
		
		//Toca o clipe relacionado ao lançamento do feitiço
		swordAudioSource.Play();
		
		//Repassa o valor "SwordAttack" ao controlador de animaçoes para ativar a animaçao relacionada ao feitiço
		playerAnimator.SetTrigger("SwordAttack");

		//Define a posiçao de partida da linha da espada a partir do ponto especificado pela posiçao do objeto swordAtPoint
		swordRay.origin = swordAtPoint.gameObject.transform.position;

		//Define a direçao da linha da espada especificada pelo objeto swordAtPoint
		swordRay.direction = swordAtPoint.gameObject.transform.forward;

		// Perform the raycast against gameobjects on the shootable layer and if it hits something...
		//Define o raycast (linha invisivel infinita), informando o ponto inicial e a direçao do raycast (swordRay), o objeto que recebera a colisao que precisa receber a keyword out para aceitar associaçao em seu valor,
		//a distancia maxima que este raycast pode alcançar (swordRange), e a mascara que ele trabalhara (collidableMask), fazendo com que acerte somente objetos presentes na camada especificada
		if (Physics.Raycast (swordRay, out swordCollided, swordRange, collidableMask)) {

			//Relaciona o componente EnemyHealthController a variavel enemyHealth
			EnemyHealthController enemyHealthController = swordCollided.transform.GetComponent<EnemyHealthController>();
			TakeDamageTrigger objectAdvanceTrigger = swordCollided.transform.GetComponent<TakeDamageTrigger>();
			//Verifica se o objeto colidido possui o script necessario para retirar quantidade de vida
			if (enemyHealthController != null) {//Se sim

					//Envia a quantidade de vida a ser retirada do personagem
					enemyHealthController.TakeDamage(swordDamage, 2);
			} else if( objectAdvanceTrigger != null)
				objectAdvanceTrigger.VerifyAndAdvance("Sword");
		}
	}
}