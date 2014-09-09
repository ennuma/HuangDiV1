using UnityEngine;
using System.Collections;

public class MagicHurtMultiplier : Buff
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
		float currentV = parentController.getMH ();
		float resultV = currentV + affectValue;
		parentController.setMH (resultV);
	}

	protected override void End(){
		float currentV = parentController.getMH ();
		float resultV = currentV - affectValue;
		parentController.setMH (resultV);
		//Debug.Log (resultV);
	}
	
}

