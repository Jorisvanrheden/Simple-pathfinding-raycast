using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour {

	//if you want to use variables or functions from the actual champion, you have to 
	//make a conversion from a Champion-type to a specific ChampionName-type
	
	private Champion parentChampion;

	private Champion targetChampion;

	public void setParent(Champion parent){
		parentChampion = parent;
	}

	public void setTargetChampion(Champion target){
		targetChampion = target;
	}

	public Champion getParent(){
		return parentChampion;
	}

	public Champion getTargetChampion(){
		return targetChampion;
	}

	public void setQCooldown(int cooldown){
		parentChampion.Q_Cooldown = cooldown;
	}

	public void setWCooldown(int cooldown){
		parentChampion.W_Cooldown = cooldown;
	}

	public void setECooldown(int cooldown){
		parentChampion.E_Cooldown = cooldown;
	}

	public void setRCooldown(int cooldown){
		parentChampion.R_Cooldown = cooldown;
	}
}
