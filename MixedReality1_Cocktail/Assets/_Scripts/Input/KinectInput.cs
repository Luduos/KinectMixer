using Windows.Kinect;
using UnityEngine;
using System.Collections.Generic;

public class KinectInput : MonoBehaviour {

    [SerializeField]
    private BodySourceManager bodySourceManager;

    private Dictionary<ulong, GameObject> m_TrackedBodyObjects = new Dictionary<ulong, GameObject>();

    IList<Body> m_Bodies;
    // Use this for initialization
    void Start () {
        /**
        if(null != m_Sensor)
        {
            m_Sensor.Open();

            m_FrameReader = m_Sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color |
                                             FrameSourceTypes.Depth |
                                             FrameSourceTypes.Infrared |
                                             FrameSourceTypes.Body);
            m_FrameReader.MultiSourceFrameArrived += OnSourceFrameArrived;
        }
        */
    }

    /*
    private void OnSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs eventArgs)
    {
        MultiSourceFrame multiFrame = eventArgs.FrameReference.AcquireFrame();

        using(BodyFrame bodyFrame = multiFrame.BodyFrameReference.AcquireFrame()){
            if (null != bodyFrame)
            {
                m_Bodies = new Body[bodyFrame.BodyFrameSource.BodyCount];
                bodyFrame.GetAndRefreshBodyData(m_Bodies);

                foreach (Body currentBody in m_Bodies)
                {
                    if (null != currentBody)
                    {
                        CheckBody(currentBody);
                    }
                }
            }
        } 
    }
    */

    private void CheckBody(Body body)
    {
        if (body.IsTracked)
        {
            Windows.Kinect.Joint handRight = body.Joints[JointType.HandRight];

            float X = handRight.Position.X;
            float Y = handRight.Position.Y;
            float Z = handRight.Position.Z;

            print("X: " + X + " Y: " + Y + " Z: " + Z);
            this.transform.position = new Vector3(X, Y, Z);
        }
    }
	
	// Update is called once per frame
	void Update () {

        Body[] currentBodies = bodySourceManager.GetData();

        if (null == currentBodies)
        {
            return;
        }

        List<ulong> kinectTrackedIds = GetKinectTrackedIds(currentBodies);

        DeleteUntrackedBodies(kinectTrackedIds);

        foreach (Body body in currentBodies)
        {
            if (null != body && body.IsTracked)
            {
                if (!m_TrackedBodyObjects.ContainsKey(body.TrackingId))
                {
                    m_TrackedBodyObjects[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }

                RefreshBodyObject(body, m_TrackedBodyObjects[body.TrackingId]);
            }
        }

    }

    /// <summary>
    /// Get body-Ids that are being tracked by kinect
    /// </summary>
    /// <param name="currentBodies">Bodies that are being tracked by kinect</param>
    /// <returns>body-Ids that are being tracked by kinect</returns>
    private List<ulong> GetKinectTrackedIds(Body[] currentBodies)
    {
        List<ulong> kinectTrackedIds = new List<ulong>();
        foreach (Body body in currentBodies)
        {
            if (null != body && body.IsTracked)
            {
                kinectTrackedIds.Add(body.TrackingId);
            }
        }
        return kinectTrackedIds;
    }

    /// <summary>
    /// Deletes Bodies, that are not being tracked by kinect anymore, by destroying
    /// the corresponding Gameobjects and deleting the entry in the tracking-dictionary
    /// </summary>
    /// <param name="KinectTrackedIds">Ids that are being tracked by the kinect</param>
    private void DeleteUntrackedBodies(List<ulong> KinectTrackedIds)
    {
        List<ulong> currentlyKnownIds = new List<ulong>(m_TrackedBodyObjects.Keys);

        // First delete untracked bodies
        foreach (ulong currentlyTrackedId in currentlyKnownIds)
        {
            // if body is not tracked by kinect anymore
            if (!KinectTrackedIds.Contains(currentlyTrackedId))
            {
                // remove the gameobject representing the body
                Destroy(m_TrackedBodyObjects[currentlyTrackedId]);
                //remove trackedId from the dictionary
                m_TrackedBodyObjects.Remove(currentlyTrackedId);
            }
        }
    }

    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);

        // TODO: Create objects for left and right hand

        return body;
    }

    private void RefreshBodyObject(Body body, GameObject bodyObject)
    {

        // TODO: Update left and right hand position
    }
}
