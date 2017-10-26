using UnityEngine;
using UnityEngine.Events;

public class MotionDetector : MonoBehaviour {

    public UnityAction<int> OnMotionSelected;

	void Update () {
        DummyMotionDetection();
	}

    private void DetectMotions()
    {
        // TODO: Actually put Kinnect code in here


    }

    /// <summary>
    /// Checks for number-keys "1", "2" and "3" on top of the alphanumerical keyboard, if one was pressed.
    /// If yes, invokes action, that the motion the "key-number - 1"-ID was selected
    /// </summary>
    private void DummyMotionDetection()
    {
        if(null != OnMotionSelected)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                OnMotionSelected.Invoke(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                OnMotionSelected.Invoke(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                OnMotionSelected.Invoke(2);
            }
        }
    }
}
