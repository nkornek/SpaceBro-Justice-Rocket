using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {
	
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target, bg;
	public bool cangrow;
	
	// Update is called once per frame
	void Update () 
	{
		if (target)
		{
			Vector3 point = camera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(point.x, 0.5f, 20)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			transform.position = new Vector3 (target.position.x, transform.position.y, transform.position.z);
		}		
	}
}