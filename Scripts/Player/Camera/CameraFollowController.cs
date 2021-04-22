using UnityEngine;
using System.Collections;

public class CameraFollowController : MonoBehaviour {

	//Objeto que define o objeto de controles mobile
	public TouchController playerButtons;

	//Variavel que define a velocidade de rotaçao da camera
	public float turnSpeed = 10f;

	//Objeto que define o ponto que a camera devera seguir
	public Transform lookAtPoint;

	//Variavel que define a altura (Y) padrao da camera em relaçao ao objeto que devera seguir
	public float offsetCameraY = 1.5f;

	//Variavel que define a distancia (Z) da camera em relaçao ao objeto que devera seguir
	public float offsetCameraZ = 8f;

	//Vetor com a definiçao padrao da posicao da camera em relaçao ao ponto de visualizaçao
	private Vector3 offsetCameraPosition;

	//Funçao executada quando o objeto pai deste script foi habilitado
	void Start () {

		//Relacionando os valores definidos de Y e Z a um vetor que sera utilizado no calculo da distancia da camera em relacao ao ponto de visualizaçao
		offsetCameraPosition = new Vector3(0, offsetCameraY, -offsetCameraZ);
	}
	
	//Funcao executada em uma taxa fixa em cada frame, normalmente utilizada quando utilizamos Rigidbody
	void FixedUpdate() {


		//Altera a posiçao atual da camera de forma suave (Vector3.Lerp), informando o ponto inicial, ou seja a posiçao atual da camera (transform.position) e a posiçao final apos a movimentaçao,
		//ou seja, a posiçao do ponto de visualizaçao (lookAtPoint.position) somada a posiçao da distancia da camera em relaçao a esse ponto (offsetCameraPosition)
		transform.position = Vector3.Lerp (transform.position, lookAtPoint.position + offsetCameraPosition,100);
		//Altera os valores de rotaçao da camera necessarios (transform.LookAt), para que a camera continue apontada para o ponto de visualizaçao (LookAt.position)
		transform.LookAt(lookAtPoint.position);
	}
}