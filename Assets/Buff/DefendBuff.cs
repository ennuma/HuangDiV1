using UnityEngine;
using System.Collections;

public class DefendBuff : Buff
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
		float currentV = parentController.getDefence ();
		float resultV = currentV + affectValue;
		parentController.setDefence (resultV);
	}

	protected override void End(){
		float currentV = parentController.getDefence ();
		float resultV = currentV - affectValue;
		parentController.setDefence (resultV);
		//Debug.Log (resultV);
	}
	
}

