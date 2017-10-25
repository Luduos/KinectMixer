using UnityEngine;

public abstract class SelectionAreaDisplayer : MonoBehaviour
{
    /// <summary>
    /// Display a new Ingredient based on the information given
    /// </summary>
    /// <param name="toDisplay">Ingredient to display</param>
    public abstract void DisplayIngredient(Ingredient toDisplay);

    /// <summary>
    /// Called, when collision area is touched, but not yet long enough to count as selected
    /// </summary>
    public abstract  void HighlightIngredientOnTouch();

    /// <summary>
    /// Called, when an ingredient was successfully selected
    /// </summary>
    public abstract  void HighlightOnSelected();
}
