using UnityEngine;
using System.Collections;

public class LuckyBuff : Buff
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
		float currentV = parentController.getLucky ();
		float resultV = currentV + affectValue;
		parentController.setLucky (resultV);
	}

	protected override void End(){
		float currentV = parentController.getLucky ();
		float resultV = currentV - affectValue;
		parentController.setLucky (resultV);
		//Debug.Log (resultV);
	}
	
}

