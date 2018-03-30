using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;

public class ScreenWnd : RectComponent {

	public float wnd_alpha = 0.5f;
	public string wnd_color = "#000000";
	
	public GameObject scene_obj;

	public GameObject sound_btn;

	// Инициализация окна
	public static GameObject init(Type type){
		GameObject canvas = Funcs.get_cnv ();
		
		GameObject wnd = new GameObject(type.ToString ());
		wnd.transform.SetParent(canvas.transform);

		wnd.AddComponent(type);

		return wnd;
	}

	// Авейк окна
	public void Awake(){
		base.Awake ();
		
		Image i = gameObject.AddComponent<Image>();
		i.color = Funcs.hex_to_col (wnd_color, wnd_alpha);

		scene_obj = GameObject.FindGameObjectWithTag("GameController");

		add_sound_btn();
	}

	// Кнопка выкл музыки
	public void add_sound_btn(){
		sound_btn = add_child(typeof(RectButton));
		RectButton sound_btn_comp = sound_btn.GetComponent<RectButton>();
		sound_btn_comp.set_size(new RectSize(64, 64));
		sound_btn_comp.set_pos(new Vector2(720, 20));
		sound_btn_comp.set_icon(Scene.get_sprite("play_sound"));
		
		sound_btn_comp.btn.onClick.AddListener(()=>{
			StartCoroutine(Funcs.play_sound(gameObject, "btn_click"));
			
			Scene.toggle_music(scene_obj, "game_music");
		});
	}

	// Установить положение окна
	public override void set_pos(Vector2 pos, bool loc = false){
		base.set_pos (pos, false);
	}
}
