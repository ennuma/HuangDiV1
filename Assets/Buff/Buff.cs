using UnityEngine;
using System.Collections;

public class Buff:MonoBehaviour
{
	protected bool isPause = false;
	public NinJaController parentController;
	protected GameController gameCtrl;
	protected GameController.Side side;
	public float duration;

	public void attachTo(NinJaController entity, GameController ctrl)
	{
		parentController = entity;
		gameCtrl = ctrl;
		entity.addBuff (this);
		Begin ();
	}
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{

	}

	public void elapse(float decBuffTime)
	{
		//Debug.Log (duration);
		duration -= decBuffTime;
		if (duration <= 0) {
			gameCtrl.dettachBuffFromEntityController(this,parentController);	
		}
	}

	public void detach()
	{
		End ();
		parentController.deleteBuff (this);
	}
	protected virtual void Begin()
	{

	}

	protected virtual void End()
	{

	}
}

