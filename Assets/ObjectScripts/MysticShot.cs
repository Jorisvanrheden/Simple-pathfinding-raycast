using UnityEngine;
using System.Collections;

public class MysticShot : MonoBehaviour {

	private Vector3 direction = new Vector3(0,0,0);
	private Vector3 startPos;

	private Champion sourceChampion;

	private int projectileSpeed = 20;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (direction != Vector3.zero) {
			transform.Translate(Vector3.forward*projectileSpeed*Time.deltaTime);

			if(Vector3.Distance(startPos, transform.position)>10){
				Destroy(gameObject);
			}
		}
	}

	public void fireInDirection(Vector3 target, Champion source){
		//normalize direction
		direction = (target - source.transform.position).normalized;

		//store sourcePos
		sourceChampion = source;
		startPos = source.transform.position;

		//make sure the object faces the target
		transform.LookAt(target);
	}
}
