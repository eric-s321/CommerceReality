using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchandiseReader : MonoBehaviour {

	private List<Merchandise> merchList;

	public MerchandiseReader(){
		merchList = new List<Merchandise>();
	}


	/* FILE TO READ FROM IS HARD CODED INTO HERE -- MUST MAKE CHANGES IN CODE OR PASS AS PARAMETER IN FUTURE */
	public List<Merchandise> readAllInputData(){
		var reader = new System.IO.StreamReader(System.IO.File.OpenRead(@"/Users/ericscagnelli/Desktop/MerchandiseInfo - Sheet1.csv"));
		string line = reader.ReadLine();  //Skip first line in file (headers)
		while (!reader.EndOfStream){
			line = reader.ReadLine();
			string[] values = line.Split (',');
			string[] measurement = values[6].Split('x');
			try{
				double height = System.Convert.ToDouble(measurement[0]);
				double width = System.Convert.ToDouble(measurement [1]);
				double length = System.Convert.ToDouble(measurement [2]);
				Merchandise newMerch = new Merchandise(values[0], values[1], values[2],
					values[3], System.Convert.ToDouble(values[4]), values[5], height, width, length);
				merchList.Add (newMerch);
			}
			catch(System.FormatException e){
				Debug.Log ("Error parsing Measurement values to a double: ");
				foreach (var data in measurement) {
					Debug.Log (data);
				}
				
			}
		}
		return merchList;
	}
}
