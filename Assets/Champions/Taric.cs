using UnityEngine;
using System.Collections;

public class Taric : Champion {
	
	// Use this for initialization
	void Start () {
		getStats ().name = "Taric";
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q)){
			if(Q_Cooldown==0){
				//ensuring that you can only have one instance that will load the actual skill for you
				if(Q_holder==null){
					Q_holder = (Ability)Instantiate (Q_Ability, transform.position, Quaternion.identity);
					Q_holder.setParent (this);
				}
			}
		}
		else if(Input.GetKeyDown(KeyCode.W)){
			if(W_Cooldown==0){
				//ensuring that you can only have one instance that will load the actual skill for you
				if(W_holder==null){
					W_holder = (Ability)Instantiate (W_Ability, transform.position, Quaternion.identity);
					W_holder.setParent (this);
				}
			}
		}
		else if(Input.GetKeyDown(KeyCode.E)){
			if(E_Cooldown==0){
				//ensuring that you can only have one instance that will load the actual skill for you
				if(E_holder==null){
					E_holder = (Ability)Instantiate (E_Ability, transform.position, Quaternion.identity);
					E_holder.setParent (this);
				}
			}
		}
		else if(Input.GetKeyDown(KeyCode.R)){
			if(R_Cooldown==0){
				//ensuring that you can only have one instance that will load the actual skill for you
				if(R_holder==null){
					R_holder = (Ability)Instantiate (R_Ability, transform.position, Quaternion.identity);
					R_holder.setParent (this);
				}
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
