using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinder {

	//every waypoint will have a list of hits to look from
	private List<Node> waypoints = new List<Node>();



	void Start(){

		//set the first waypoint

	}

	void Update(){
		//waypoints [0] = new Node (start, end);
	}

	public List<Vector3> newPath(Vector3 start, Vector3 end){
		List<Vector3> points = new List<Vector3>();
		Node path = new Node (start, end, points);

		return points;
	}
}