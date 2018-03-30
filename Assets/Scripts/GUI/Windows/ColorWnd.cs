using UnityEngine;

using System;
using System.Collections;

public class ColorWnd : ScreenWnd {

	public GameObject wnd_bg;

	public float btn_alpha = 0.0009f;

	public GameObject blue_col;
	public GameObject red_col;
	public GameObject white_col;
	public GameObject yellow_col;
	public GameObject black_col;
	public GameObject green_col;

	// Авейк
	void Awake(){
		wnd_alpha = 0f;
		
		base.Awake();
		
		RectSize wnd_size = new RectSize (800, 600);
		
		this.set_size (wnd_size);
		
		float x_pos = (Screen.width - wnd_size.width) / 2;
		float y_pos = Screen.height - (wnd_size.height + 20);
		
		this.set_pos (new Vector2 (x_pos, y_pos));

		add_wnd_bg();
		add_col_btns();
	}

	// Добавление бекграунда
	public void add_wnd_bg(){
		wnd_bg = add_child(typeof(RectImage));
		RectImage wnd_bg_comp = wnd_bg.GetComponent<RectImage>();
		wnd_bg_comp.set_size(new RectSize(get_size().width, get_size().height));
		wnd_bg_comp.set_pos(new Vector2(0, 0));
		wnd_bg_comp.set_sprite("col_sel_bg");
	}

	// Добавление кнопок смены цвета
	public void add_col_btns(){
		blue_col = add_child(typeof(RectButton));
		RectButton blue_col_comp = blue_col.GetComponent<RectButton>();
		blue_col_comp.set_size(new RectSize(104, 74));
		blue_col_comp.set_pos(new Vector2(186, 200));
		blue_col_comp.btn_bg_alpha = btn_alpha;
		blue_col_comp.add_image(new Color(255, 255, 255));

		blue_col_comp.on_mouse_down = ()=>{
			StartCoroutine(Funcs.play_sound(gameObject.transform.parent.gameObject, "col_select"));
		};

		blue_col_comp.btn.onClick.AddListener(()=>{
			set_col(BrushCols.Blue);
		});

		red_col = add_child(typeof(RectButton));
		RectButton red_col_comp = red_col.GetComponent<RectButton>();
		red_col_comp.set_size(new RectSize(104, 74));
		red_col_comp.set_pos(new Vector2(268, 123));
		red_col_comp.btn_bg_alpha = btn_alpha;
		red_col_comp.add_image(new Color(255, 255, 255));

		red_col_comp.on_mouse_down = ()=>{
			StartCoroutine(Funcs.play_sound(gameObject.transform.parent.gameObject, "col_select"));
		};

		red_col_comp.btn.onClick.AddListener(()=>{
			set_col(BrushCols.Red);
		});

		white_col = add_child(typeof(RectButton));
		RectButton white_col_comp = white_col.GetComponent<RectButton>();
		white_col_comp.set_size(new RectSize(104, 74));
		white_col_comp.set_pos(new Vector2(377, 116));
		white_col_comp.btn_bg_alpha = btn_alpha;
		white_col_comp.add_image(new Color(255, 255, 255));

		white_col_comp.on_mouse_down = ()=>{
			StartCoroutine(Funcs.play_sound(gameObject.transform.parent.gameObject, "col_select"));
		};

		white_col_comp.btn.onClick.AddListener(()=>{
			set_col(BrushCols.White);
		});

		yellow_col = add_child(typeof(RectButton));
		RectButton yellow_col_comp = yellow_col.GetComponent<RectButton>();
		yellow_col_comp.set_size(new RectSize(104, 74));
		yellow_col_comp.set_pos(new Vector2(481, 136));
		yellow_col_comp.btn_bg_alpha = btn_alpha;
		yellow_col_comp.add_image(new Color(255, 255, 255));

		yellow_col_comp.on_mouse_down = ()=>{
			StartCoroutine(Funcs.play_sound(gameObject.transform.parent.gameObject, "col_select"));
		};

		yellow_col_comp.btn.onClick.AddListener(()=>{
			set_col(BrushCols.Yellow);
		});

		black_col = add_child(typeof(RectButton));
		RectButton black_col_comp = black_col.GetComponent<RectButton>();
		black_col_comp.set_size(new RectSize(104, 74));
		black_col_comp.set_pos(new Vector2(473, 262));
		black_col_comp.btn_bg_alpha = btn_alpha;
		black_col_comp.add_image(new Color(255, 255, 255));

		black_col_comp.on_mouse_down = ()=>{
			StartCoroutine(Funcs.play_sound(gameObject.transform.parent.gameObject, "col_select"));
		};

		black_col_comp.btn.onClick.AddListener(()=>{
			set_col(BrushCols.Black);
		});

		green_col = add_child(typeof(RectButton));
		RectButton green_col_comp = green_col.GetComponent<RectButton>();
		green_col_comp.set_size(new RectSize(104, 74));
		green_col_comp.set_pos(new Vector2(431, 373));
		green_col_comp.btn_bg_alpha = btn_alpha;
		green_col_comp.add_image(new Color(255, 255, 255));

		green_col_comp.on_mouse_down = ()=>{
			StartCoroutine(Funcs.play_sound(gameObject.transform.parent.gameObject, "col_select"));
		};

		green_col_comp.btn.onClick.AddListener(()=>{
			set_col(BrushCols.Green);
		});
	}

	// Установить цвет кисти
	public void set_col(BrushCols col){
		ParticleSystem part_sys;

		GameObject scn = GameObject.FindGameObjectWithTag("GameController");

		Scene scn_comp = scn.GetComponent<Scene>();

		Destroy(scn_comp.particle.gameObject);

		switch(col){
			case BrushCols.Blue:
				scn_comp.load_brush(BrushCols.Blue);
				break;

			case BrushCols.Red:
				scn_comp.load_brush(BrushCols.Red);
				break;

			case BrushCols.White:
				scn_comp.load_brush(BrushCols.White);
				break;

			case BrushCols.Yellow:
				scn_comp.load_brush(BrushCols.Yellow);
				break;

			case BrushCols.Black:
				scn_comp.load_brush(BrushCols.Black);
				break;

			case BrushCols.Green:
				scn_comp.load_brush(BrushCols.Green);
				break;
		}

		Display.color_wnd_close();
	}
}
