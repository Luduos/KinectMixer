using UnityEngine;

/// <summary>
/// Should be derived by any display, that wants to show the selection area. 
/// Abstract functions are called by the Logic-Systems to pass over needed
/// visualization information.
/// </summary>
public abstract class SelectionAreaDisplayer : MonoBehaviour
{
    /// <summary>
    /// Display a Ingredient based on the information given
    /// </summary>
    /// <param name="toDisplay">Ingredient to display</param>
    public abstract void DisplayIngredient(Ingredient toDisplay);

    /// <summary>
    /// Display a Mixing Motion based on the information given.
    /// </summary>
    /// <param name="toDisplay">Mixing motion to display</param>
    public abstract void DisplayMixingMotion(MixingMotion toDisplay);

    /// <summary>
    /// Called, when collision area is touched, but not yet long enough to count as selected
    /// </summary>
    public abstract  void HighlightIngredientOnTouch();

    /// <summary>
    /// Called, when an ingredient was successfully selected
    /// </summary>
    public abstract  void HighlightOnSelected();
}
