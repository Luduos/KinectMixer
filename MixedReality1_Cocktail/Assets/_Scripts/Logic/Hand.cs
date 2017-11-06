using UnityEngine;

public class Hand : MonoBehaviour {
    [SerializeField]
    private SpriteRenderer CocktailSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	public void ShowCocktailSprite(bool visible)
    {
        if(null != CocktailSprite)
        {
            this.gameObject.SetActive(visible);
        }
    }
}
