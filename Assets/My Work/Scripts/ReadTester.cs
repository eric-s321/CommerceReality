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
		//	Debug.Log(merchs[i].ToString());
			dataLog += merchs[i].ToString() + "\n";
		}
		Debug.Log (dataLog);
	}

}
