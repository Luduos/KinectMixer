using Windows.Kinect;
using UnityEngine;
using System.Collections.Generic;

public class KinectInput : MonoBehaviour {

    KinectSensor m_Sensor;
    MultiSourceFrameReader m_FrameReader;
    IList<Body> m_Bodies;
    // Use this for initialization
    void Start () {
        m_Sensor = KinectSensor.GetDefault();

        if(null != m_Sensor)
        {
            m_Sensor.Open();

            m_FrameReader = m_Sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color |
                                             FrameSourceTypes.Depth |
                                             FrameSourceTypes.Infrared |
                                             FrameSourceTypes.Body);
            m_FrameReader.MultiSourceFrameArrived += OnSourceFrameArrived;
        }
    }

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
        MultiSourceFrame multiFrame = m_FrameReader.AcquireLatestFrame();
        if(null != multiFrame)
        {
            BodyFrame bodyFrame = multiFrame.BodyFrameReference.AcquireFrame();
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

    private void OnDestroy()
    {
        if(null != m_Sensor)
        {
            m_Sensor.Close();
        }
    }
}
