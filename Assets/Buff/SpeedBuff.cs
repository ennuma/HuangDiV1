using UnityEngine;
using System.Collections;

public class SpeedBuff : Buff
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
		float currentV = parentController.getVelocity ();
		float resultV = currentV + affectValue;
		parentController.setVelocity (resultV);
	}

	protected override void End(){
		float currentV = parentController.getVelocity ();
		float resultV = currentV - affectValue;
		parentController.setVelocity (resultV);
		Debug.Log (resultV);
	}
	
}

