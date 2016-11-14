using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarGameController : MonoBehaviour {

    /*List<Merchandise> SedanList = new List<Merchandise>();
    List<Merchandise> SportsCarList = new List<Merchandise>();
    List<Merchandise> TruckList = new List<Merchandise>();

    public Button[] SedanButtons;
    public Button[] SportsCarButtons;
    public Button[] TruckButtons;

    void Awake() {
        MerchandiseReader reader = new MerchandiseReader();
        List<Merchandise> merchs = reader.readAllInputData();

        //Get car lists upon start
        for (int i = 0; i < merchs.Count; i++) {
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
        SedanButtons = new Button[SedanList.Count];
        for (int i = 0; i < SedanButtons.Length; i++) {
            Debug.Log("Position is " + SedanButtons[i].transform.position);
            SedanButtons[i].transform.Find("Text").GetComponent<Text>().text = "this car's name";
            GameObject newModel = Instantiate(Resources.Load("BMW Z4" + ".prefab", typeof(GameObject)) as GameObject, SedanButtons[i].transform.position, Quaternion.identity);



            //SedanButtons[i].GetComponent<CarButtonList>().myMerchandise = SedanList[i];
        }
    }*/

    public GameObject MainMenuCanvas;
    public GameObject largeViewing;
    public GameObject pedestalViewing;
    public Text Description;
    public Text Title;
    public Text Brand;
    public Text Price;
    private Merchandise merch;

	public GameObject BMWM3GTR;
	public GameObject BMWZ4;
	public GameObject FerrariCalifornia;
	public GameObject LamborghiniMurcielago;
	public GameObject MercedesBenzSL500;
	public GameObject MercedesCLKGTR;
	public GameObject Porsche911GT2;

    public GameObject FULLBMWM3GTR;
    public GameObject FULLBMWZ4;
    public GameObject FULLFerrariCalifornia;
    public GameObject FULLLamborghiniMurcielago;
    public GameObject FULLMercedesBenzSL500;
    public GameObject FULLMercedesCLKGTR;
    public GameObject FULLPorsche911GT2;

    public GameObject fullsizebutton;

    public string currentCar;

    public void ViewCar (string Car) {
        currentCar = Car;
        //move main menu down
        MainMenuCanvas.SetActive(false);
        fullsizebutton.SetActive(false);
        largeViewing.SetActive(false);
        pedestalViewing.SetActive(true);
        MerchandiseReader reader = new MerchandiseReader();
        List<Merchandise> merchs = reader.readAllInputData();

        Merchandise meme = reader.getMerchandiseByName(Car);
        Description.text = meme.getDescription();
        Brand.text = meme.getBrand();
        Price.text = meme.getPrice()+"";
        Title.text = meme.getName();

        // how to get merchendise type from car name? merch = 1; //

		switch (Car)
		{
		case "BMW M3 GTR":
			BMWM3GTR.SetActive (true);
			break;
		case "BMW Z4":
			BMWZ4.SetActive (true);
			break;
		case "Ferrari California":
			FerrariCalifornia.SetActive (true);
			break;
		case "Lamborghini Murcielago":
			LamborghiniMurcielago.SetActive (true);
			break;
		case "Mercedes Benz SL 500":
			MercedesBenzSL500.SetActive (true);
                fullsizebutton.SetActive(true);
			break;
		case "Mercedes CLK GTR":
			MercedesCLKGTR.SetActive (true);
			break;
		case "Porsche 911 GT2":
			Porsche911GT2.SetActive (true);
			break;
		default:
			break;
		}

        /*FULLBMWM3GTR.SetActive(false);
        FULLBMWZ4.SetActive(false);
        FULLFerrariCalifornia.SetActive(false);
        FULLLamborghiniMurcielago.SetActive(false);
        FULLMercedesBenzSL500.SetActive(false);
        FULLMercedesCLKGTR.SetActive(false);
        FULLPorsche911GT2.SetActive(false);*/
        FULLMercedesBenzSL500.SetActive(false);

    }

    public void BackButton () {
        currentCar = null;

        MainMenuCanvas.SetActive(true);
        pedestalViewing.SetActive(false);

		BMWM3GTR.SetActive (false);
		BMWZ4.SetActive (false);
		FerrariCalifornia.SetActive (false);
		LamborghiniMurcielago.SetActive (false);
		MercedesBenzSL500.SetActive (false);
		MercedesCLKGTR.SetActive (false);
		Porsche911GT2.SetActive (false);

        /*FULLBMWM3GTR.SetActive(false);
        FULLBMWZ4.SetActive(false);
        FULLFerrariCalifornia.SetActive(false);
        FULLLamborghiniMurcielago.SetActive(false);
        FULLMercedesBenzSL500.SetActive(false);
        FULLMercedesCLKGTR.SetActive(false);
        FULLPorsche911GT2.SetActive(false);*/
        FULLMercedesBenzSL500.SetActive(false);
    }

    public void FullSized () {
        //make all UI disappear
        MainMenuCanvas.SetActive(false);
        largeViewing.SetActive(true);
        pedestalViewing.SetActive(false);
        //make large switch change

        switch (currentCar) {
            case "BMW M3 GTR":
                FULLBMWM3GTR.SetActive(true);
                break;
            case "BMW Z4":
                FULLBMWZ4.SetActive(true);
                break;
            case "Ferrari California":
                FULLFerrariCalifornia.SetActive(true);
                break;
            case "Lamborghini Murcielago":
                FULLLamborghiniMurcielago.SetActive(true);
                break;
            case "Mercedes Benz SL 500":
                FULLMercedesBenzSL500.SetActive(true);
                break;
            case "Mercedes CLK GTR":
                FULLMercedesCLKGTR.SetActive(true);
                break;
            case "Porsche 911 GT2":
                FULLPorsche911GT2.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void Minimize() {
        ViewCar(currentCar);
    }

}
