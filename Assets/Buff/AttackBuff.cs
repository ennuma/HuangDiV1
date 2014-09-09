using UnityEngine;
using System.Collections;

public class AttackBuff : Buff
{
	
	public float affectValue;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

	protected override void Begin(){
		float currentV = parentController.getAttack ();
		float resultV = currentV + affectValue;
		parentController.setAttack (resultV);
	}

	protected override void End(){
		float currentV = parentController.getAttack ();
		float resultV = currentV - affectValue;
		parentController.setAttack (resultV);
		//Debug.Log (resultV);
	}
	
}

