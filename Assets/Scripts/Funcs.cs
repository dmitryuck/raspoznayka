using UnityEngine;

using System;
using System.IO;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;

public class Funcs {

	// Задержка
	public static IEnumerator delay(float time, System.Action callback){
		yield return new WaitForSeconds(time);
		
		callback();

		yield break;
	}

	// Проиграть звук
	public static IEnumerator play_sound(GameObject targ, string name){
		AudioSource snd = targ.AddComponent<AudioSource>();
		AudioClip clip = Resources.Load(Config.snd_load_path + name) as AudioClip;
		snd.PlayOneShot(clip);
		
		while(snd.isPlaying == true){
			yield return new WaitForSeconds(0.1f);
		}
		
		MonoBehaviour.Destroy(snd);
	}

	// Ждать обьект
	public delegate object wait(object args);
	public static IEnumerator wait_for(wait exp, object args, System.Action<object> callback){
		while (true) {
			object obj = exp (args);

			if (obj != null) {
				callback (obj);

				yield break;
			}

			yield return new WaitForSeconds(0.1f);
		}
	}

	// Корневой родитель
	public static GameObject get_root_mesh(GameObject targ_obj){
		Transform find_trans = targ_obj.transform.parent;

		while(find_trans != null){
			Transform parent_trans = find_trans.transform.parent;

			if(parent_trans != null){
				find_trans = parent_trans;
			} else {
				break;
			}
		}

		return find_trans.gameObject;
	}

	// Поиск меша в обьекте
	public static GameObject get_sub_mesh(GameObject targ_obj, string name){
		GameObject find_obj;
		
		for(int i = 0; i < targ_obj.transform.childCount; i ++){
			GameObject curr_obj = targ_obj.transform.GetChild(i).gameObject;

			find_obj = curr_obj;

			if(curr_obj.name != name){
				recurs_find(curr_obj, name, ref find_obj);
			}

			if(find_obj.name == name){
				return find_obj;
			}
		}
		
		find_obj = null;
		
		return null;
	}
	
	// Рекурсивный поиск меша
	private static void recurs_find(GameObject targ_obj, string name, ref GameObject ret_obj){
		for(int a = 0; a < targ_obj.transform.childCount; a ++){
			GameObject pod_obj = targ_obj.transform.GetChild(a).gameObject;
			
			if(pod_obj.name == name){				
				ret_obj = pod_obj;
				
				break;
			} else {
				recurs_find(pod_obj, name, ref ret_obj);
			}
		}
	}

	// Хекс ту колор
	public static Color hex_to_col(string hex, float alpha = 1.0f){
		if (hex != null) {
			hex = hex.Replace ("0x", ""); //in case the string is formatted 0xFFFFFF
			hex = hex.Replace ("#", ""); //in case the string is formatted #FFFFFF

			byte r = byte.Parse (hex.Substring (0, 2), System.Globalization.NumberStyles.HexNumber);
			byte g = byte.Parse (hex.Substring (2, 2), System.Globalization.NumberStyles.HexNumber);
			byte b = byte.Parse (hex.Substring (4, 2), System.Globalization.NumberStyles.HexNumber);

			byte a = 255; //assume fully visible unless specified in hex

			if (hex.Length == 8) {
				a = byte.Parse (hex.Substring (4, 2), System.Globalization.NumberStyles.HexNumber);
			}

			return new Color (r, g, b, alpha);
		}

		return new Color ();
	}
	
	// Взять камеру
	public static GameObject get_cam(){
		return GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Взять плеера
	public static GameObject get_play(){
		return GameObject.FindGameObjectWithTag("Player");
	}

	// Взять канву - холст
	public static GameObject get_cnv(){
		return GameObject.Find("Canvas");
	}
	
	// Очистить строку
	public static string strip_tags(string input){
		string output;

		output = Regex.Replace(input, @"</?.+?>", string.Empty);
		output = Regex.Replace(output, @"<(.|\n)*?>", string.Empty);
		
		return output;
	}	
	
	// Выброс ошибки
	public static void throw_error(string message){
		throw new Exception(message);
	}

	// Генерация уникального цифрового значения
	public static int gen_unique_id(int size = 4){
		int tm = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

		string s_tm = tm.ToString();

		return Convert.ToInt32(s_tm.Substring(s_tm.Length - (s_tm.Length - size), size));
	}
	
	// Получить md5 строки
    	public static string get_md5(string input){
	        MD5 md5 = MD5.Create();

	        byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(input));

	        StringBuilder sBuilder = new StringBuilder();

	        for (int i = 0; i < data.Length; i++){
	            sBuilder.Append(data[i].ToString("x2"));
		}
			
	        return sBuilder.ToString();
    	}

	// Конвертировать обьект с набором полей в Хеш-Таблицу
	public static Hashtable object_to_hash(object obj){
		Hashtable hash = new Hashtable ();
		
		PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
		
		foreach (PropertyDescriptor prop in props) { 
			object val = prop.GetValue(obj);
			hash.Add(prop.Name, val);
		}
		
		return hash;
	}

	// Конвертация обьекта в оыщт строку
	//public static string object_to_json(object obj){
	
	//}
		
	// Зашифровать строку
	public static string encode_string(string str){
        	return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
    	}
	
	// Расшифровать строку
	public static string decode_string(string str){
        	return Encoding.UTF8.GetString(Convert.FromBase64String(str));
	}
}
