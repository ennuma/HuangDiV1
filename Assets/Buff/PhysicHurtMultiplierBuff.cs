using UnityEngine;
using System.Collections;

public class PhysicHurtMultiplierBuff : Buff
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
		float currentV = parentController.getPH ();
		float resultV = currentV + affectValue;
		parentController.setPH (resultV);
	}

	protected override void End(){
		float currentV = parentController.getPH ();
		float resultV = currentV - affectValue;
		parentController.setPH (resultV);
		//Debug.Log (resultV);
	}
	
}

