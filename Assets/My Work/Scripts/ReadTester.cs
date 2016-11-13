using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadTester : MonoBehaviour {

	// Use this for initialization
	void Start() {
		MerchandiseReader reader = new MerchandiseReader();
		List<Merchandise> merchs = reader.readAllInputData();
		string dataLog = "";
		for(int i = 0; i < merchs.Count; i++){
//			Debug.Log ("i is " + i);
//			Debug.Log ("Model object is: " + merchs [i].getModelObject());
			dataLog += merchs[i].ToString() + "\n";
		}
		Debug.Log (dataLog);
		Merchandise merch = reader.getMerchandiseByName ("BMW Z4");
		Debug.Log ("Merchandise returned is " + merch.ToString ());

	}
}
