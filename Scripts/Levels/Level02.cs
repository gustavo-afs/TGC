using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Level02 : MonoBehaviour {

	//Variaveis padrao
	int currentPart;
	bool partChanged;
	public GameObject playerObject;
	PlayerHealthController healthController;
	public Button consoleButton;
	public Text consoleText;
	public GameObject winPanel;
	public GameObject losePanel;

	//Variaveis que alteram de acordo com a fase
	public GameObject[] enemys;
	//public GameObject Enemy;

	void Awake() {
		winPanel.SetActive (false);
		losePanel.SetActive (false);
		currentPart = 1;
		partChanged = true;
		healthController = playerObject.gameObject.GetComponent<PlayerHealthController> ();

		//de acordo com a fase

	}

	void FixedUpdate () {

		if (!healthController.PlayerLifeStatus ()) {
			currentPart = 0;
			partChanged = true;
		}

		//Debug.Log ("Current Part: " + currentPart + " | partChanged: " + partChanged);

		if (partChanged) {
			partChanged = false;
			switch (currentPart) {
			case 0:
				Lose();
				break;
			case 1:
				consoleButton.interactable = true;
				consoleText.text = ("Essa e a sua primeira missao. Cuidado com os Goblins, sao criaturas rapidas e voce deve derrotar todos! [Clique Aqui]");
				break;
			case 2:
				consoleButton.interactable = false;
				consoleText.text = ("Derrote todos os goblins!");
				break;
			case 3:
				Win ();
				break;
			}
		}

		if (currentPart == 2) {
			bool enemysStatus = false;
			foreach (GameObject enemy in enemys) {
				EnemyHealthController enemyHealth;
				enemyHealth = enemy.gameObject.GetComponentInChildren<EnemyHealthController>();
				if (enemyHealth.EnemyLifeStatus() && !enemysStatus) {
					//Debug.Log("Enemy: " + enemy + " | enemyHealth.Status: " + enemyHealth.EnemyLifeStatus() + " | enemysStatus: " + enemysStatus);
					enemysStatus = true;
				} 
			}
			//Debug.Log("EnemysStatus: " + enemysStatus);

			if (!enemysStatus) {
				AdvancePart();
			}
		}

	}

	public void AdvancePart() {
		currentPart +=1;
		partChanged = true;

	}

	void Win() {
		int actualLevel = 1;
		if (actualLevel >= PlayerPrefs.GetInt ("unlockedLevel")) {
			PlayerPrefs.SetInt("unlockedLevel",actualLevel + 1);
		}
		//Diminui a escala de tempo do jogo a zero, fazendo com que nenhuma açao possa acontecer.
		Time.timeScale = 0;

		winPanel.SetActive (true);
	}

	void Lose() {
		//Diminui a escala de tempo do jogo a zero, fazendo com que nenhuma açao possa acontecer.
		Time.timeScale = 0;

		losePanel.SetActive (true);
	}
}

/*	

}
*/