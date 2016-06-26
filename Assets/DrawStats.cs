using UnityEngine;
using System.Collections;

public class DrawStats : MonoBehaviour {

	Champion champion;

	private int width = 100;
	private int height = 20;

	void Awake(){
		champion = GetComponent<Champion> ();
	}

	void OnGUI(){
		Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
		
		GUI.Box (new Rect (pos.x - width / 2, Screen.height - pos.y - height*1.5f, width, height), champion.getStats ().name);
		GUI.Box (new Rect (pos.x - width / 2, Screen.height - pos.y + height, width * (champion.getStats ().health/100), height), "");

		GUI.Box(new Rect(0,0,100,100), ((float)1/Time.deltaTime).ToString());
	}
}
