using UnityEngine;
using System.Collections;

public class TestLvl : MonoBehaviour {

	// Carrega o level de acordo com o ID especificado
	public void LoadScene(int ID) {
		Application.LoadLevel (ID);
	}
	
	// Reinicia o level atual
	public void UnlockScene(int ID) {
		PlayerPrefs.SetInt("UnlockedLevel",PlayerPrefs.GetInt ("UnlockedLevel")+1);
		int actualLevel = 1;
		if (actualLevel >= PlayerPrefs.GetInt ("UnlockedLevel")) {
			PlayerPrefs.SetInt("UnlockedLevel",actualLevel + 1);
		}
		Application.LoadLevel (ID);
	}
	
	// Encerra a aplicaçao
	public void QuitApplication() {
		Application.Quit();
	}
}
