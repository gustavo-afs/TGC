using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LevelS : MonoBehaviour {

	//Variaveis padrao
	public GameObject playerObject;
	PlayerHealthController healthController;
	public Button consoleButton;
	public Text consoleText;
	public GameObject losePanel;
	public GameObject lifePotion;

	//Variaveis que alteram de acordo com a fase
	public Transform[] spawnPoints;
	public GameObject[] enemys;
	float roundTime;
	int survivalRecord;
	int enemysCount;
	int round;
	bool spawnReady;
	const float minRoundTime = 15f;
	int enemysInGame;
	int enemyQnty;

	//Variavel que controla o cronometro
	float stopWatch;

	void Awake() {
		enemysInGame = 0;
		consoleButton.interactable = false;
		losePanel.SetActive (false);
		healthController = playerObject.gameObject.GetComponent<PlayerHealthController> ();
		if (PlayerPrefs.HasKey ("survivalRecord")) {
			survivalRecord = PlayerPrefs.GetInt ("survivalRecord");
		} else {
			survivalRecord = 0;
		}
		//de acordo com a fase
		roundTime = 60f;
		enemysCount = 0;
		round = 1;
		consoleText.text = "Derrote quantos inimigos conseguir!\nMaior Recorde: " + survivalRecord + "\nInimigos mortos nesta missao: " + enemysCount;
		spawnReady = true;
	}

	void FixedUpdate () {
		stopWatch += Time.deltaTime;
		if (!healthController.PlayerLifeStatus ()) {
			Lose ();
		}

		if (spawnReady) {
			enemyQnty = 1 + (round * 2);
			if (20 < (enemyQnty + enemysInGame)) {
				enemyQnty = 20 - enemysInGame; 
			}
			StartCoroutine (SpawnEnemy (enemyQnty));
			Vector3 lifePotionPosition = new Vector3(Random.Range (32,66),0,Random.Range (28,64));
			Instantiate(lifePotion, lifePotionPosition, lifePotion.transform.rotation);
		}
		if (roundTime < stopWatch) {
			spawnReady = true;
		}
	}

	public void CountAEnemy() {
		enemysCount +=1;
		enemysInGame -= 1;
		consoleText.text = ("Derrote quantos inimigos conseguir!\nMaior Recorde: " + survivalRecord + "\nInimigos mortos nesta missao: " + enemysCount);
	}

	IEnumerator SpawnEnemy(int enemyQnty) {
		spawnReady = false;
		stopWatch = 0;
			for (int i= 1; i <= enemyQnty; i++) {
				GameObject spawnEnemy = enemys[Random.Range(0,enemys.Length-1)];
				Transform spawnPoint = spawnPoints [Random.Range (0, spawnPoints.Length - 1)];
				GameObject newEnemy = Instantiate(spawnEnemy, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
				EnemyWakeController enemyWake = newEnemy.gameObject.GetComponent<EnemyWakeController> ();
				enemyWake.WakeEnemy ();
				yield return new WaitForSeconds(1.3f);
			}
				enemysInGame += enemyQnty;

				if (enemyQnty > 0)
				round +=1;

			if (roundTime != minRoundTime) {
				float nextRoundTime = roundTime - round/2;
				if (nextRoundTime < minRoundTime) {
					roundTime = minRoundTime;
				} else
					roundTime = nextRoundTime;
		}
	}

	void Lose() {

		if (enemysCount > survivalRecord)
			PlayerPrefs.SetInt ("survivalRecord", enemysCount);
		gameObject.SetActive (false);
		//Diminui a escala de tempo do jogo a zero, fazendo com que nenhuma açao possa acontecer.
		Time.timeScale = 0;

		losePanel.SetActive (true);
	}
}

/*	

}
*/