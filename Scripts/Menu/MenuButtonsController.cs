using UnityEngine;
using System.Collections;

public class MenuButtonsController : MonoBehaviour {
	
	// Reinicia a cena atual, chamada pelo botao de reiniciar a fase no pause do jogo
	public void ReLoadScene() {
		Application.LoadLevel(Application.loadedLevel);
	}

	// Funcao publica que encerra a aplicaçao, chamada pelo botao do menu principal
	public void QuitApplication() {
		Application.Quit();
	}

	// Carrega a cena de acordo com o indice (id) especificado, chamado nos demais botoes da aplicacao
	public void LoadScene(int id) {
		Application.LoadLevel (id);
	}
}
