using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flowing : MonoBehaviour {
	private ParticleSystem particles;
	private bool flow;
	public float speed = 1.0f;
	public float rate = 5.0f;

	void Start () {
	particles = GetComponent<ParticleSystem>();
	flow=false;
		
	}
	
	// Update is called once per frame
	void Update () {
//	Debug.Log("flowing");
//	Debug.Log(transform.parent.gameObject.name);
//	Debug.Log(transform.parent.rotation.z);
//	Debug.Log(flow);

		if(transform.parent.rotation.z > 0.6 || transform.parent.rotation.z < -0.6){
				flow = true;
				//Debug.Log("flow is now true");
			
				if (flow==true)
						{floww();}}
		else if(transform.parent.rotation.z < 0.6 || transform.parent.rotation.z > -0.6){
						
					flow = false;
					stopFloww();
					//Debug.Log("Flow is now False");
					}
		}
	

	void floww(){
		
//			Debug.Log("Gonna flow from the particle system");
//			Debug.Log(particles.isEmitting);
			particles.Play();
			var main = particles.main;
		    var em = particles.emission;


			main.startSpeed= speed;

			em.enabled=true;
			em.rateOverTime=rate;


		
}

	void stopFloww(){
		particles.Stop();
	}


	}
