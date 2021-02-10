using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class jsonDataclass{
	public List<IdList> acList;
	public string src;
}
[Serializable]
public class jsonDataImg
{
	public List<img_plane_api_hold> img;
	public string data;
}

[Serializable]
public class IdList{
	public string Id;
	public string Reg;
    public string Icao;
	public string Call;
	public string Type;//typas
	public string Mdl; //detalus typas
	public string From; //ið
	public string To; //i
	public string Op;//operatorius
	public int  Alt; // feet
	public float Trak; //direction
	public float  Spd; //km/h
	public float  Lat; //cor
	public float  Long;//cor
	public GameObject plane; //plane 
}
[Serializable]
public class img_plane_api_hold
{
	public string status;
	public string count;
	public string image;
	public string link;
	public GameObject main_img; //img
}