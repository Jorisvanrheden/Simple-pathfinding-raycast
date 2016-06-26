using UnityEngine;
using System.Collections;

public class Ezreal : Champion {

	// Use this for initialization
	void Start () {
		getStats ().name = "Ezreal";
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q)){
			if(Q_Cooldown==0){
				Ability test = (Ability)Instantiate (Q_Ability, transform.position, Quaternion.identity);
				test.setParent (this);
			}
		}
		else if(Input.GetKeyDown(KeyCode.W)){
			if(W_Cooldown==0){
				Ability test = (Ability)Instantiate (W_Ability, transform.position, Quaternion.identity);
				test.setParent (this);
			}
		}
		else if(Input.GetKeyDown(KeyCode.E)){
			if(E_Cooldown==0){
				Ability test = (Ability)Instantiate (E_Ability, transform.position, Quaternion.identity);
				test.setParent (this);
			}
		}
		else if(Input.GetKeyDown(KeyCode.R)){
			if(R_Cooldown==0){
				Ability test = (Ability)Instantiate (R_Ability, transform.position, Quaternion.identity);
				test.setParent (this);
			}
		}
		
		Q_Cooldown -= 1 * Time.deltaTime;
		if (Q_Cooldown < 0)
			Q_Cooldown = 0;
		W_Cooldown -= 1 * Time.deltaTime;
		if (W_Cooldown < 0)
			W_Cooldown = 0;
		E_Cooldown -= 1 * Time.deltaTime;
		if (E_Cooldown < 0)
			E_Cooldown = 0;
		R_Cooldown -= 1 * Time.deltaTime;
		if (R_Cooldown < 0)
			R_Cooldown = 0;
	}
}
