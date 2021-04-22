using UnityEngine;
using System.Collections;

public class PauseMenuController : MonoBehaviour {

	//Objeto que define o objeto de controles mobile
	public TouchController menuButtons;

	//Objeto relacionado ao painel de menu que sera ativado quando pressionado o botao pause
	public GameObject menuContainer;

	//Funçao executada apos o Wake de todos os objetos
	void Start () {
		Time.timeScale = 1;
		//Define o Menu de Pause como inativo
		menuContainer.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		//Relaciona ao objeto pauseButton o botao relacionado ao menu de pause definido pela ID 3, configurado no objeto de controles mobile
		TouchZone	pauseButton	= menuButtons.GetZone(3);

		//Verifica se o botao de pause (pauseButton) foi pressionado (.Unipressed())
		if (pauseButton.UniPressed ()) {

			//Define o painel de menu como ativo
			menuContainer.SetActive(true);

			//Diminui a escala de tempo do jogo a zero, fazendo com que nenhuma açao possa acontecer.
			Time.timeScale = 0;
		}
	}

	//Botao pressionado no menu para voltar ao jogo
	public void Unpause() {

		//Define a escala de jogo igual a 1, fazendo com que as açoes tenham o seu tempo normal de execuçao
		Time.timeScale = 1;

		//Define o painel de menu como inativo
		menuContainer.SetActive(false);
	}
}
