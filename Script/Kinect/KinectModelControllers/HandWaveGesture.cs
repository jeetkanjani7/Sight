using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using Kinect;
using System.Collections;



public class HandWaveGesture : MonoBehaviour {

	SkeletonWrapper sw;
	public KinectModelControllerV2 control;

	public int flag=0;
//	public frameData Reader;
	//private BodyFrameReader Reader;
//	public Body[] bodies;
	private int bodyCount;
	public bool waveSegment1;
	public bool waveComplete;
	public DeviceOrEmulator devOrEmu;
	private Kinect.KinectInterface kinect;

	//private Kinect.KinectInterface kinect;

	// Use this for initialization

	void Start()
	{
	//	kinect = devOrEmu.getKinect();
	//	 sw=new SkeletonWrapper();
	//	skeleton=GameObject.Find("SkeletonWrapper");
	//	StartCoroutine("WaveSegments");
	}

	void OnApplicationQuit()
	{
	/*	if (Reader != null)
		{
			Reader.Dispose();
			Reader = null;
		}

		if (Sensor != null)
		{
			if (Sensor.IsOpen)
			{
				Sensor.Close();
			}

			Sensor = null;
		}*/
	}
	// Update is called once per frame
	void Update () 
	{
		/*if (Reader != null)
		{
			var frame = Reader.AcquireLatestFrame();
			if (frame != null)
			{
				if (bodies == null)
				{
					bodies = new Body[Sensor.BodyFrameSource.BodyCount];
				}

				frame.GetAndRefreshBodyData(bodies);

				frame.Dispose();
				frame = null;
			}
		}    */

	//	WaveSegments ();
	}



}
	
