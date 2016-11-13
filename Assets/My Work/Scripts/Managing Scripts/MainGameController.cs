using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour {

	public enum UIState {
        MainMenu,
        IntermediateShop,
        Settings,
        ViewAllSavedObjects,
        ViewIndividualObject
    };
    public UIState uistate;

    public void BackClicked () {
        if (uistate == UIState.IntermediateShop) {

        }
        else if (uistate == UIState.Settings) {

        }
        else if (uistate == UIState.ViewAllSavedObjects) {

        }
        else if (uistate == UIState.ViewIndividualObject) {

        }
    }
    public void SettingsClicked () {

    }
}
