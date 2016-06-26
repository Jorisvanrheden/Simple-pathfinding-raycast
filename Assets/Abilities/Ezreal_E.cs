using UnityEngine;
using System.Collections;

public class Ezreal_E : Ability {

	//Blink
	Ezreal ezreal_ref;
	
	// Use this for initialization
	void Start () {
		ezreal_ref = (Ezreal)getParent();

		Execute();
		
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (ezreal_ref != null) {
			
		}
	}
	
	private void Execute(){
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray,out hit)){
			Vector3 newPos = hit.point;
			newPos.y = ezreal_ref.transform.position.y;
			ezreal_ref.transform.position = newPos;

			//find around lying targets
		}

		ezreal_ref.GetComponent<MouseControl> ().setHasWaypoint (false);
		
		//last line of execute will set the cooldown again
		setECooldown (5);
	}
}
