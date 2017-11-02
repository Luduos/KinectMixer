using UnityEngine;

public class SelectionAreaDisplayerDummy : SelectionAreaDisplayer
{
    [SerializeField]
    private SpriteRenderer m_DisplaySprite;
    [SerializeField]
    private SpriteRenderer m_BackgroundSprite;
    [SerializeField]
    private TextMesh m_Text;

    [SerializeField]
    private Color m_OnHoveredColor = new Color(1.0f, 0.5f, 0.0f);

    [SerializeField]
    private Color m_OnInvalidHoveringColor = new Color(1.0f, 0.0f, 0.0f);

    [SerializeField]
    private Color m_OnSelectedColor = new Color(0.0f, 1.0f, 0.0f);

    private Color m_OrigSpriteColor;

    private void Start()
    {
        m_OrigSpriteColor = m_BackgroundSprite.color;
    }

    public override void DisplayIngredient(Ingredient toDisplay)
    {
        m_DisplaySprite.sprite = toDisplay.m_Sprite;
        m_Text.text = toDisplay.m_Name;
    }

    public override void DisplayMixingMotion(MixingMotion toDisplay)
    {
        m_DisplaySprite.sprite = toDisplay.m_DescriptionSprite;
        m_Text.text = toDisplay.m_Name;
    }

    public override void HighlightOnHovered()
    {
        m_BackgroundSprite.color = m_OnHoveredColor;
    }

    public override void HighlightOnInvalidHovered()
    {
        m_BackgroundSprite.color = m_OnInvalidHoveringColor;
    }

    public override void HighlightOnSelected()
    {
        m_BackgroundSprite.color = m_OnSelectedColor;

    }

    public override void NoHighlights()
    {
        m_BackgroundSprite.color = m_OrigSpriteColor;
    }
}
