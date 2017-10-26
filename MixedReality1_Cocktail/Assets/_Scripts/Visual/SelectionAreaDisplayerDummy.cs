using UnityEngine;

public class SelectionAreaDisplayerDummy : SelectionAreaDisplayer
{
    [SerializeField]
    private SpriteRenderer m_SpriteRenderer;
    [SerializeField]
    private TextMesh m_Text;

    public override void DisplayIngredient(Ingredient toDisplay)
    {
        m_SpriteRenderer.sprite = toDisplay.m_Sprite;
        m_Text.text = toDisplay.m_Name;
    }

    public override void DisplayMixingMotion(MixingMotion toDisplay)
    {
        m_SpriteRenderer.sprite = toDisplay.m_DescriptionSprite;
        m_Text.text = toDisplay.m_Name;
    }

    public override void HighlightIngredientOnTouch()
    {
        throw new System.NotImplementedException();
    }

    public override void HighlightOnSelected()
    {
        throw new System.NotImplementedException();
    }
}
