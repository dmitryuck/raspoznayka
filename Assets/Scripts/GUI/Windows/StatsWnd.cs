using UnityEngine;

using System.Collections;

public class StatsWnd : ScreenWnd {

	public GameObject wnd_bg;

	public float btn_alpha = 0.0009f;

	public GameObject scores_pan;
	public GameObject respawn_btn;

	void Awake(){
		wnd_alpha = 0f;
		
		base.Awake();
		
		RectSize wnd_size = new RectSize (300, 300);
		
		this.set_size (wnd_size);
		
		float x_pos = (Screen.width - wnd_size.width) / 2;
		float y_pos = (Screen.height - wnd_size.height) / 2;
		
		this.set_pos (new Vector2 (x_pos, y_pos));

		add_wnd_bg();

		Destroy(sound_btn);

		add_scores_pan();
		add_respawn_btn();
	}

	// Добавить фон к окну
	public void add_wnd_bg(){
		wnd_bg = add_child(typeof(RectImage));
		RectImage wnd_bg_comp = wnd_bg.GetComponent<RectImage>();
		wnd_bg_comp.set_size(new RectSize(get_size().width, get_size().height));
		wnd_bg_comp.set_pos(new Vector2(0, 0));
		wnd_bg_comp.set_sprite("stats_bg");
	}

	// Панель вывода счета
	public void add_scores_pan(){
		scores_pan = add_child(typeof(RectText), "Scores");
		RectText scores = scores_pan.GetComponent<RectText>();
		scores.set_size(new RectSize(160, 40));
		scores.set_pos(new Vector2(90, 56));
		scores.set_style(FontStyle.Bold);
		scores.set_font("Arial.ttf");
		scores.set_font_size(24);
		scores.set_color(Funcs.hex_to_col("#6FFF0BFF"));

		Scene scn = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scene>();
		scores.set_text("Scores: " + scn.scores_num);
	}

	// Кнопка играть снова
	public void add_respawn_btn(){
		respawn_btn = add_child(typeof(RectButton));
		RectButton respawn_btn_comp = respawn_btn.GetComponent<RectButton>();
		respawn_btn_comp.set_size(new RectSize(160, 47.8f));
		respawn_btn_comp.set_pos(new Vector2(71.7f, 201.6f));
		respawn_btn_comp.btn_bg_alpha = btn_alpha;
		respawn_btn_comp.add_image(new Color(255, 255, 255));
		
		respawn_btn_comp.on_mouse_down = ()=>{
			StartCoroutine(Funcs.play_sound(gameObject.transform.parent.gameObject, "btn_click"));
		};
		
		respawn_btn_comp.btn.onClick.AddListener(()=>{
			Display.stats_wnd_close();
			
			Scene scn = scene_obj.GetComponent<Scene>();
			scn.is_play = true;
		});
	}
}
