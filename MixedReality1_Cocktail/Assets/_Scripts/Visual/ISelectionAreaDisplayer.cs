using UnityEngine;

public interface ISelectionAreaDisplayer
{
    /// <summary>
    /// Display a new Ingredient based on the information given
    /// </summary>
    /// <param name="toDisplay">Ingredient to display</param>
    void DisplayIngredient(Ingredient toDisplay);

    /// <summary>
    /// Called, when collision area is touched, but not yet long enough to count as selected
    /// </summary>
    void HighlightIngredientOnTouch();
    
    /// <summary>
    /// Called, when an ingredient was successfully selected
    /// </summary>
    void HighlightOnSelected();
}
