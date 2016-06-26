using UnityEngine;
using System.Collections;

public class Ezreal_Q : Ability {

	//Mystic shot
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
			Vector3 target = hit.point;
			target.y = ezreal_ref.transform.position.y;

			//look at the target
			ezreal_ref.transform.LookAt(target);

			GameObject mystic = (GameObject)Instantiate (Resources.Load ("Mystic Shot"), ezreal_ref.transform.position, Quaternion.identity);
			mystic.GetComponent<MysticShot>().fireInDirection(target, ezreal_ref);
		
		}
		
		//last line of execute will set the cooldown again
		setQCooldown (2);
	}
}
