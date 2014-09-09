using UnityEngine;
using System.Collections;

public class ManaBarConfig : MonoBehaviour {

	private Vector3 offset;
	private float manaScale;
	private float targetPercentage;
	private float alpha = 0.5f;
	// Use this for initialization
	void Start () {
		offset = new Vector3 (-renderer.bounds.size.x/2.0f, 1.0f+renderer.bounds.size.y-0.1f, 0.0f);
		manaScale = Mathf.Abs( transform.localScale.x);
		Color color = renderer.material.color;
		color.a = Mathf.Abs(alpha);
		renderer.material.color = color;
		reset ();
		//transform.position = transform.position+offset;
	}
	void Update(){
		/**
		renderer.sortingOrder = transform.parent.renderer.sortingOrder;
		float absval = Mathf.Abs (transform.localScale.x);;
		transform.localScale = new Vector3(absval,transform.localScale.y,transform.localScale.z);
		**/
	}
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = transform.parent.position + offset;
		renderer.sortingOrder = transform.parent.renderer.sortingOrder;
		float currentPercentage = Mathf.Abs (transform.localScale.x / manaScale);
		float multiplier = currentPercentage;
		if (targetPercentage > currentPercentage ) {
			multiplier = (currentPercentage + 0.01f);
			//Debug.Log(multiplier);
			//SpriteRenderer rd = GetComponent (SpriteRenderer);
			Color color = renderer.material.color;
			color.a = Mathf.Abs(currentPercentage+0.01f)*alpha;
			renderer.material.color = color;
		}
		setXScale(manaScale*multiplier);
	}

	void setXScale(float x)
	{
		if ((transform.parent.localScale.x < 0 && x > 0) || (transform.parent.localScale.x > 0 && x < 0)) {
			x=-x;		
		}
		transform.localScale = new Vector3 (x, transform.localScale.y, transform.localScale.z);
	}

	public void reset(){
		targetPercentage = 0.0f;
		setXScale (0);
	}
	
	public void setmanaBarPercentage(float percentage)
	{
		//Debug.Log(percentage);
		//transform.localScale = new Vector3 (percentage*manaScale, transform.localScale.y, transform.localScale.z);
		//SpriteRenderer rd = GetComponent (SpriteRenderer);
		//Color color = renderer.material.color;
		//color.a = percentage;
		//renderer.material.color = color;
		//((SpriteRenderer)GetComponent(SpriteRenderer)).color.a = percentage;
		//Debug.Log (percentage);
		targetPercentage = percentage;
	}
}
