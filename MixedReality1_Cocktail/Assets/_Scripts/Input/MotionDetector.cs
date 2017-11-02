using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class MotionDetector : MonoBehaviour, KinectGestures.GestureListenerInterface
{

    public UnityAction<int> OnMotionSelected;

    private Dictionary<KinectGestures.Gestures, MixingMotion> m_MixingMotionDictionary = new Dictionary<KinectGestures.Gestures, MixingMotion>();

    public void SetPossibleMotions(MixingMotion[] possibleMotions)
    {
        foreach(MixingMotion motion in possibleMotions)
        {
            m_MixingMotionDictionary.Add(motion.m_Gesture, motion);
        }
    }

	void Update () {
        DummyMotionDetection();
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

    public void UserDetected(long userId, int userIndex)
    {
    }

    public void UserLost(long userId, int userIndex)
    {
    }

    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
    }

    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint, Vector3 screenPos)
    {
        if (this.gameObject.activeSelf)
        {
            switch (gesture)
            {
                case KinectGestures.Gestures.SwipeLeft:
                case KinectGestures.Gestures.SwipeRight:
                    OnMotionSelected.Invoke(1);
                    break;
                case KinectGestures.Gestures.SwipeDown:
                case KinectGestures.Gestures.SwipeUp:
                    OnMotionSelected.Invoke(0);
                    break;
                default:
                    OnMotionSelected.Invoke(2);
                    break;
            }
        }
        return true;
    }

    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint)
    {
        return true;
    }
}
