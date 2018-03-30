using UnityEngine;

using System.Collections;	


public class Figure {

	public string[] figures = new string[]{"TriangLeft", "TriangRight", "TriangTop", "TriangDown", "Rectangle"};

	public string fig_icon;
	public string div_plane;

	public int[] quadrant;

	public Figure(){
		string figure = figures[UnityEngine.Random.Range(0, 5)];
		
		fig_icon = figure;
		
		switch(figure){
		case "TriangLeft":
			div_plane = "vert";
			quadrant = new int[]{0,1};
			break;
			
		case "TriangRight":
			div_plane = "vert";
			quadrant = new int[]{1,0};
			break;
			
		case "TriangTop":
			div_plane = "hor";
			quadrant = new int[]{0,1};
			break;
			
		case "TriangDown":
			div_plane = "hor";
			quadrant = new int[]{1,0};
			break;
			
		case "Rectangle":
			div_plane = "quad";
			quadrant = new int[]{1,1};
			break;
		}
	}
}
