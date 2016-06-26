using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseControl : MonoBehaviour {

	private Champion champion;

	private Vector3 wayPoint;

	private Vector3 newPos;

	private bool hasWaypoint = false;

	private List<Vector3> waypoints;
	private int currentWaypoint = 0;

	private GameObject goal;
	private GameObject goalRef;

	//every character has an instance of a pathfinder to find the path
	private Pathfinder pathfinder;
	
	void Awake(){
		champion = GetComponent<Champion> ();
		wayPoint = champion.transform.position;

		pathfinder = new Pathfinder ();
		goal = (GameObject)Resources.Load ("goal");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(1)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit)){
				//make sure the place can is actually a reachable place
				if(hit.transform.gameObject.name == "Floor"){

					if(goalRef!=null)Destroy(goalRef);
					goalRef = (GameObject)Instantiate(goal, hit.point, Quaternion.identity);

					newPos = hit.point;
					newPos.y = champion.transform.position.y;

					waypoints = pathfinder.newPath(champion.transform.position, newPos);
					currentWaypoint = 0;
					
					setHasWaypoint(true);
				}
			}
		}
			
		if (hasWaypoint) {
			transform.LookAt(waypoints[currentWaypoint]);
			transform.Translate(Vector3.forward*champion.getStats().movementSpeed*Time.deltaTime);
			
			if(Vector3.Distance(transform.position, waypoints[currentWaypoint]) < 0.5f){
				if(currentWaypoint<waypoints.Count-1){
					currentWaypoint ++;
				}
				else{
					setHasWaypoint(false);

					if(goalRef!=null)Destroy(goalRef);
				}
			}

			waypoints = pathfinder.newPath(champion.transform.position, newPos);
			currentWaypoint = 0;
		}
	}

	public void setHasWaypoint(bool setter){
		hasWaypoint = setter;
		if (setter) {
			//champion.animation.Play("RUN00_F");
		}
		//else champion.animation.Play("WAIT02");
	}
}
