using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameController : MonoBehaviour {
	public enum Side{
		leftSide,
		rightSide
	}
	public List<NinJaController> leftEntities;
	public List<NinJaController> rightEntities;
	List<GameObject> list = new List<GameObject> ();
	// Use this for initialization
	void Start () {
		//List<GameObject> list = new List<GameObject> ();
		foreach(NinJaController temp in leftEntities){
			temp.side = Side.leftSide;
			list.Add(temp.gameObject);
		}
		foreach(NinJaController temp in rightEntities){
			temp.side = Side.rightSide;
			list.Add(temp.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		list.Sort (delegate(GameObject x, GameObject y) {
			NinJaController _x = x.GetComponent(typeof (NinJaController)) as NinJaController;
			NinJaController _y = y.GetComponent(typeof (NinJaController)) as NinJaController;
			if(_x.isDead){
				return -1;
			}
			if(_y.isDead){
				return 1;
			}
						return -(_x.transform.position.y.CompareTo (_y.transform.position.y));
				});
		for (int i = 0; i < list.Count; i++) {
			GameObject go = list[i];
			go.renderer.sortingOrder = i;
			//Debug.Log (((NinJaController)go.GetComponent(typeof(NinJaController))).side.ToString());
		}
		//Debug.Log("==");

	}

	public NinJaController getTargetEnemyForEntity(NinJaController me){
		NinJaController ret = null;
		float minDis = 9999;

		List<NinJaController> enemySide;
		if (me.side == Side.leftSide) {			
			enemySide = rightEntities;		
		} else {
			enemySide = leftEntities;		
		}
		foreach (NinJaController temp in enemySide) {
			float currentDis = Vector3.Distance(me.transform.position, temp.transform.position);
			if(currentDis<minDis){
				minDis = currentDis;
				ret = temp;
			}
		}

		return ret;
	}
	public void attackEnemyController(NinJaController from, NinJaController to)
	{
		to.takeDamageFromEnemy (from);
	}
	public void entityDead(NinJaController entity){
		//Debug.Log("dead");
		int a = leftEntities.IndexOf (entity);
		if (a != -1) {
			Debug.Log("left");
			leftEntities.RemoveAt(a);		
		}
		int b = rightEntities.IndexOf (entity);
		if (b != -1) {
			//Debug.Log("right");
			rightEntities.RemoveAt(b);		
		}
	}
}
