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
	public GameObject magicContainer;
	List<GameObject> list = new List<GameObject> ();
	protected List<MagicController> magicList = new List<MagicController>();
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
			if(go.renderer.sortingOrder<0)
			{
				continue;
			}
			go.renderer.sortingOrder = i;
		}

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
		Debug.Log("dead");
		int a = leftEntities.IndexOf (entity);
		if (a != -1) {
			leftEntities.RemoveAt(a);		
		}
		int b = rightEntities.IndexOf (entity);
		if (b != -1) {
			rightEntities.RemoveAt(b);		
		}
		entity.renderer.sortingOrder = -99;
	}

	public void castMagic(MagicController magic, float direction)
	{
		//Debug.Log (direction);
		if (direction < 0) {
			//right
			//magic.transform.position = new Vector3(magic.transform.position.x+magic.renderer.bounds.size.x,magic.transform.position
			//                                       .y,magic.transform.position.z);
			magic.initPosition(magic.parentController.transform.position);
			magic.play(Side.rightSide);
		} else {
			//magic.transform.localScale = new Vector3(-Mathf.Abs(magic.transform.localScale.x),magic.transform.localScale.y,transform.localScale.z);
			//magic.transform.position = new Vector3(magic.transform.position.x-magic.renderer.bounds.size.x,magic.transform.position
			//                                       .y,magic.transform.position.z);
			magic.initPosition(magic.parentController.transform.position);
			magic.play(Side.leftSide);
			//left
		}

	}

	public bool checkMagicHit(MagicController magic, Side side)
	{
		if (side == Side.leftSide) {
			foreach(NinJaController enemy in rightEntities){
				if(!enemy.gameObject.activeSelf){
					continue;
				}
				if(magic.renderer.bounds.Intersects(enemy.renderer.bounds)){
					enemy.takeDamageFromMagic(magic);
					return true;
				}
			}	
		}
		else{
			foreach(NinJaController enemy in leftEntities){
				if(magic.collider2D.bounds.Intersects(enemy.renderer.bounds)){
					enemy.takeDamageFromMagic(magic);
					return true;
				}
			}	
		}

		return false;
	}

	public GameObject initMagic(string resource){
		GameObject magic = Instantiate (Resources.Load (resource)) as GameObject;
		magic.transform.parent = magicContainer.transform;
		MagicController con = magic.GetComponent<MagicController> () as MagicController;
		magic.SetActive (false);
		magicList.Add (con);
		return magic;
	}

	public void pauseAllEntityAndMagic(){
		foreach (NinJaController entity in rightEntities) {
			entity.setPause(true);		
		}
		foreach (NinJaController entity in leftEntities) {
			entity.setPause(true);		
		}
		foreach (MagicController magic in magicList) {
			magic.setPause(true);
		}
	}
	public void unpauseAllEntityAndMagic(){
		//Debug.Log ("unpause");
		foreach (NinJaController entity in rightEntities) {
			entity.setPause(false);		
		}
		foreach (NinJaController entity in leftEntities) {
			entity.setPause(false);		
		}
		foreach (MagicController magic in magicList) {
			magic.setPause(false);
		}
	}

	public void pauseForSeconds(float seconds)
	{
		Debug.Log("pause for seconds");
		pauseAllEntityAndMagic ();
		Invoke ("unpauseAllEntityAndMagic", seconds);
	}

	public void attachBuffToEntityController(Buff buff, NinJaController entity)
	{
		buff.attachTo (entity, this);
	}

	private static List<Buff> buffDeleteList = new List<Buff>();

	public void dettachBuffFromEntityController(Buff buff, NinJaController entity)
	{
		buffDeleteList.Add (buff);
	}


	public void updateBufferList(){
		foreach (Buff buff in buffDeleteList) {
			buff.detach();
		}
		buffDeleteList.Clear ();
	}
}
