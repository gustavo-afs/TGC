using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuLevelsController : MonoBehaviour {

	public GameObject[] panels;

	//Array com a quantidade fases do jogo, controladas por botoes, associados atraves da cena 
	public Button[] levels;

	//Variaveis com os objetos dos botoes de proxima fase e fase anterior 
	public Button nextLevelButton;
	public Button previousLevelButton;
	public GameObject[] loadingScreen;
	public GameObject loadingBg;
	public GameObject loadingText;

	//Variavel que recebera o numero de fases concluidas no jogo
	int unlockedLevel;

	//Variavel que recebe o ID da fase visivel ao usuario
	int currentLevel;

	int currentPanel;

	//Funçao executada logo apos a a inicializacao de todos objetos
	void Awake() {
		Time.timeScale = 1;
		loadingText.SetActive(false);
		foreach (GameObject gmo in loadingScreen) {
			gmo.SetActive(false);
		}
		loadingBg.SetActive(false);
		panels[0].SetActive(true);
		panels[1].SetActive(false);
		panels[2].SetActive(false);
		PlayerPrefs.SetInt("currentPanel", 0);

		//Verifica se existe alguma entrada com o nome UnlockedLevel (Significa que a aplicaçao ja foi utilizada)
		//no PlayerPrefs (classe que armazena informaçoes entre cenas)
		if (PlayerPrefs.HasKey("unlockedLevel")){ //Se (existe) sim
		//Atribuiçao do valor da entrada unlockedLevel presente na classe PlayerPrefs na variavel unlockedLevel desta classe
		unlockedLevel = PlayerPrefs.GetInt ("unlockedLevel");
		}
		else{ //Se nao (existe)
			//A aplicaçao nunca foi utilizada e portanto gera uma entrada inicial para a aplicacao com a primeira fase disponivel
			PlayerPrefs.SetInt("unlockedLevel",0);
			//Atribuiçao da entrada unlockedLevel a variavel
			unlockedLevel = PlayerPrefs.GetInt ("unlockedLevel");
		}


		//Debug.Log ("(PlayerPrefs) unlockedLevel: " + unlockedLevel); //Debug para verificar a quantidade de fases desbloqueadas
	}
	
	//Funçao executada quando o objeto pai deste script foi habilitado
	void Start () {

		//Torna cada fase desabilitada para interaçao e visualizao, por segurança
		foreach (Button level in levels) {

			//desabilita a interaçao da fase
			level.interactable = false;

			//desabilita a visualizaçao da fase
			level.gameObject.SetActive(false);
		}

		//Enquanto o ID de fases for menor que o numero de fases desbloqueadas (unlockedLevel) a fase sera habilitada, ou seja, habilita a interaçao das fases disponiveis
		for (int levelID = 0; levelID <= unlockedLevel; levelID++) {

			//habilita a interacao da fase especificada no ID
			levels[levelID].interactable = true;
		}

		//Relaciona a ultima fase desbloqueada ao level atual
		currentLevel = unlockedLevel;

		//Chama a funçao carregara o botao do level atual (pois o indice de navegacao sera 0)
		NavigateLevel(0);
	}
	public void NavigatePanel(int i) {
		panels [currentPanel].SetActive (false);
		panels [i].SetActive (true);
		currentPanel = i;
	}

	//Funcao publica que e executa a navegacao de acordo com o level atual caso o valor seja 1, a funcao exibira o proximo level
	//caso seja -1 a funçao exibira o level anterior
	//caso seja 0 ela carregara o level atual
	//Ou seja o valor do indice sempre sera somado ao level atual. Ex: i = 2 | ID da fase atual + 2 (indice)
	public void NavigateLevel(int i) {

		//soma o valor do indice a fase atual na variavel newID, gerando, portanto, o novo ID da fase
		int newID = currentLevel + i;

		//Desabilita a fase ativo antes da navegacao, ou seja, antes da funcao ser chamada
		levels[currentLevel].gameObject.SetActive(false);

		//Habilita a fase solicitada na navegacao, a partir do novo ID gerada
		levels[newID].gameObject.SetActive(true);

		//Transforma a fase solicitada (ja habilitada), na fase atual
		currentLevel = newID;


		//Conjunto de codigos que analisara se os botoes de navegaçao, podem continuar disponiveis ou nao, apos a navegacao
		if (currentLevel == 0) { //Caso a fase atual seja 0, portanto a primeira fase

			//O botao de navegar para a fase anterior perde a interatividade
			previousLevelButton.interactable = false;

		} else previousLevelButton.interactable = true; //Caso nao seja 0, portanto nao sera a primeira fase, o botao de navegar para a fase anterior sera habilitado

		if (currentLevel == (levels.Length -1)) { // Caso a fase atual tenha o mesmo numero da ultima fase (quantidade de fases -1), portanto ultima fase

			//O botao de navegar para a proxima fase perde a interatividade
			nextLevelButton.interactable = false;

		} else nextLevelButton.interactable = true;//Caso nao tenha o mesmo numero da ultima fase, portanto nao sera a ultima fase, o botao de navegar para a proxima fase sera habilitado
	}
	public void LoadLevel(int id) {
		loadingBg.SetActive(true);
		loadingScreen [Random.Range (0, loadingScreen.Length - 1)].SetActive (true);
		loadingText.SetActive (true);
		Application.LoadLevel (id);
	}
}