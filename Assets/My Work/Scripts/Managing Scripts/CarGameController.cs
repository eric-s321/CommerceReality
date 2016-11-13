using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarGameController : MonoBehaviour {

    List<Merchandise> SedanList = new List<Merchandise>();
    List<Merchandise> SportsCarList = new List<Merchandise>();
    List<Merchandise> TruckList = new List<Merchandise>();

    public Button[] SedanButtons;
    public Button[] SportsCarButtons;
    public Button[] TruckButtons;

   /* void Awake () {
        MerchandiseReader reader = new MerchandiseReader();
        List<Merchandise> merchs = reader.readAllInputData();

        //Get car lists upon start
        for (int i=0; i < merchs.Count; i++) {
            if (merchs[i].getSubCategory().Equals("Sedan")) {
                SedanList.Add(merchs[i]);
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

        //populate all the buttonlists
//        SedanButtons = new Button[SedanList.Count];
//        for (int i = 0; i < SedanButtons.Length; i++) {
//			Debug.Log ("Position is " + SedanButtons [i].transform.position);
//           SedanButtons[i].transform.Find("Text").GetComponent<Text>().text = "this car's name";
//            GameObject newModel = Instantiate(Resources.Load("BMW Z4" + ".prefab", typeof(GameObject)) as GameObject, SedanButtons[i].transform.position, Quaternion.identity);



            //SedanButtons[i].GetComponent<CarButtonList>().myMerchandise = SedanList[i];
        }
    }*/

//    public Text texting;
//
//    public void ViewCar (string CarName) {
//        //SedanButtons[i].transform.FindChild("Text").GetComponent<Text>().text = "this car's name";
//        GameObject newModel = Instantiate(Resources.Load("BMW Z4" + ".prefab", typeof(GameObject)) as GameObject, transform.position, Quaternion.identity);
//        texting.text = "string";
//    }


}
