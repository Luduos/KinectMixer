using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    [SerializeField]
    private SpriteRenderer CocktailSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	public void ShowCocktailSprite(bool visible)
    {
        CocktailSprite.gameObject.SetActive(visible);
    }
}
