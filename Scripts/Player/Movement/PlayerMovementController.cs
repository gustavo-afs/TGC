using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {

	//Objeto que define o objeto de controles mobile
	public TouchController playerButtons;
	
	//Objeto controlador de animaçoes, neste caso controlador das animaçoes do personagem
	Animator playerAnimator;

	//Objeto controlador do corpo rigido do personagem, ou seja, controladora de funcoes relacionadas a colisao e gravidade
	Rigidbody playerRigidbody;

	//Variavel que controla a velocidade do personagem em uma equaçao que envolve a velocidade e a direçao da movimentaçao
	public float movimentSpeed = 4.5f;

	//Funçao executada logo apos a a inicializacao de todos objetos
	void Awake() {

		//Carrega na variavel playerAnimator o componente Animator do personagem
		playerAnimator = GetComponent<Animator>();

		//Carrega na variavel playerRigidbody o componente Rigidbody do personagem
		playerRigidbody = GetComponent<Rigidbody>();
	}

	//Funcao executada em uma taxa fixa em cada frame, normalmente utilizada quando utilizamos Rigidbody
	void FixedUpdate() {

		//Relaciona ao objeto walkStick o controle analogico de movimentaçao definido pela ID 0, configurado no objeto de controles mobile
		TouchStick walkStick = playerButtons.GetStick(0);

		//Variavel float com a quantidade de unidades em centimetros, movidas pelo controle analogico de movimentao na horizontal (para os lados)
		float horizontalStick = walkStick.GetVec().x;
		
		//Variavel float com a quantidade de unidades em centimetros, movidas pelo controle analogico de movimentao na vertical (para frente e para tras)
		float verticalStick = walkStick.GetVec().y;

		//Debug.Log ("horizontalValue: " + horizontalValue + " | verticalValue: " + verticalValue); //Debug para analisar os valores recebidos nesta funcao

		//Verifica se o analogico de movimentaçao foi pressionado
		if (walkStick.Pressed ()) {//Se sim

			//Variavel que recebe o valor do angulo do analogico de movimentaçao para que o personagem gire na mesma direçao
			float walkAngle	= walkStick.GetAngle();

			//Chamada da funçao que realizara a movimentaçao do personagem, repassando a ela os valores de entrada recebidos do analogico
			PlayerMove(horizontalStick, verticalStick);

			//Chamada da funçao que realiza a rotaçao do personagem informando o angulo de entrada no analogico de movimentaçao
			PlayerTurn(walkAngle);
		}

		//Chamada da funçao que realiza a animaçao do personagem se o mesmo estiver andando ou nao
		PlayerAnimate(horizontalStick,verticalStick);
	}

	//Funçao que recebe as posiçoes verticais e horizontais do analogico para realizar a movimentaçao do personagem
	void PlayerMove(float horizontalValue, float verticalValue) {

		//Vetor com os valores a horizontais(x) e verticais(z) de distancia de movimentacao. O segundo valor (y), definido como 0f para que o personagem nao se mova para cima nem para baixo,
		//Esses valores sao arrendodados (.normalized) e multiplicados pela velocidade (movimentSpeed) e por uma taxa fixa de tempo de frame a frame  (Time.deltaTime)
		Vector3 movement = new Vector3(horizontalValue, 0f, verticalValue).normalized * movimentSpeed * 1 * Time.deltaTime;

		//Movimentaçao do Corpo Rigido do personagem utilizando a sua posiçao atual(transform.position) somada a distancia a ser movimentada (moviment) 
		playerRigidbody.MovePosition(transform.position + movement);
	}

	//Funçao que recebe os valores de movimentaçao horizontal e vertical para verificar se o personagem deve ser animado
	void PlayerAnimate(float horizontalValue, float verticalValue) {

		//Variavel booleana que recebe sim caso o a movimentaçao horizontal seja diferente de 0f ou se a movimentaçao vertical seja diferente de 0f
		bool Walking =  horizontalValue != 0f || verticalValue != 0f;

		//Debug.Log ("Player is Walking?: " + walking); //Debug que verifica se o personagem esta ou nao recebendo a entrada para se mover

		//Envia ao componente de animaçao se o valor de movimentaçao esta verdadeiro ou falso, ou seja, se o personagem esta se movendo ou nao
		playerAnimator.SetBool ("IsWalking", Walking);
	}

	//Funçao que recebe o valor e converte em rotaçao (y) do personagem.
	void PlayerTurn(float angleValue) {
		playerRigidbody.transform.rotation = Quaternion.Lerp (playerRigidbody.transform.rotation, Quaternion.Euler (0, angleValue, 0), 5 * Time.deltaTime);
	}	
}
