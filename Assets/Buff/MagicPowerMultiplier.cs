using UnityEngine;
using System.Collections;

public class MagicPowerMultiplier : Buff
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
		float currentV = parentController.getMP ();
		float resultV = currentV + affectValue;
		parentController.setMP (resultV);
	}

	protected override void End(){
		float currentV = parentController.getMP ();
		float resultV = currentV - affectValue;
		parentController.setMP (resultV);
		//Debug.Log (resultV);
	}
	
}

