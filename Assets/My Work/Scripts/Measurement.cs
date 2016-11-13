using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Measurement : MonoBehaviour {
	private double height;
	private double width;
	private double length;

	public Measurement(double height, double width, double length){
		this.height = height;
		this.width = width;
		this.length = length;
	}

	public double getHeight(){
		return height;
	}
	public double getWidth(){
		return width;
	}
	public double getLength(){
		return length;
	}
	public void setHeight(double height){
		this.height = height;
	}
	public void setWidth(double width){
		this.width = width;
	}
	public void setLength(double length){
		this.length = length;
	}
}
