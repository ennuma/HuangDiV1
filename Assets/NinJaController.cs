using UnityEngine;
using System.Collections;

public class NinJaController : MonoBehaviour {

	public GameController ctrl;
	public GameController.Side side;
	public float attackCoolDown;
	public bool isDead = false;

	protected bool isFacingRight;
	protected float horiVelocity = 10.0f;
	protected float vertVelocity = 6.0f;
	protected float velocity = 1.0f;
	protected bool isFighting = false;
	protected Animator animator;
	protected float _attackCoolDown;


	protected int health = 30;
	protected int attack = 10;

	protected enum State{
		Find,
		Fight,
		Def,
		Escape,
		Dead
	}

	protected State state; 
	protected NinJaController targetEnemy;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		state = State.Find;
		if (transform.localScale.x < 0) {
			isFacingRight = false;		
		} else {
			isFacingRight = true;		
		}
		_attackCoolDown = attackCoolDown;
		isDead = false;
	}
	void Update(){
		if (isDead) {
			return;		
		}
		_attackCoolDown -= Time.deltaTime;
		if (_attackCoolDown < 0) {
			_attackCoolDown = 0;		
		}
		//renderer.sortingOrder = (int)transform.position.y;

	}
	// Update is called once per frame
	void FixedUpdate () {
		if (isDead) {
			return;		
		}

		float move = Input.GetAxis("Horizontal");
		rigidbody2D.velocity = new Vector2 (move * horiVelocity, rigidbody2D.velocity.y);
		if (move < 0 && isFacingRight) {
			flipFacing();		
		}
		if (move > 0 && !isFacingRight) {
			flipFacing();		
		}

		float move2 = Input.GetAxis("Vertical");
		rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, move2 * vertVelocity);

		if (rigidbody2D.velocity != Vector2.zero) {
			onMove();		
		}

		if (Input.GetMouseButtonDown (0)&&!isFighting) {
			onFight();		
		}
		if (Input.anyKey) {
			return;		
		}
		//my code begins here
		switch (state) {
		case State.Find:{
			onFindHandler();
		}	
			break;
		case State.Fight:{
			onFightHandler();
		}
			break;
		case State.Def:{

		}
			break;
		case State.Escape:{

		}
			break;
		case State.Dead:{
			onDeadHandler();
		}
			break;
		}

	}
	protected void onFightHandler(){
		//Debug.Log (_attackCoolDown);
		if (_attackCoolDown == 0) {
			//Debug.Log("atk");
			onFight ();		
			_attackCoolDown = attackCoolDown;
		} else {
			if(!isFighting){
				onIdle();
			}		
		}

	}
	void OnCollisionEnter2D(Collision2D coll){
		coll.gameObject.rigidbody2D.velocity = new Vector2 (0, 0);
	}
	void OnCollisionStay2D(Collision2D coll){
		//Debug.Log("col");
		NinJaController colliderContoller = coll.gameObject.GetComponent(typeof(NinJaController)) as NinJaController;
		if (isEnemy (colliderContoller)) {
			targetEnemy = colliderContoller;
			//onFight();
			state = State.Fight;
		}
	}
	void OnCollisionExit2D(Collision2D coll){
		//Debug.Log("exit");
		state = State.Find;
	}
	protected bool isEnemy(NinJaController other)
	{
		if (side == other.side) {
			return false;
		}
		return true;
	}
	protected void onFindHandler(){
		if (!targetEnemy) {
			targetEnemy = ctrl.getTargetEnemyForEntity (this);
		}
		if (targetEnemy == null) {
			Debug.Log("victory");
			return;
		}
		float distance = Vector2.Distance (targetEnemy.transform.position, transform.position);
		Vector2 nextPos = Vector2.Lerp (transform.position, targetEnemy.transform.position, velocity*Time.deltaTime / distance);
		rigidbody2D.MovePosition (nextPos);
		onMove ();
	}
	protected void onDeadHandler(){
		if (!isDead) {
			onDead ();
		}
		isDead = true;
		//Destroy(rigidbody2D);
		Collider2D col = GetComponent (typeof(PolygonCollider2D)) as Collider2D;
		col.enabled = false;
		ctrl.entityDead (this);
		rigidbody2D.velocity = new Vector2 (0, 0);
		renderer.sortingOrder = -999;
	}
	public void takeDamageFromEnemy(NinJaController other){
		Debug.Log(health);
		int dechealth = other.attack;
		health -= dechealth;
		if (health <= 0) {
			state = State.Dead;	
			Debug.Log (state.ToString ());
		}
	}
	protected void flipFacing(){
		isFacingRight = !isFacingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	protected void attackCallBackFunc(){
		animator.SetBool ("setAttack", false);
		isFighting = false;
		ctrl.attackEnemyController (this, targetEnemy);
		targetEnemy = null;
		state = State.Find;
	}

	protected void onFight(){
		animator.SetBool ("setAttack", true);
		isFighting = true;
	}
	protected void onMove(){
		//float velo = Vector2.SqrMagnitude (rigidbody2D.velocity);
		//Debug.Log (velo);
		animator.SetFloat ("velocity", 1.0f);
	}
	protected void onIdle(){
		animator.SetFloat ("velocity", 0.0f);
	}
	protected void onDead(){
		animator.SetBool ("setDead", true);
	}
	/**protected void deadCallBackFunc(){
		animator.SetBool ("setDead", false);
	}**/
}
