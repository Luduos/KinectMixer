using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelectedObjects : MonoBehaviour {

	private Button button; 
	private bool selected;
	public Animator animator;
	//Array for objects that have been selected

	//Finally selected objects
	public GameObject[] selectedObjects = new GameObject[3];


	//Temp objects to select
	public List<GameObject> temporarySelectedObjects = new List<GameObject>();


	//	SelectedObjects Instantaited
	GameObject obj1;
	GameObject obj2;
	GameObject obj3;

	//Arrays for objects to select from
	public GameObject[] fruits;
	public GameObject[] alcohol;
	public GameObject[] mixer;

	private static int round;
	
	void Start () {

	button = FindObjectOfType<Button>();
	round = 1;


	Debug.Log(round);
	}

	public void displaySelectionObjects(){
		if(round ==1){

			obj1 = Instantiate (this.fruits[0], new Vector3(-300,-500,1),Quaternion.identity);
		 obj2 = Instantiate (this.fruits[1], new Vector3(0,-500,1),Quaternion.identity);
		 obj3 = Instantiate (this.fruits[2], new Vector3(300,-500,1),Quaternion.identity);

		temporarySelectedObjects.Add(obj1);
		temporarySelectedObjects.Add(obj2);
		temporarySelectedObjects.Add(obj3);
		button.interactable= false;
	}

		else if (round ==2){
			obj1 = Instantiate (this.alcohol[0], new Vector3(-300,-500,1),Quaternion.identity);
		 obj2 = Instantiate (this.alcohol[1], new Vector3(0,-500,1),Quaternion.identity);
		 obj3 = Instantiate (this.alcohol[2], new Vector3(300,-500,1),Quaternion.identity);
		temporarySelectedObjects.Add(obj1);
		temporarySelectedObjects.Add(obj2);
		temporarySelectedObjects.Add(obj3);
		button.interactable= false;
		}

		else if (round ==3){
			obj1 = Instantiate (this.mixer[0], new Vector3(-300,-500,1),Quaternion.identity);
		 obj2 = Instantiate (this.mixer[1], new Vector3(0,-500,1),Quaternion.identity);
		 obj3 = Instantiate (this.mixer[2], new Vector3(300,-500,1),Quaternion.identity);
		temporarySelectedObjects.Add(obj1);
		temporarySelectedObjects.Add(obj2);
		temporarySelectedObjects.Add(obj3);
		button.interactable= false;
		}

    }	


	// Update is called once per frame
	void Update() {

	Debug.Log(round);

	if(Input.GetMouseButtonDown(0)){
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			int i= round;
         if (hit)
			{ round++;
				button.interactable = true;
             Debug.Log(hit.transform.gameObject);
				selectedObjects[i-1]=hit.transform.gameObject;
				GameObject theObject = selectedObjects[i-1];
				animator = theObject.GetComponent<Animator>();
				animator.SetBool("selected",true);


				selected = true;
				}
			if(selected== true){		
			foreach(GameObject obj in temporarySelectedObjects){
					if(obj != hit.transform.gameObject && (obj != selectedObjects[0] && obj != selectedObjects[1] && obj != selectedObjects[2] )){
				Destroy(obj.gameObject);
				selected=false;}}
         
     }

					
			}




	}

}

		
	







