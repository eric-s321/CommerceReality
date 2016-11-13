using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainGameController : MonoBehaviour {

	public enum UIState {
        MainMenu,
        IntermediateShop,
        Settings,
        ViewAllShoppingCart,
        ViewIndividualObject
    };
    public UIState uistate;

    public Canvas MainMenuCanvas;
    public Canvas IntermediateCanvas;
    public Canvas SettingsCanvas;
    public Canvas CartCanvas;

    /*GameObject go = Instantiate(bob, transform.position, Quaternion.identity) as GameObject;
    go.name = "Bike";*/


    public void m_BackClicked () {
		IntermediateCanvas.gameObject.SetActive(false);
		MainMenuCanvas.gameObject.SetActive(true);
//        if (uistate == UIState.IntermediateShop) {
//            IntermediateCanvas.gameObject.SetActive(false);
//            MainMenuCanvas.gameObject.SetActive(true);
//        }
//        else if (uistate == UIState.Settings) {
//            SettingsCanvas.gameObject.SetActive(false);
//            MainMenuCanvas.gameObject.SetActive(true);
//        }
//        else if (uistate == UIState.ViewAllShoppingCart) {
//            CartCanvas.gameObject.SetActive(false);
//            MainMenuCanvas.gameObject.SetActive(true);
//        }
//        else if (uistate == UIState.ViewIndividualObject) {
//            // disappear individual object and fade in main menu in front of user
//        }
    }

    /// 
    /// MAIN MENU
    /// 
    public void m_SettingsClicked () {
        IntermediateCanvas.gameObject.SetActive(true);
        MainMenuCanvas.gameObject.SetActive(false);
    }
    public void m_ShopClicked() {
        //show the intermediate UI
        IntermediateCanvas.gameObject.SetActive(true);
        MainMenuCanvas.gameObject.SetActive(false);
        //hide the main menu

        //fade in the 
    }
    public void m_ScanClicked () {
        //Go to Sam's scene
    }
    public void m_ViewAllSavedObjects() {
        //Nothing right now
    }
    public void m_ViewIndividualObject () {
        //Nothing right now
    }


    /// 
    /// INTERMEDIATE
    /// 
    public void m_CarsClicked () {
        SceneManager.LoadScene("Car");
    }
    public void m_ElectronicsClicked () {

    }
    public void m_FurnitureClicked () {

    }

    /// 
    /// SETTINGS
    /// 

    /// 
    /// MAIN MENU
    /// 
}
