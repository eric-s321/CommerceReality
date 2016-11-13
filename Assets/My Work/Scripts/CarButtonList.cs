using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarButtonList : MonoBehaviour {

    public GameObject myModel;
    private GameObject privateInstantiatedModel;
    public Merchandise myMerchandise;

//	public CarButtonList(){
//		myModel = new GameObject ();
//		privateInstantiatedModel = new GameObject ();
//		myMerchandise = new Merchandise ();
//	}

    /*void Start () {
        myModel = myMerchandise.getModelObject();
        privateInstantiatedModel = Instantiate(myModel, transform.position, Quaternion.identity);
        privateInstantiatedModel.transform.parent = gameObject.transform;
        privateInstantiatedModel.name = myMerchandise.getName() + " Model";
    }*/


}
