using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchandise : MonoBehaviour {

	private string category;
	private string subCategory;
	private string name;
	private string description;
	private double price;
	private string brand;
	private Measurement measurement;

	public Merchandise(string category, string subCategory, string name,
		string description, double price, string brand, double height, double width,
		double length){
		this.category = category;
		this.subCategory = subCategory;
		this.name = name;
		this.description = description;
		this.price = price;
		this.brand = brand;
		this.measurement = new Measurement(height,width,length);
    }

	public string getCategory(){
		return category;
	}
	public string getSubCategory(){
		return subCategory;
	}
	public string getName(){
		return name;
	}
	public string getDescription(){
		return description;
	}
	public double getPrice(){
		return price;
	}
	public string getBrand(){
		return brand;
	}
	public Measurement getMeasurement(){
		return measurement;
	}

	public void setCategory(string category){
		this.category = category;
	}
	public void setSubCategory(string subCategory){
		this.subCategory = subCategory;
	}
	public void setName(string name){
		this.name = name;
	}
	public void setDescription(string description){
		this.description = description;
	}
	public void setPrice(double price){
		this.price = price;
	}
	public void setBrand(string brand){
		this.brand = brand;
	}
	public void setMeasurement(Measurement measurement){
		this.measurement = measurement;
	}


	public string ToString(){
		return "Category: " + category + "\n" +
		"Sub Category: " + subCategory + "\n" +
		"Name: " + name + "\n" +
		"Description: " + description + "\n" +
		"Price: " + price + "\n" +
		"Brand: " + brand + "\n" +
		"Measurement: " + measurement.getHeight () + " x " +
		measurement.getWidth () + " x " + measurement.getLength ();
	}

		
}
