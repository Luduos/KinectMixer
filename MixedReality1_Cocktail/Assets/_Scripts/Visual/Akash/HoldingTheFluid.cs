using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldingTheFluid : MonoBehaviour {
	public GameObject FillingComp ;
	//private Canvas FillingCompCanvas;
	private Image FillingCompImage;


	void OnParticleCollision(GameObject other){

	
	FillingCompImage = FillingComp.GetComponent<Image>();
        //David here below
      //  FillingCompImage.color = sum Of the SelectedObjects colours;
	FillingCompImage.fillAmount +=0.05f;
	Debug.Log("filling");



	Debug.Log(other +"is the object in parameters");
	Debug.Log("Collision detected from PS Script");


	}
}
