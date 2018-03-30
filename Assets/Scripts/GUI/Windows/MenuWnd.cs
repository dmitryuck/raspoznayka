using UnityEngine;

using System.Collections;

public class MenuWnd : ScreenWnd {

	public GameObject start_btn;

	void Awake(){
		wnd_alpha = 0f;
		
		base.Awake();
		
		RectSize wnd_size = new RectSize (800, 600);
		
		this.set_size (wnd_size);
		
		float x_pos = (Screen.width - wnd_size.width) / 2;
		float y_pos = (Screen.height - wnd_size.height) / 2;
		
		this.set_pos (new Vector2 (x_pos, y_pos));

		add_start_btn();

		Destroy(sound_btn);
	}

	// Добавление кнопки Старт
	public void add_start_btn(){
		start_btn = add_child(typeof(RectButton), "StartBtn");
		RectButton start_btn_comp = start_btn.GetComponent<RectButton>();
		start_btn_comp.set_size(new RectSize(256, 211));
		start_btn_comp.set_pos(new Vector2(get_size().width/2 - 128, get_size().height - 180));
		start_btn_comp.set_icon(Scene.get_sprite("start_btn"));
		
		start_btn_comp.on_mouse_down = ()=>{
			start_btn_comp.set_icon(Scene.get_sprite("start_btn_down"));
			
			StartCoroutine(Funcs.play_sound(gameObject, "btn_click"));
		};
		
		start_btn_comp.btn.onClick.AddListener(()=>{
			Display.menu_wnd_close();

			Scene scn = scene_obj.GetComponent<Scene>();
			scn.is_play = true;
		});
	}
}
