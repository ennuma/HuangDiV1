using UnityEngine;
using System.Collections;

public class NinJaControllerCom : NinJaController {

	void Start () {
		gameObject.tag = "entity";
		
		animator = GetComponent<Animator>();
		state = State.Find;
		if (transform.localScale.x > 0) {
			isFacingRight = false;		
		} else {
			isFacingRight = true;		
		}
		_attackCoolDown = attackCoolDown;
		isDead = false;
		health = maxhealth;
		GameObject healthbar = Instantiate (Resources.Load (healthBarResource)) as GameObject;
		healthbar.transform.parent = transform;
		
		GameObject manabar = Instantiate (Resources.Load (manaBarResource)) as GameObject;
		manabar.transform.parent = transform;
		
		//magic
		GameObject magic1 = ctrl.initMagic ("prefab/xuanyuanjianzhen");
		MagicController con = magic1.GetComponent<MagicController> () as MagicController;
		//magic1.transform.parent = transform;
		con.setParentController (this,ctrl);
		magicList.Add (con);

	}
	// Update is called once per frame
	void FixedUpdate () {
		if (isPause) {
			return;		
		}
		if (isDead) {
			return;		
		}
		currentMana = manaSpeed + currentMana;
		updateManaBar ();
		if (currentMana > manaRequiredToCast) {
			canCast = true;
			currentMana = manaRequiredToCast;			
		}
		//Debug.Log (currentMana);
		//Debug.Log (state.ToString());
		//update buff
		foreach (Buff buff in buffList) {
			buff.elapse(Time.deltaTime*buffTimeElapseMultiplier);	
		}
		ctrl.updateBufferList ();
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
		case State.Cast:{
			onCastHandler();
		}
			break;
		case State.Dead:{
			onDeadHandler();
		}
			break;
		}
	}

}
