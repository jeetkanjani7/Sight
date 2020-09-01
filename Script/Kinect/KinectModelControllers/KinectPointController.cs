/*
 * KinectModelController.cs - Moves every 'bone' given to match
 * 				the position of the corresponding bone given by
 * 				the kinect. Useful for viewing the point tracking
 * 				in 3D.
 * 
 * 		Developed by Peter Kinney -- 6/30/2011
 * 
 */

using UnityEngine;
using System;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class KinectPointController : MonoBehaviour {
	public bool detect=false;
	public GameObject DepthObject;
	public GameObject hello;
	public GameObject shahrukh;
	public GameObject namaste;
	public GameObject detectobject;
	public GameObject deinit;
	bool flag=true;
	bool flag1=true;
	//Assignments for a bitmask to control which bones to look at and which to ignore
	public enum BoneMask
	{
		None = 0x0,
		Hip_Center = 0x1,
		Spine = 0x2,
		Shoulder_Center = 0x4,
		Head = 0x8,
		Shoulder_Left = 0x10,
		Elbow_Left = 0x20,
		Wrist_Left = 0x40,
		Hand_Left = 0x80,
		Shoulder_Right = 0x100,
		Elbow_Right = 0x200,
		Wrist_Right = 0x400,
		Hand_Right = 0x800,
		Hip_Left = 0x1000,
		Knee_Left = 0x2000,
		Ankle_Left = 0x4000,
		Foot_Left = 0x8000,
		Hip_Right = 0x10000,
		Knee_Right = 0x20000,
		Ankle_Right = 0x40000,
		Foot_Right = 0x80000,
		All = 0xFFFFF,
		Torso = 0x10000F, //the leading bit is used to force the ordering in the editor
		Left_Arm = 0x1000F0,
		Right_Arm = 0x100F00,
		Left_Leg = 0x10F000,
		Right_Leg = 0x1F0000,
		R_Arm_Chest = Right_Arm | Spine,
		No_Feet = All & ~(Foot_Left | Foot_Right),
		UpperBody = Shoulder_Center | Head|Shoulder_Left | Elbow_Left | Wrist_Left | Hand_Left|
		Shoulder_Right | Elbow_Right | Wrist_Right | Hand_Right
		
	}
	
	public SkeletonWrapper sw;
	public DisplayDepth dd;
	public GameObject Hip_Center;
	public GameObject Spine;
	public GameObject Shoulder_Center;
	public GameObject Head;
	public GameObject Shoulder_Left;
	public GameObject Elbow_Left;
	public GameObject Wrist_Left;
	public GameObject Hand_Left;
	public GameObject Shoulder_Right;
	public GameObject Elbow_Right;
	public GameObject Wrist_Right;
	public GameObject Hand_Right;
	public GameObject Hip_Left;
	public GameObject Knee_Left;
	public GameObject Ankle_Left;
	public GameObject Foot_Left;
	public GameObject Hip_Right;
	public GameObject Knee_Right;
	public GameObject Ankle_Right;
	public GameObject Foot_Right;
	
	private GameObject[] _bones; //internal handle for the bones of the model
	//private Vector4[] _bonePos; //internal handle for the bone positions from the kinect
	
	public int player;
	public BoneMask Mask = BoneMask.All;
	 
	public float scale = 1.0f;

	// Use this for initialization
	void Start () {
		Debug.Log ("Kinect Point Controller Active");
		DepthObject=GameObject.Find ("DepthImagePlane");
		if (DepthObject.GetComponent("FlagPROJECT" +
			"") != null) {

 			Debug.Log ("Depth object working");
		}
	
		if (dd == null) {
			Debug.Log ("Display depth not found");
		}
		detectobject = GameObject.Find ("PersonDetected");
		namaste = GameObject.Find ("Namaste");
		shahrukh = GameObject.Find ("Shah_Rukh");
		hello = GameObject.Find ("Hello_voice");
		deinit = GameObject.Find ("PersonDeinit");
		if (hello == null) {
			Debug.Log ("Could not find hello" );
		}
	//	AudioSource audio=GetComponent<AudioSource>();
	//	audio.Play ();
		//store bones in a list for easier access
		_bones = new GameObject[(int)Kinect.NuiSkeletonPositionIndex.Count] {Hip_Center, Spine, Shoulder_Center, Head,
			Shoulder_Left, Elbow_Left, Wrist_Left, Hand_Left,
			Shoulder_Right, Elbow_Right, Wrist_Right, Hand_Right,
			Hip_Left, Knee_Left, Ankle_Left, Foot_Left,
			Hip_Right, Knee_Right, Ankle_Right, Foot_Right};
		//_bonePos = new Vector4[(int)BoneIndex.Num_Bones];

		StartCoroutine ("initskel");

		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Kinect Point Controller Update Active");
		if (detect == true) {
			StartCoroutine ("WaveSegments");
			//StartCoroutine ("Armextension");
			StartCoroutine ("Namaste");
			if (detect == false) {
				yield return new WaitForSeconds (5);
			}
			StartCoroutine ("deinitskel");
			if (detect == false) {
				yield return new WaitForSeconds (5);
			}
		} else {
			
			StartCoroutine ("initskel");
			if (detect == true) {
				yield return new WaitForSeconds (5);
			}
		}
		if(player == -1)
			return;
		//update all of the bones positions
		if (sw.pollSkeleton())
		{
			
			for (int ii = 0; ii < (int)Kinect.NuiSkeletonPositionIndex.Count; ii++) {
				
				//_bonePos[ii] = sw.getBonePos(ii);
				if (((uint)Mask & (uint)(1 << ii)) > 0) {
					

					//_bones[ii].transform.localPosition = sw.bonePos[player,ii];
					_bones [ii].transform.localPosition = new Vector3 (
						sw.bonePos [player, ii].x * scale,
						sw.bonePos [player, ii].y * scale,
						sw.bonePos [player, ii].z * scale);
					
				}
			}
		}
	}
	IEnumerator WaveSegments()
	{
			
		flag = true;
		while (flag) {	
			
			Debug.Log ("enter while of wave");
			
			yield return new WaitForSeconds (1f);
		/*	for (int player = 0; player < Kinect.Constants.NuiSkeletonCount; player++) {
				if (dw.players [0] ==true ) {
					flag = 1;
				}
			}*/
		//flag=skeleton.GetComponent<SkeletonWrapper> ().trackedPlayers[0];
		//Debug.Log ("plyer: " + dw.players [0]);
		//Debug.Log("tracked player"+flag);

		//	Debug.Log ("Enter Control");
		if (Wrist_Right.transform.position != null) {

			// Hand above elbow
			if (Wrist_Right.transform.position.y > Elbow_Right.transform.position.y) {
				// Hand right of elbow
				if (Wrist_Right.transform.position.x > Elbow_Right.transform.position.x) {
					Debug.Log ("Part1Wave Occured");
					//	waveSegment1 = true;
					//	waveComplete = false;
				} 
			}
			
				if (Wrist_Right.transform.position.y > Elbow_Right.transform.position.y) {
					if (Wrist_Right.transform.position.x > Elbow_Right.transform.position.x && Wrist_Left.transform.position.y < Elbow_Left.transform.position.y) {
						//waveSegment1 = false;
						Debug.Log ("Part2 of wave");
							
						hello.GetComponent<AudioSource> ().Play ();
					
						Debug.Log ("You have Completed a Wave");
						
					} else {
						break;
					}
				}
						

						//	waveComplete = true;

					

					//	waveComplete = true;
				 else {

				}
			


		}




	}
	}


	IEnumerator Armextension()
	{
		flag1 = true;	
		while (flag1) {


			yield return new WaitForSeconds (1);
			/*	for (int player = 0; player < Kinect.Constants.NuiSkeletonCount; player++) {
				if (dw.players [0] ==true ) {
					flag = 1;
				}
			}*/
			//flag=skeleton.GetComponent<SkeletonWrapper> ().trackedPlayers[0];
			//Debug.Log ("plyer: " + dw.players [0]);
			//Debug.Log("tracked player"+flag);

			//	Debug.Log ("Enter Control");
			if (Wrist_Right.transform.position != null) {

				// Hand above elbow
				if (Wrist_Right.transform.position.y> Elbow_Right.transform.position.y) {
					// Hand right of elbow
					if (Wrist_Left.transform.position.y >Elbow_Left.transform.position.y) {
						
							
								shahrukh.GetComponent<AudioSource> ().Play ();
					
							Debug.Log ("Sharukh khan");

						//	waveSegment1 = true;
						//	waveComplete = false;
					} else {
						//waveSegment1 = false;
					}
				}


				//	waveComplete = true;



				//	waveComplete = true;



			}




		}
	}


	IEnumerator Namaste()
	{
		flag1 = true;	
		while (flag1) {


			yield return new WaitForSeconds (1);

			if (Wrist_Right.transform.position != null) {

		
				if (Wrist_Right.transform.position.y > Elbow_Right.transform.position.y && Wrist_Left.transform.position.y > Elbow_Left.transform.position.y) {
			
					if (Wrist_Right.transform.position.x < Elbow_Right.transform.position.x && Wrist_Left.transform.position.x > Elbow_Left.transform.position.x) {
						Debug.Log ("Namaste");
					

						namaste.GetComponent<AudioSource> ().Play();
					} else {
						break;
					}
				
				} else {
					break;
				}


			} else {
				break;
			}




		}
	}


	IEnumerator initskel()
	{
		flag1 = true;	
		while (true) {

			Debug.Log ("initskel");
			yield return new WaitForSeconds (1f);
			/*	for (int player = 0; player < Kinect.Constants.NuiSkeletonCount; player++) {
				if (dw.players [0] ==true ) {
					flag = 1;
				}
			}*/
			//flag=skeleton.GetComponent<SkeletonWrapper> ().trackedPlayers[0];
			//Debug.Log ("plyer: " + dw.players [0]);
			//Debug.Log("tracked player"+flag);

			//	Debug.Log ("Enter Control");
			if (Wrist_Right.transform.position != null) {
				Debug.Log ("wrist found");
				// Hand above elbow
			
				if (Wrist_Right.transform.position.y > Head.transform.position.y && Wrist_Left.transform.position.y > Head.transform.position.y) {

					if (detect == false) {
						detect = true;
						detectobject.GetComponent<AudioSource> ().Play ();

						Debug.Log ("Person Detected");
					}
					//	waveSegment1 = true;
					//	waveComplete = false;
				}

			}
		




		}
	}




	IEnumerator deinitskel()
	{
		flag1 = true;	
		while (flag1) {


			yield return new WaitForSeconds (2);
			/*	for (int player = 0; player < Kinect.Constants.NuiSkeletonCount; player++) {
				if (dw.players [0] ==true ) {
					flag = 1;
				}
			}*/
			//flag = skeleton.GetComponent<SkeletonWrapper> ().trackedPlayers[0];
			//Debug.Log ("plyer: " + dw.players [0]);
			//Debug.Log("tracked player"+flag);

			//	Debug.Log ("Enter Control");
			if (Wrist_Right.transform.position != null) {

				// Hand above elbow

				if (Wrist_Right.transform.position.y > Head.transform.position.y && Wrist_Left.transform.position.y > Head.transform.position.y) {

						
					deinit.GetComponent<AudioSource> ().Play ();

					Debug.Log ("Person Deinitialized");
					detect = false;
					//	waveSegment1 = true;
					//	waveComplete = false;
				} else {
					detect = true;
				}


			}




		}
	}
}
