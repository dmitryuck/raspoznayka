using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System;
using System.Collections;
using System.Collections.Generic;

public class Display {

	public static GameObject game_wnd;
	public static GameObject menu_wnd;
	public static GameObject color_wnd;
	public static GameObject stats_wnd;

	public static GameObject msg_box;

	public static List<GameObject> wnds;	


	// Инициализация дисплея
	public static void init(){
		if (wnds == null)
			wnds = new List<GameObject> ();
	}

	// Открыть окно
	public static GameObject show_wnd (Type wnd_type){
		GameObject wnd = (GameObject)ScreenWnd.init (wnd_type);
		
		wnds.Add (wnd);

		return wnd;
	}

	// Закрыть окно
	public static void close_wnd(GameObject wnd){
		foreach(GameObject curr_wnd in wnds){
			if(curr_wnd == wnd){
				wnds.Remove(curr_wnd);

				MonoBehaviour.Destroy (wnd);
				
				wnd = null;
				
				break;
			}
		}
	}

	// Открыто ли хоть одно окно
	public static bool check_wnd(){
		return wnds.Count > 0 ? true : false;
	}

	// Закрыть все окна
	public static void close_all_wnds(){
		while(wnds.Count > 0) {			
			close_wnd(wnds[0]);
		}
	}

	public static void game_wnd_open(){
		if (game_wnd == null) {
			game_wnd = show_wnd(typeof(GameWnd));
		}
	}

	public static void game_wnd_close(){
		if (game_wnd != null) {
			close_wnd(game_wnd);
		}
	}

	public static void menu_wnd_open(){
		if(menu_wnd == null){
			menu_wnd = show_wnd(typeof(MenuWnd));
		}
	}

	public static void menu_wnd_close(){
		if(menu_wnd != null){
			close_wnd(menu_wnd);
		}
	}

	public static void stats_wnd_open(){
		if(stats_wnd == null){
			stats_wnd = show_wnd(typeof(StatsWnd));
		}
	}

	public static void stats_wnd_close(){
		if(stats_wnd != null){
			close_wnd(stats_wnd);
		}
	}

	public static void color_wnd_open(){
		if(color_wnd == null){
			color_wnd = show_wnd(typeof(ColorWnd));
		}
	}

	public static void color_wnd_close(){
		if(color_wnd != null){
			close_wnd(color_wnd);
		}
	}
}
