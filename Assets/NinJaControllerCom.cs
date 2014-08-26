using UnityEngine;
using System.Collections;

public class NinJaControllerCom : NinJaController {
	

	// Update is called once per frame
	void FixedUpdate () {
		if (isDead) {
			return;		
		}
		//Debug.Log (state.ToString());
		//my code begins here
		switch (state) {
		case State.Find:{
			onFindHandler();
		}	
			break;
		case State.Fight:{
			onFightHandler();
		}
			break;
		case State.Def:{
			
		}
			break;
		case State.Escape:{
			
		}
			break;
		case State.Dead:{
			onDeadHandler();
		}
			break;
		}
	}
	void OnCollisionEnter2D(Collision2D coll){
		coll.gameObject.rigidbody2D.velocity = new Vector2 (0, 0);
	}
	void OnCollisionStay2D(Collision2D coll){
		//Debug.Log("col");
		NinJaController colliderContoller = coll.gameObject.GetComponent(typeof(NinJaController)) as NinJaController;
		if (isEnemy (colliderContoller)) {
			targetEnemy = colliderContoller;
			//onFight();
			state = State.Fight;
		}
	}
	void OnCollisionExit2D(Collision2D coll){
		//Debug.Log("exit");
		state = State.Find;
	}
}
