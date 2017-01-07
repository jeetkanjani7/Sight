using UnityEngine;
using System.Collections;

public class corutine : MonoBehaviour {
	void Awake()
	{
		StartCoroutine("SetGuard");
	}

	IEnumerator SetGuard()
	{
		while(true)
		{
			yield return new WaitForSeconds(5);
			Debug.Log("Sending");
		}
	}
}