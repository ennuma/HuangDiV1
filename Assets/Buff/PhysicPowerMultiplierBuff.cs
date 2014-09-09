using UnityEngine;
using System.Collections;

public class PhysicPowerMultiplierBuff : Buff
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
		float currentV = parentController.getPP ();
		float resultV = currentV + affectValue;
		parentController.setPP (resultV);
	}

	protected override void End(){
		float currentV = parentController.getPP ();
		float resultV = currentV - affectValue;
		parentController.setPP (resultV);
		//Debug.Log (resultV);
	}
	
}

