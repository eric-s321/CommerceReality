#if WINDOWS_UWP
using Windows.Storage;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using System;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchandiseReader : MonoBehaviour {

	private List<Merchandise> merchList;

	public MerchandiseReader(){
		merchList = new List<Merchandise>();
	}

    public string GetFilePath(string fileName) {
        return System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
    }

    /* FILE TO READ FROM IS HARD CODED INTO HERE -- MUST MAKE CHANGES IN CODE OR PASS AS PARAMETER IN FUTURE */
    public List<Merchandise> readAllInputData(){
        //#if WINDOWS_UWP
        //       string currentDirectory = System.IO.Directory.GetCurrentDirectory();
        MerchandiseReader tempMerch = new MerchandiseReader();
        string filePath = tempMerch.GetFilePath("MerchandiseInfo - Sheet1.csv");
//		var reader = new System.IO.StreamReader(System.IO.File.OpenRead("../Commerce Reality/Assets/My Work/DataFiles/MerchandiseInfo - Sheet1.csv"));
        var reader = new System.IO.StreamReader(System.IO.File.OpenRead(filePath));
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
/*#else
        Task task = new Task(

            async () =>
            {                              
                StorageFile textFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Text.txt"));
                plainText = await FileIO.ReadTextAsync(textFile);

                txtTextMesh.text = plainText;

            });
        task.Start();
        task.Wait();

#endif*/
    }


    /* returns null if merchandise not found */
    public Merchandise getMerchandiseByName(string name){
		for (int i = 0; i < merchList.Count; i++) {
			if (merchList [i].getName ().Equals (name)) {
				return merchList [i];
			}
		}
		return null;
	}
}
