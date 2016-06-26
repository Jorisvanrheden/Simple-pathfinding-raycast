using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node{

	private Vector3 pos;
	private Vector3 goal;

	//perhaps also a node list 
	private List<Node> childrenNodes = new List<Node>();

	//properties of the ray
	private float radius = 100.0f;
	private float drawDuration = 3f;
	private int rayCount = 45;
	private float to360factor;

	//setting to high value to set 'not used', to prevent comparing with 0 
	private float lastMissAngle = 999;
	private float lastHitAngle = 999;

	private GameObject obj;

	private List<Vector3> positions = new List<Vector3>();	

	private float pathOffset = 0.3f;
	
	public Node(Vector3 _pos, Vector3 _goal, List<Vector3> points){
		pos = _pos;
		goal = _goal;

		to360factor = (360 / (float)rayCount);

		//fireRays (pos, goal);
		if(Physics.Linecast (pos, goal)) {
			//fire rays over 360 degrees to find potential points
			fireRays (pos, goal);
			//compare and calculate best point to continue from
			comparePositions (points);
		}
		else{
			Debug.DrawLine(pos, goal, Color.green, drawDuration);
			points.Add(goal);
			//Debug.Log("Path found");
		}
	}

	private void fireRays(Vector3 start, Vector3 end){
		//fire a new ray and store in a list to get the data from previous points
		for (int i=0; i<rayCount+1; i++) {

			Vector3 hitPoint = new Vector3(radius*Mathf.Cos (i*to360factor*Mathf.Deg2Rad),0,radius*Mathf.Sin (i*to360factor*Mathf.Deg2Rad) );

			RaycastHit hit;
			if (Physics.Linecast (pos, hitPoint, out hit)) {

				//Debug.DrawLine (pos, hit.point, Color.black, drawDuration);

				if(obj==null){
					obj = hit.transform.gameObject;
				}
				if(obj == hit.transform.gameObject){
					
				}
				else{
					findEdgeException(lastHitAngle, i, hit.transform.gameObject);
					obj = hit.transform.gameObject;
				}

				lastHitAngle = i;
				
				//don't make a comparison if the numbers haven't been set yet
				if(lastMissAngle!=999){
					//check if the current ray is the following of a ray that misses, so only iterate back in this case
					if(lastHitAngle-lastMissAngle == 1){
						//go back to the previous non hit distance, to determine the edge
						findEdge(i, lastMissAngle);
					}
				}
			} 
			else{
				//
				//Debug.DrawLine (pos, hitPoint, Color.black, drawDuration);

				//before storing new lastMissAngle, check if it's different than the previous, so we know if we can find an edge again
				//also make sure the lastHitAngle variable has been assigned to some number before, so you can pass it as a parameter
				if(lastMissAngle!=i && lastHitAngle!=999){
					if(i-lastHitAngle == 1){
						//need some exceptions here, but for now keep the amount of rays limited
						findEdge(lastHitAngle, i);
					}
				}
				//store the last angle, so you can compare it when there does occur a hit
				lastMissAngle = i;
			}
		}
	}

	private void findEdgeException(float angle, float lastAngle, GameObject o){
		float interpolate = (float)(lastAngle + angle)/2;
		Vector3 newPoint = new Vector3(radius*Mathf.Cos (interpolate*to360factor*Mathf.Deg2Rad),0,radius*Mathf.Sin (interpolate*to360factor*Mathf.Deg2Rad) );

		RaycastHit hit;
		//make sure that the hit that is being processed comes from the same gameobject
		if (Physics.Linecast (pos, newPoint, out hit) && hit.transform.gameObject == o) {
			//Debug.DrawLine (pos, newPoint, Color.black, drawDuration);

			//Debug.Log(Mathf.Abs(angle*to360factor-lastAngle*to360factor));
			//check if the angle difference is small enough to get a better idea where the edge is, else loop again
			if(Mathf.Abs(angle*to360factor-lastAngle*to360factor)>0.2f){
				//no need to adjust the lastRayAngle, since we want to compare the point that is closer to the edge to the point that has no hits
				findEdgeException(angle, interpolate, o);
			}
			else{
				//compare last two results, then make the point an expectation of that trend
				//previous result
				float diff = pathOffset/to360factor;
			
				Vector3 old = new Vector3(radius*Mathf.Cos ((interpolate-diff)*to360factor*Mathf.Deg2Rad),0,radius*Mathf.Sin ((interpolate-diff)*to360factor*Mathf.Deg2Rad) );
				//Debug.DrawLine (pos, hit.point, Color.black, drawDuration);
				Vector3 newPos = (newPoint-old)/2;
				Vector3 d = hit.point - newPos;

				//extra exception, for the second hit of the object, it normally iteration to the right, but that doesn't work here
				//so since the newPos will be negative, if we change the - in a + to get the desired result, in contrary of the other function
				if (Physics.Linecast (pos, d, out hit)){
					d = hit.point + newPos;
				}
				//Debug.DrawLine (pos, d, Color.black, drawDuration);
				positions.Add(d);
			}
		} 
		else {
			//set the new angle to compare with as the interpolation that has been done
			findEdgeException(interpolate, lastAngle, o);	
		}
	}

	private void findEdge(float angle, float lastAngle){
		float interpolate = (float)(lastAngle + angle)/2;
		Vector3 newPoint = new Vector3(radius*Mathf.Cos (interpolate*to360factor*Mathf.Deg2Rad),0,radius*Mathf.Sin (interpolate*to360factor*Mathf.Deg2Rad) );

		RaycastHit hit;
		if (Physics.Linecast (pos, newPoint, out hit)) {
			//Debug.DrawLine (pos, hit.point, Color.black, drawDuration);

			//check if the angle difference is small enough to get a better idea where the edge is, else loop again
			if(Mathf.Abs(angle*to360factor-lastAngle*to360factor)>0.2f){

				//no need to adjust the lastRayAngle, since we want to compare the point that is closer to the edge to the point that has no hits
				findEdge(interpolate, lastAngle);
			}
			else{
				//use the angle one iteration before the hitpoint angle and compare it to that one's previous not-hitting angle
				//now you can check the distances between these two vectors in order to find out what way the new node point has to be positioned
				float diff = pathOffset/to360factor;
				Vector3 old = new Vector3();
				if(angle-lastAngle > 0){
					old = new Vector3(radius*Mathf.Cos ((interpolate-diff)*to360factor*Mathf.Deg2Rad),0,radius*Mathf.Sin ((interpolate-diff)*to360factor*Mathf.Deg2Rad) );
					//Debug.Log ("pos");
				}
				else{

					old = new Vector3(radius*Mathf.Cos ((interpolate+diff)*to360factor*Mathf.Deg2Rad),0,radius*Mathf.Sin ((interpolate+diff)*to360factor*Mathf.Deg2Rad) );
					//Debug.Log ("neg");
				}

				Vector3 newPos = (newPoint-old)/2;
				Vector3 d = hit.point - newPos;
				//Debug.Log(d);
				//Debug.DrawLine (pos, d, Color.black, drawDuration);
				positions.Add(d);
			}
		} 
		else {
			//Debug.DrawLine (pos, newPoint, Color.black, drawDuration);
			//set the new angle to compare with as the interpolation that has been done
			lastAngle = interpolate;
			findEdge(angle, lastAngle);
		}
	}

	private void comparePositions(List<Vector3> points){
		float dist = 9999;
		Vector3 bestPos = new Vector3 ();
		foreach (Vector3 p in positions) {
			if(Vector3.Distance(p, goal)<dist){
				dist = Vector3.Distance(p,goal);
				bestPos = p;
			}
		}

		points.Add (bestPos);
		childrenNodes.Add(new Node(bestPos, goal, points));
		Debug.DrawLine (pos, bestPos, Color.black, drawDuration);
	}
}
