using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class jsonDataclass{
	public List<IdList> acList;
	public string src;
}
[Serializable]
public class img_data
{
	public string status;
	public string count; 
	public List<img_list> data;
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
	public string Mil;
	public int  Alt; // feet
	public float Trak; //direction
	public float  Spd; //km/h
	public float  Lat; //cor
	public float  Long;//cor
	public GameObject plane; //plane 
}
[Serializable]
public class img_list
{
	public string image;
	public string link;
	public string photographer; //img
}