using UnityEngine;
using System.Collections;

public class Taric_E : Ability {

	//Dazzle

	Taric taric_ref;
	
	// Use this for initialization
	void Start () {
		taric_ref = (Taric)getParent();
	}
	
	// Update is called once per frame
	void Update () {
		if (taric_ref != null) {

			//draw range indicator

			if(Input.GetMouseButtonUp(0)){
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if(Physics.Raycast(ray,out hit)){
					
					Champion target = hit.transform.gameObject.GetComponent<Champion>();
					setTargetChampion(target);

					if(target!=null){
						Execute();
						Destroy(gameObject);
					}
				}
			}
			foreach(KeyCode key in taric_ref.settings.skillButtons){
				if(Input.GetKeyDown(key)){
					if(key!=KeyCode.E){
						Destroy(gameObject);
					}
				}
			}
		}
	}
	
	private void Execute(){

		GameObject dazzle = (GameObject)Instantiate (Resources.Load ("Dazzle"), taric_ref.transform.position, Quaternion.identity);
		dazzle.GetComponent<Dazzle>().setTarget (getTargetChampion());
		
		
		//last line of execute will set the cooldown again
		setECooldown (3);
	}
}
