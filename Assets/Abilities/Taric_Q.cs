using UnityEngine;
using System.Collections;

public class Taric_Q : Ability {
	
	Taric taric_ref;

	// Use this for initialization
	void Start () {
		taric_ref = (Taric)getParent();

		Execute();

		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (taric_ref != null) {
		
		}
	}

	private void Execute(){
		taric_ref.Heal (10);


		//last line of execute will set the cooldown again
		setQCooldown (5);
	}
}
