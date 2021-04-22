using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Level01 : MonoBehaviour {

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
	public GameObject localTrigger;
	public GameObject barrelCollider;
	public GameObject finalEnemy;
	EnemyWakeController awakeEnemy;
	EnemyHealthController enemyHealth;
	//public GameObject Enemy;

	void Awake() {
		consoleButton.interactable = false;
		barrelCollider.SetActive (false);
		winPanel.SetActive (false);
		losePanel.SetActive (false);
		//objeto_menu_derrota.SetActive(false);
		currentPart = 1;
		partChanged = true;


		//de acordo com a fase
		localTrigger.SetActive (false);
		healthController = playerObject.gameObject.GetComponent<PlayerHealthController> ();
		awakeEnemy = finalEnemy.gameObject.GetComponent<EnemyWakeController> ();
		enemyHealth = finalEnemy.gameObject.GetComponentInChildren<EnemyHealthController>();
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
				consoleText.text = ("Bem vindo ao treinamento guerreiro, ensinaremos você a se tornar um verdadeiro herói! [Clique Aqui]");
				break;
			case 2:
				consoleButton.interactable = true;
				consoleText.text = ("Não percamos tempo mova-se para o local marcado! [Clique Aqui]");
				break;
			case 3:
				consoleButton.interactable = false;
				consoleText.text = ("Mova-se ao local indicado no cenario. (Utilize o analógico para mover-se)");
				localTrigger.SetActive (true);
				break;
			case 4:
				localTrigger.SetActive (false);
				consoleText.text = ("Muito bem! Agora utilize o seu poder e lance uma bola de energia no barril! (Utilize o botão para lançar a bola de energia)");
				barrelCollider.SetActive (true);
				barrelCollider.GetComponent<TakeDamageTrigger> ().SetDamageType ("Cast");
				break;
			case 5:
				consoleText.text = ("Perfeito! Agora teste a sua espada no barril! (Utilize o botão para atacar com a espada)");
				barrelCollider.GetComponent<TakeDamageTrigger> ().SetDamageType ("Sword");
				break;
			case 6:
				barrelCollider.SetActive (false);
				consoleText.text = ("Vamos ver se esta preparado para atacar um inimigo real! [Clique para continuar]");
				consoleButton.interactable = true;
				break;
			case 7:
				consoleButton.interactable = false;
				AwakefinalEnemy ();
				consoleText.text = ("Derrote o inimigo!");
				break;
			case 8:
				StartCoroutine (Win ());
				break;
			}
		}

		if (currentPart == 7) {
			if (!enemyHealth.EnemyLifeStatus()) {
				AdvancePart();
			}
		}
	}

	public void AdvancePart() {
		currentPart +=1;
		partChanged = true;

	}

	IEnumerator Win() {
		int actualLevel = 3;
		if (actualLevel >= PlayerPrefs.GetInt ("unlockedLevel")) {
			PlayerPrefs.SetInt("unlockedLevel",actualLevel + 1);
		}
		yield return new WaitForSeconds(1.3f);

		//Diminui a escala de tempo do jogo a zero, fazendo com que nenhuma açao possa acontecer.
		Time.timeScale = 0;

		winPanel.SetActive (true);
	}

	void Lose() {
		//Diminui a escala de tempo do jogo a zero, fazendo com que nenhuma açao possa acontecer.
		Time.timeScale = 0;

		losePanel.SetActive (true);
	}

	public void AwakefinalEnemy() {
		awakeEnemy.WakeEnemy ();
	}
}

/*	

}
*/