using UnityEngine;
using System.Collections;

public class BossCastController : MonoBehaviour {
	

	//Objeto controlador de animaçoes, neste caso controlador das animaçoes do personagem
	Animator EnemyAnimator;

	//Objeto de jogo relacionado ao objeto do feitiço
	public GameObject castObject;

	//Objeto relacionado a posiçao e a fonte de audio do feitiço
	public GameObject castAtPoint;

	//variavel que especifica o tempo minimo de lançamento entre os feitiços
	public float castTime = 0.2f;

	//Variavel que controla quando sera possivel lançar o feitiço de acordo com o tempo definido em castTime
	float castStopWatch;

	int enemyscount;

	int spelledTimes;

	//Objeto relacionado ao componente de som do feitiço, diferente do PlayerHealth Controller esta fonte de audio ja sera populada pois so exite um som a tocar
	AudioSource castAudioSource;

	EnemyMovementController enemyMovementController;

	//Funçao executada logo apos a inicializacao de todos objetos
	void Awake() {

		//Busca no objeto castAtPoint a referencia AudioSource (GetComponent<AudioSource>), relacionando ao objeto
		castAudioSource = castAtPoint.GetComponent<AudioSource>();

		//Carrega na variavel playerAnimator o componente Animator do personagem
		EnemyAnimator = GetComponent<Animator>();

		enemyMovementController = gameObject.GetComponentInChildren<EnemyMovementController> ();

		spelledTimes = 1;
	}
	
	//Funcao executada em uma taxa fixa em cada frame
	void FixedUpdate() {

		//Acrescenta a quantidade de tempo que passou entre um frame e outro a variavel
		castStopWatch += Time.deltaTime;

		//Verifica se o botao de feitiço (castButton) foi pressionado (.Unipressed()) e se o cronometro do lançamento de feitiços (castStopWatch) e menor do que a quantidade minima para o lançamento (>=casTime)
		//nao permitindo que multiplos feitiços sejam lançados de uma vez
		if (enemyscount < 20) {
			Debug.Log (spelledTimes);
			if (castStopWatch >= castTime && spelledTimes < 4) {
				//Inicia uma corotina do lançamento do feitiço
				Debug.Log (spelledTimes);
				StartCoroutine (CastIt ());
			}
		} else {
			spelledTimes +=1;
			}

			if (spelledTimes == 4) {
					Debug.Log(spelledTimes);
				StartCoroutine (RestIt ());
		}
	}

	//Metodo IEnumerator, utilizado para percorrer funçoes com retorno yield, executando as açoes necessarias para atirar
	IEnumerator CastIt() {
		spelledTimes += 1;
		//define o valor do cronometro do feitiço igual a 0, fazendo com que o jogador nao possa lançar o feitiço enquanto nao ultrapassar o tempo minimo
		castStopWatch = 0f;

		//Toca o clipe relacionado ao lançamento do feitiço
		castAudioSource.Play();

		//Repassa o valor "CastAttack" ao controlador de animaçoes para ativar a animaçao relacionada ao feitiço
		EnemyAnimator.SetTrigger("CastAttack");

		//Executa uma funçao especial do System.Collections que permite a continuaçao do script do metodo apos a passagem de tempo enviada
		//utilizada para aguardar o final da animaçao de feitiço
		yield return new WaitForSeconds(0.29f);

		EnemyWakeController enemyWake;
		//Instancia um objeto do feitiço (castObject), com a posiçao (castAtPoint.position) e rotaçao (castAtPoint.rotation) especificadas no Objeto 
		GameObject newEnemy;
		newEnemy = Instantiate(castObject, castAtPoint.transform.position, castAtPoint.transform.rotation) as GameObject;
		enemyWake = newEnemy.gameObject.GetComponent<EnemyWakeController> ();
		enemyWake.WakeEnemy ();
		yield return new WaitForSeconds(0.9f);
		newEnemy = Instantiate(castObject, castAtPoint.transform.position, castAtPoint.transform.rotation) as GameObject;
		enemyWake = newEnemy.gameObject.GetComponent<EnemyWakeController> ();
		enemyWake.WakeEnemy ();
		enemyscount += 2;
	}
	IEnumerator RestIt() {
		enemyMovementController.enemyWakedStatus = false;
		EnemyAnimator.SetTrigger("Resting");
		yield return new WaitForSeconds(20f);
		enemyMovementController.enemyWakedStatus = true;
		spelledTimes = 1;
	}
}
