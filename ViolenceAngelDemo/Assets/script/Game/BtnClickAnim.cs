using UnityEngine;
using System.Collections;

public class BtnClickAnim : MonoBehaviour {
	void OnMouseDown(){
		if (name == "back") {
			SpriteRenderer spriteRenderer = gameObject.GetComponent (typeof(SpriteRenderer))as SpriteRenderer;
			spriteRenderer.sprite = Resources.Load ("btn_back_click", typeof(Sprite)) as Sprite;
		} else if (name == "forward") {
			SpriteRenderer spriteRenderer = gameObject.GetComponent (typeof(SpriteRenderer))as SpriteRenderer;
			spriteRenderer.sprite = Resources.Load ("btn_forward_click", typeof(Sprite)) as Sprite;
		}else {
			transform.localScale =new Vector3(1.1f,1.1f,1f);	
		}
		
	}
	
	void OnMouseUpAsButton(){
		if (name == "game_pay_confirm") {
			SpriteRenderer spriteRenderer = gameObject.GetComponent (typeof(SpriteRenderer))as SpriteRenderer;
			spriteRenderer.sprite = Resources.Load ("btn_back_normal", typeof(Sprite)) as Sprite;
		} else if (name == "game_pay_cancel") {
			SpriteRenderer spriteRenderer = gameObject.GetComponent (typeof(SpriteRenderer))as SpriteRenderer;
			spriteRenderer.sprite = Resources.Load ("btn_forward_normal", typeof(Sprite)) as Sprite;
		} else {
			transform.localScale =new Vector3(1.0f,1.0f,1f);	
		}
		
	}
	
	
	
	void OnMouseExit(){
		if (name == "game_pay_confirm") {
			SpriteRenderer spriteRenderer = gameObject.GetComponent (typeof(SpriteRenderer))as SpriteRenderer;
			spriteRenderer.sprite = Resources.Load ("btn_back_normal", typeof(Sprite)) as Sprite;
		} else if (name == "game_pay_cancel") {
			SpriteRenderer spriteRenderer = gameObject.GetComponent (typeof(SpriteRenderer))as SpriteRenderer;
			spriteRenderer.sprite = Resources.Load ("btn_forward_normal", typeof(Sprite)) as Sprite;
		} else {
			transform.localScale =new Vector3(1.0f,1.0f,1f);	
		}
	}
}
