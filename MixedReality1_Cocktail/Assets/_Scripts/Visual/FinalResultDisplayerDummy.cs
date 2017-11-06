using UnityEngine;

public class FinalResultDisplayerDummy : FinalResultDisplayer
{

    [SerializeField]
    private KinectInput m_KinectInput;

    [SerializeField]
    private TextMesh m_DisplayText;

    [SerializeField]
    private SpriteRenderer m_CocktailGlassFillDisplay;

    [SerializeField]
    private SpriteRenderer m_FoamDisplay;

    private GameObject rightHand;

    private void Start()
    {

       
    }

    public override void DisplayFinalResult(FinalResult toDisplay)
    {
        this.gameObject.SetActive(true);

        print(toDisplay.m_Name);
        print(toDisplay.m_Color);

        m_DisplayText.text = toDisplay.m_Name;
        m_CocktailGlassFillDisplay.color = new Color(toDisplay.m_Color.r, toDisplay.m_Color.g, toDisplay.m_Color.b, 0.5f);
        m_FoamDisplay.sprite = toDisplay.m_Foam;
        m_FoamDisplay.color = toDisplay.m_FoamColor;
    }


}
