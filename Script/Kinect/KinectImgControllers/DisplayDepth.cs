using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
//[RequireComponent(typeof(Renderer))]
public class DisplayDepth : MonoBehaviour {
	SkeletonWrapper sw;
	public GameObject pointman;
	public bool track_skeleton=false;
	public short[] depth_ar;
	public GameObject left, right;
	public DepthWrapper dw;
	public int count=80;	
	public KinectSensor ks;
	private Texture2D tex;
	int depth;
	public DeviceOrEmulator devOrEmu;
	private Kinect.KinectInterface kinect;
	public bool R_motor=false,  L_motor=false;
	public int flag=1;
	// Use this for initialization
	public static SerialPort sp=new SerialPort("COM5",9600);
	public GameObject hello;
	public GameObject shahrukh;



	void Start () {
		
		sw = new SkeletonWrapper ();
		openconnection ();
		kinect = devOrEmu.getKinect();
		StartCoroutine("SetGuard");
		tex = new Texture2D(320,240,TextureFormat.ARGB32,false);
		GetComponent<Renderer>().material.mainTexture = tex;
	}




	IEnumerator SetGuard()
	{

		while (true) {
			
			yield return new WaitForSeconds (2f);
			//Debug.Log ("enter While");





			while (count < 76000) {



				if (dw.depthImg [count] < 1000) {	
					if (calarea (count, 0, 160)) {
						
						Debug.Log ("Break1:  " + count);
						flag = 1;
						break;

					}
				} else {
					flag = 0;
				}



				count += 160;

				if (dw.depthImg [count] < 1000) {
					if (calarea (count, 160, 320)) {

						flag = 1;
						break;
					}
				}
				else
				{
					flag = 0;
				}

				count += 160;

			}
			count = 80;
			if (flag == 1) {
				pointman.gameObject.SetActive (false);
			} 
			else if (pointman.activeSelf == true) {

			} 

		
		}

	}










	// Update is called once per frame
	void Update () {

		if (dw.pollDepth())
		{
			tex.SetPixels32(convertDepthToColor(dw.depthImg));
			//tex.SetPixels32(convertPlayersToCutout(dw.segmentations));
			tex.Apply(false);
		}
	}

	private Color32[] convertDepthToColor(short[] depthBuf)
	{
		Color32[] img = new Color32[depthBuf.Length];
		for (int pix = 0; pix < depthBuf.Length; pix++)
		{

			img[pix].r = (byte)(depthBuf[pix] / 32);
			img[pix].g = (byte)(depthBuf[pix] / 32);
			img[pix].b = (byte)(depthBuf[pix] / 32);
		}
		return img;
	}

	private Color32[] convertPlayersToCutout(bool[,] players)
	{
		Color32[] img = new Color32[320*240];
		for (int pix = 0; pix < 320*240; pix++)
		{
			if(players[0,pix]|players[1,pix]|players[2,pix]|players[3,pix]|players[4,pix]|players[5,pix])
			{
				img[pix].a = (byte)255;
			} else {
				img[pix].a = (byte)0;
			}
		}
		return img;
	}

	/*	bool checkobject(int pixel)
	{

		int hor, ver,area;
		int h_temp1 = pixel;
		int h_temp = pixel;
		int v_temp = pixel;
		int v_temp1 = 0;
		while (dw.depthImg[h_temp]<1000 && h_temp%320!=0) {

			h_temp+=10;

		}
		while (dw.depthImg[h_temp1]<1000 && h_temp1%320!=0) {

			h_temp1-=10;

		}
		hor = h_temp - h_temp1;

		for (v_temp=pixel;  dw.depthImg[v_temp]<1000 && v_temp> v_temp%320; v_temp -= 240) {
			v_temp1++;
		}

		for (v_temp=pixel;  dw.depthImg[v_temp]<1000 && v_temp< 76480+(v_temp%320); v_temp += 240) {
			v_temp1++;
		}

		area = hor * v_temp1;

		if (area > 30000) {
		//	count = 80;
			checkposition (pixel, v_temp1);
			return true;

			//Debug.Log ("Object found");
		} else {
			return false;
		}

	}

	public void checkposition (int pixel,int v_temp1)
	{	
		int pixel_mod = pixel % 320;

		if (pixel_mod > 0 && pixel_mod< 160)
		{
			calarea (pixel,0 , 160, v_temp1);
		} 
		else if (pixel_mod> 160 && pixel_mod < 320)
		{
			calarea (pixel,160,320, v_temp1);
		} 

	


	}*/
	public bool calarea(int pixel,int start,int end)
	{

		flag = 1;
		int h_temp = pixel;
		int h_temp1 = pixel;
		int hor=0,area;
		int v_temp = pixel;
		int v_temp1 = 0;
		while (dw.depthImg[h_temp]<1000 && (h_temp%160)!=0) {

			h_temp+=10;
		}
		while (dw.depthImg[h_temp1]<1000 && (h_temp1%160)!=0) {

			h_temp1-=10;

		}
		for (v_temp=pixel;  dw.depthImg[v_temp]<1000 && v_temp> v_temp%320; v_temp -= 320) {
			v_temp1++;
		}

		for (v_temp=pixel;  dw.depthImg[v_temp]<1000 && v_temp< 76480+(v_temp%320); v_temp += 320) {
			v_temp1++;
		}
		hor = h_temp - h_temp1;
		area = hor * v_temp1;
		//Debug.Log ("h_temp:  " + h_temp);
		//Debug.Log ("h_temp1:  " + h_temp1);
		//Debug.Log ("v_temp1:  " + v_temp1);


		if (area > 25000) {
			enablemotor (start);
			Debug.Log ("Starting area:   " + start);
			return true;
		}
		else {
		sp.Write ("d");
			sp.Write ("c");
			return false;
		}



	}

	/*void disablemotor(int index)
	{

		if(index==0)
		{
			L_motor = false;	
		}
		else if (index == 160)
		{
			R_motor = false;
		}
	}
*/
	void enablemotor(int index)
	{

		if(index==0)
		{
			sp.Write ("a");

			sp.Write ("d");
			right.GetComponent<AudioSource>().Play ();
			L_motor = false;	
		}
		else if (index == 160)
		{
		sp.Write ("b");
		sp.Write ("c");

			left.GetComponent<AudioSource>().Play ();

			R_motor = false;
		}
	}

public void openconnection()
	{
		if (sp != null) {
			if (sp.IsOpen) {
				sp.Close ();
				Debug.Log ("close port ");
			} else {
				sp.Open ();
				sp.ReadTimeout = 16;
				Debug.Log ("Port opened");

			}

		}
	}


}
