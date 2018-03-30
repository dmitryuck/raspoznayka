using UnityEngine;

using System.Collections;

public class GameWnd : ScreenWnd {

	public GameObject col_sel_btn;

	public GameObject timer_pan;
	public GameObject scores_pan;
	public GameObject figure_pan;

	public GameObject fig_icon;

	public RectText timer;
	public RectText scores;

	// Авейк
	void Awake () {
		wnd_alpha = 0f;

		base.Awake();
		
		RectSize wnd_size = new RectSize (800, 600);
		
		this.set_size (wnd_size);
		
		float x_pos = (Screen.width - wnd_size.width) / 2;
		float y_pos = (Screen.height - wnd_size.height) / 2;
		
		this.set_pos (new Vector2 (x_pos, y_pos));
		
		add_col_sel_btn();
		add_figure_pan();
		add_timer_pan();
		add_scores_pan();
	}

	// Добавить очко
	public void add_score(){
		Scene scn = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scene>();
		scn.scores_num += 1;
		
		scores.set_text("Scores: " + scn.scores_num);
	}

	// Установить значение времени
	public void set_timer_time(string name){
		timer.set_text(name);
	}

	// Установить иконку для фигуры
	public void set_fig_icon(string name){
		RectImage fig_icon_comp = fig_icon.GetComponent<RectImage>();
		fig_icon_comp.set_sprite(name);
	}

	// Добавление панели таймера
	public void add_timer_pan(){
		timer_pan = add_child(typeof(RectText), "Timer");
		timer = timer_pan.GetComponent<RectText>();
		timer.set_size(new RectSize(240, 40));
		timer.set_pos(new Vector2(10, 10));
		timer.set_style(FontStyle.Bold);
		timer.set_font("Arial.ttf");
		timer.set_font_size(24);
		timer.set_color(Funcs.hex_to_col("#6FFF0BFF"));
		timer.set_text("Timer: 0");
	}

	// Добавление панели очков
	public void add_scores_pan(){
		scores_pan = add_child(typeof(RectText), "Scores");
		scores = scores_pan.GetComponent<RectText>();
		scores.set_size(new RectSize(240, 40));
		scores.set_pos(new Vector2(10, 50));
		scores.set_style(FontStyle.Bold);
		scores.set_font("Arial.ttf");
		scores.set_font_size(24);
		scores.set_color(Funcs.hex_to_col("#6FFF0BFF"));
		scores.set_text("Scores: 0");
	}

	// Добавление панели фигуры
	public void add_figure_pan(){
		figure_pan = add_child(typeof(RectImage), "FigurePan");
		RectImage figure_pan_comp = figure_pan.GetComponent<RectImage>();
		figure_pan_comp.set_size(new RectSize(256, 256));
		figure_pan_comp.set_pos(new Vector2(get_size().width/2-128, -30));
		figure_pan_comp.set_sprite("figure_frame");

		fig_icon = figure_pan_comp.add_child(typeof(RectImage), "FigIcon");
		RectImage fig_icon_comp = fig_icon.GetComponent<RectImage>();
		fig_icon_comp.set_size(new RectSize(78, 64));
		fig_icon_comp.set_pos(new Vector2(89, 90));
	}

	// Добавление кнопки выбора цвета
	public void add_col_sel_btn(){
		col_sel_btn = add_child(typeof(RectButton), "SelColorBtn");
		RectButton col_sel_comp = col_sel_btn.GetComponent<RectButton>();
		col_sel_comp.set_size(new RectSize(128, 85));
		col_sel_comp.set_pos(new Vector2(0, get_size().height - 64 - 40));
		col_sel_comp.set_icon(Scene.get_sprite("col_sel_btn"));

		col_sel_comp.on_mouse_enter = ()=>{
			col_sel_comp.set_icon(Scene.get_sprite("col_sel_enter"));
		};

		col_sel_comp.on_mouse_exit = ()=>{
			col_sel_comp.set_icon(Scene.get_sprite("col_sel_btn"));
		};

		col_sel_comp.btn.onClick.AddListener(()=>{
			StartCoroutine(Funcs.play_sound(gameObject, "btn_click"));

			if(Display.color_wnd == null){
				Display.color_wnd_open();
			} else Display.color_wnd_close();
		});
	}
}
