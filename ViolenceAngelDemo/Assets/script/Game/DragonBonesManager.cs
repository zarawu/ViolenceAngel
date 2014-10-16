using UnityEngine;
using System.Collections;

using DragonBones;
using DragonBones.Factorys;
using DragonBones.Animation;
using DragonBones.Objects;
using DragonBones.Display;
using DragonBones.Textures;

using System;
using System.IO;
using System.Collections.Generic;
using Com.Viperstudio.Utils;


public class DragonBonesManager : MonoBehaviour {

	private Armature armature;
	Vector3 position;

	void Start () {
		//Factory:解析数据格式和准备图像资源，并且通过它创建骨骼容器Armature。//
		//Armature: 我们可以把它想像为一个容器，
		//它对应在Flash Pro中编辑并通过骨骼面板导出的一个MoiveClip。
		//通过Armature来对各骨骼进行管理，播放动画等。//

		TextAsset jsonReader = (TextAsset)Resources.Load("skeleton.json", typeof(TextAsset));
		TextReader reader = new StringReader (jsonReader.text);
		Dictionary<String, System.Object> skeletonRawData = Json.Deserialize (reader) as Dictionary<String, System.Object>;
		SkeletonData skeletonData = ObjectDataParser.ParseSkeletonData (skeletonRawData);
		
		//read and parse texture atlas(地图集; 〈比喻〉身负重担的人) josn into TextureAtlas//
		Texture _textures = Resources.Load<Texture>("texture");
		jsonReader = (TextAsset)Resources.Load("texture.json", typeof(TextAsset));
		reader = new StringReader (jsonReader.text);
		Dictionary<String, System.Object> atlasRawData = Json.Deserialize (reader) as Dictionary<String, System.Object>;
		AtlasData atlasData = AtlasDataParser.ParseAtlasData (atlasRawData);
		TextureAtlas textureAtlas = new TextureAtlas (_textures, atlasData);
		
		//use the above data to make factory
		UnityFactory factory = new UnityFactory ();
		//print ("skeletonData.Name="+skeletonData.Name);
		factory.AddSkeletonData (skeletonData, skeletonData.Name);
		factory.AddTextureAtlas (textureAtlas);

		armature = factory.BuildArmature ("Dragon",null,"DragonBones_Tutorial_ChangeClothes");
		armature.AdvanceTime (1f);
		((armature.Display as UnityArmatureDisplay).Display as GameObject).transform.position = new Vector3(0f, 0f,0f);
		WorldClock.Clock.Add (armature);
		armature.Animation.GotoAndPlay("stand", -1, -1, 0);
		//armature.Animation.getState ("stand",0);
		Debug.Log ("name="+armature.Animation.MovementID);
		position=new Vector3(0.3f,0,0);

		transform.position+= position;
	}



	void Update () {
		WorldClock.Clock.AdvanceTime (Time.deltaTime);

		if("walk"==armature.Animation.MovementID){
			((armature.Display as UnityArmatureDisplay).Display as GameObject).transform.position += new Vector3(-0.02f, 0f,0f);
		}

		//没有touch事件时，并且当前状态为work，改为stand

		dealAndroidInput ();
	}

	
	RaycastHit2D hit;
	Vector3 inputPos;

	private void dealAndroidInput(){
		if(Input.GetMouseButton(0)){
			
			if (Input.GetMouseButtonDown(0))
			{
				inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				inputPos.z = 0;
				hit = Physics2D.Raycast(inputPos, new Vector2(0, 0), 0.1f, 1<<LayerMask.NameToLayer("GUI"));

				if (hit.collider != null) {
					if( hit.transform.name=="back"){
						armature.Animation.GotoAndPlay ("walk", -1, -1, 0);
					}else if( hit.transform.name=="forward"){
						if("fall"!=armature.Animation.MovementID){
							armature.Animation.GotoAndPlay ("fall", -1, -1, 0);
						}
						//armature.Animation.GotoAndPlay ("fall", -1, -1, 0);
					}else if( hit.transform.name=="sword"){
						armature.Animation.GotoAndPlay ("jump", -1, -1, 0);
					}
				}
			}



		}

		if(Input.GetMouseButtonUp(0)){
			Debug.Log("get mouseButton up");
			armature.Animation.GotoAndPlay ("stand", -1, -1, 0);
			
		}

	}


	private void switchStateTo(String name){


	}




//	void OnGUI(){
//		if(armature==null){
//			return;
//		}
//		//click
//
//		if (GUI.RepeatButton (new Rect (10, 220, 50, 50), (Texture)Resources.Load ("gui/btn_back_click", typeof(Texture)), GUIStyle.none)) {
//			Debug.Log ("clicked back");
//			if("walk"!=armature.Animation.MovementID){
//				armature.Animation.GotoAndPlay ("walk", -1, -1, 0);
//			}
//
//		}
//
//		if (GUI.RepeatButton (new Rect (80, 220, 50, 50), (Texture)Resources.Load ("gui/btn_forward_click", typeof(Texture)), GUIStyle.none)) {
//			Debug.Log ("clicked forward");
//
//		}
//
//	}
}
