using UnityEngine;
using System.Collections;

public abstract class Champion : MonoBehaviour {

	private Stats stats = new Stats();
	public Settings settings = new Settings();
	
	public Ability Q_Ability;
	public Ability W_Ability;
	public Ability E_Ability;
	public Ability R_Ability;

	public Ability Q_holder = null;
	public Ability E_holder = null;
	public Ability W_holder = null;
	public Ability R_holder = null;

	public float Q_Cooldown;
	public float W_Cooldown;
	public float E_Cooldown;
	public float R_Cooldown;

	public Stats getStats(){
		return stats;
	}

	public void getDamage(int amount){
		stats.health -= amount;
	}

	public void Heal(int amount){
		stats.health += amount;
	}


}
