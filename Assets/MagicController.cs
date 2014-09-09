using UnityEngine;
using System.Collections;

public class MagicController : MonoBehaviour
{
	protected bool isPause = false;
	public NinJaController parentController;
	public float attack = 1;
	protected GameController gameCtrl;
	protected float timeElapsed; 
	// Use this for initialization
	private Animator animator;
	protected GameController.Side side;
	public virtual void setParentController(NinJaController p, GameController gCtrl)
	{
		//Debug.Log("there");
		parentController = p;
		gameCtrl = gCtrl;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (isPause) {
			return;		
		}
		timeElapsed += Time.deltaTime;
		if (timeElapsed > 2) {
			gameObject.SetActive(false);
		}
		if (gameCtrl.checkMagicHit (this, parentController.side)) {
			gameObject.SetActive(false);		
		}
		if (side == GameController.Side.rightSide) {
			transform.position = transform.position + new Vector3 (0.2f, 0f, 0f);
		} else {
			transform.position = transform.position + new Vector3 (-0.2f, 0f, 0f);

		}
	}

	public virtual void play(GameController.Side s)
	{
		//initPosition ();
		gameObject.tag = "magic";
		animator = GetComponent<Animator> ();
		timeElapsed = 0.0f;
		renderer.sortingOrder = 10;
		gameObject.SetActive (true);
		//Debug.Log (animator);
		animator.SetBool ("play", true);
		side = s;
		if (s == GameController.Side.rightSide) {
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
		}else{
			transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);

		}
	}
	public void initPosition(Vector3 pos){
		transform.position = pos;
		//init position here
	}
	public virtual void setPause(bool pause){
		isPause = pause;
		if (!animator) {
			return;
		}
		if (pause) 
		{
			//Debug.Log("p");
			animator.speed = 0.0f;
		}else{
			animator.speed = 1;
		}
	}
}

