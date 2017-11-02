using UnityEngine;

public class FinalResultDisplayerDummy : FinalResultDisplayer
{

    [SerializeField]
    private KinectInput m_KinectInput;

    private GameObject rightHand;

    private void Start()
    {

       
    }

    public override void DisplayFinalResult(FinalResult toDisplay)
    {
        this.gameObject.SetActive(true);

        print(toDisplay.m_Name);
        print(toDisplay.m_Color);
    }


}
