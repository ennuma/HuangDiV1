using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Magic_XuanYuanJianZhen : MagicController
{
	List<MagicController> subMagics = new List<MagicController>();
		// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		//Debug.Log("fixedupdate");
		return;
	}
	public override void setParentController(NinJaController p, GameController gCtrl)
	{
		parentController = p;
		gameCtrl = gCtrl;

		GameObject m1 = Instantiate (Resources.Load ("prefab/testMagic")) as GameObject;
		GameObject m2 = Instantiate (Resources.Load ("prefab/testMagic")) as GameObject;
		GameObject m3 = Instantiate (Resources.Load ("prefab/testMagic")) as GameObject;
		GameObject m4 = Instantiate (Resources.Load ("prefab/testMagic")) as GameObject;
		m1.transform.parent = transform;
		m2.transform.parent = transform;
		m3.transform.parent = transform;
		m4.transform.parent = transform;
		//Debug.Log()
		MagicController mc1 = m1.GetComponent<MagicController> () as MagicController;
		MagicController mc2 = m2.GetComponent<MagicController> () as MagicController;
		MagicController mc3 = m3.GetComponent<MagicController> () as MagicController;
		MagicController mc4 = m4.GetComponent<MagicController> () as MagicController;
		subMagics.Add (mc1);
		subMagics.Add (mc2);
		subMagics.Add (mc3);
		subMagics.Add (mc4);
		mc1.gameObject.SetActive (false);
		mc2.gameObject.SetActive (false);
		mc3.gameObject.SetActive (false);
		mc4.gameObject.SetActive (false);
		foreach (MagicController mc in subMagics) {
			mc.setParentController(p,gCtrl);		
		}
	}

	public override void play(GameController.Side s)
	{
		gameObject.tag = "magic";
		timeElapsed = 0.0f;
		renderer.sortingOrder = 10;
		gameObject.SetActive (true);
		side = s;

		subMagics [0].initPosition (transform.position + (new Vector3 (-1, 0, 0)));
		subMagics [1].initPosition (transform.position + (new Vector3 (1, 0, 0)));
		subMagics [2].initPosition (transform.position + (new Vector3 (0, 1, 0)));
		subMagics [3].initPosition (transform.position + (new Vector3 (0, -1, 0)));

		foreach (MagicController mc in subMagics) {
			mc.play(s);	
		}
	}

	public override void setPause (bool pause)
	{
		isPause = pause;
		foreach (MagicController mc in subMagics) {
			mc.setPause(pause);	
		}
	}
}

