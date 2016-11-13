using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarGameController : MonoBehaviour {

    public List<Merchandise> SedanList = new List<Merchandise>();
    public List<Merchandise> SportsCarList = new List<Merchandise>();
    public List<Merchandise> TruckList = new List<Merchandise>();

    public CarButtonList[] SedanButtons;
    public CarButtonList[] SportsCarButtons;
    public CarButtonList[] TruckButtons;

    /*void Awake () {
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
        for (int i = 0; i < SedanButtons.Length; i++) {
            SedanButtons[i].GetComponent<CarButtonList>().myMerchandise = SedanList[i];
        }
    }*/




}
