using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarGameController : MonoBehaviour {

    private List<Merchandise> SedanList = new List<Merchandise>();
    private List<Merchandise> SportsCarList = new List<Merchandise>();
    private List<Merchandise> TruckList = new List<Merchandise>();

    private CarButtonList[] SedanButtons;
    private CarButtonList[] SportsCarButtons;
    private CarButtonList[] TruckButtons;

    void Awake () {
        MerchandiseReader reader = new MerchandiseReader();
        List<Merchandise> merchs = reader.readAllInputData();

        //Get car lists upon start
        for (int i=0; i < merchs.Count; i++) {
            if (merchs[i].getSubCategory().Equals("Sedan")) {
                SedanList.Add(merchs[i]);
	//			Debug.Log ("Adding Sedan: " + merchs [i].ToString ());
            }
        }
        for (int i = 0; i < merchs.Count; i++) {
            if (merchs[i].getSubCategory().Equals("Sports Car")) {
                SportsCarList.Add(merchs[i]);
            }
        }
        for (int i = 0; i < merchs.Count; i++) {
            if (merchs[i].getSubCategory().Equals("Truck")) {
                TruckList.Add(merchs[i]);
            }
        }
		Debug.Log ("Printing Sedan List");
		for (int i = 0; i < SedanList.Count; i++) {
			Debug.Log (SedanList [i].ToString());
		}

//        //populate all the buttonlists
//		SedanButtons = new CarButtonList[SedanList.Count];
//        for (int i = 0; i < SedanButtons.Length; i++) {
//           // SedanButtons[i].GetComponent<CarButtonList>().myMerchandise = SedanList[i];
//			SedanButtons[i].myMerchandise = SedanList[i];
//        }
    }




}
