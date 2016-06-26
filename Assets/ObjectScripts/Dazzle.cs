using UnityEngine;
using System.Collections;

public class Dazzle : MonoBehaviour {

	private Champion target;

	// Update is called once per frame
	void Update () {
		if (target != null) {
			transform.LookAt(target.transform.position);
			transform.Translate(Vector3.forward*5*Time.deltaTime);

			if(Vector3.Distance(transform.position, target.transform.position) < 1){
				target.getDamage(20);
				Destroy(gameObject);
			}
		}
	}

	public void setTarget(Champion _target){
		target = _target;
	}

}
