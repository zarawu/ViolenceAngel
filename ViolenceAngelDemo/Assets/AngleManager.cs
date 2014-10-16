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


public class AngleManager : MonoBehaviour {

	void Start () {
		//test animation 
		playBehavior ("AnimationTest","骨骼名称","原文件名称","动作名称");	
		//playBehavior ("DragonChangeClothes","Dragon","DragonBones_Tutorial_ChangeClothes","stand");
	}
	
	//private Armature armature;
	Vector3 position;

	UnityFactory factory;
	Armature armature;
	public void playBehavior(String dir,String armatureName,String skeletonDataName,String behaviorName){
		//Factory:解析数据格式和准备图像资源，并且通过它创建骨骼容器Armature。//
		//Armature: 我们可以把它想像为一个容器，
		//它对应在Flash Pro中编辑并通过骨骼面板导出的一个MoiveClip。
		//通过Armature来对各骨骼进行管理，播放动画等。//

		TextAsset jsonReader = (TextAsset)Resources.Load(dir+"/skeleton", typeof(TextAsset));//read json 
		//Debug.Log ("jsonReader="+jsonReader);
		TextReader reader = new StringReader (jsonReader.text);
		//Debug.Log ("reader="+reader);
		Dictionary<String, System.Object> skeletonRawData = Json.Deserialize (reader) as Dictionary<String, System.Object>;
		Debug.Log ("skeletonRawData="+skeletonRawData.ContainsKey("armature"));
		SkeletonData skeletonData = ObjectDataParser.ParseSkeletonData (skeletonRawData);
		//Debug.Log ("skeletonData="+skeletonData);
		//read and parse texture atlas(地图集; 〈比喻〉身负重担的人) josn into TextureAtlas//
		Texture _textures = Resources.Load<Texture>(dir+"/texture");
		jsonReader = (TextAsset)Resources.Load(dir+"/texture", typeof(TextAsset));
		reader = new StringReader (jsonReader.text);
		Dictionary<String, System.Object> atlasRawData = Json.Deserialize (reader) as Dictionary<String, System.Object>;
		AtlasData atlasData = AtlasDataParser.ParseAtlasData (atlasRawData);
		TextureAtlas textureAtlas = new TextureAtlas (_textures, atlasData);
		
		//use the above data to make factory
		factory = new UnityFactory ();
		print ("skeletonData.Name="+skeletonData.Name);
		factory.AddSkeletonData (skeletonData, skeletonData.Name);
		factory.AddTextureAtlas (textureAtlas);

		armature = factory.BuildArmature (armatureName,null,skeletonDataName);
		armature.AdvanceTime (1f);
		((armature.Display as UnityArmatureDisplay).Display as GameObject).transform.position = new Vector3(0f, 0f,0f);
		//((armature.Display as UnityArmatureDisplay).Display as GameObject)
		WorldClock.Clock.Add (armature);
		armature.Animation.GotoAndPlay(behaviorName, -1, -1, 0);

//		Debug.Log ("name="+armature.Animation.MovementID);
//		Debug.Log ("0 name="+armature.Animation.MovementList[0]);


		//test

	}
	


	private String[] textures=new String[]{"parts/clothes1","parts/clothes2", "parts/clothes3", "parts/clothes4"};
	private int textureIndex= 0;

	private void changeClothes(){
		//这里我们用到了dragonBones.factorys.StarlingFactory.getTextureDisplay(_textureData:TextureData, _fullName:String):Image
		//来获取由骨骼动画编辑面板导出的材质数据，然后将对应的材质赋予给骨骼的display对象，实现了换装。
		//当然，正如前面所说，换装的材质可以随意来自程序中载入的其他图片资源。
		
		//另外，当你熟悉了DragonBones的开源框架，你会发现可以通过代码实现更加灵活的变换，除了更换骨骼材质，
		//你还可以在骨骼框架（Armature）中动态的删除、添加骨骼，改变骨骼的从属关系等。
		//循环更换贴图
//		textureIndex++;
//		if(textureIndex >= textures.Length) {
//			textureIndex = textureIndex - textures.Length;
//		}
		//从骨骼面板导出的textureData中获取Image实例，也可以单独从其他图片文件中构造Image
		String _textureName= textures[textureIndex];
		factory.GetTextureDisplay (_textureName);
		Debug.Log ("factory.GetTextureDisplay (_textureName)="+factory.GetTextureDisplay (_textureName));
		//Image  _image = factory.getTextureDisplay("_textureName")as Image;
		//用image替换bone.display完成换装（注意bone.display的回收）
		Bone _bone = armature.GetBone("clothes");
		//TODO:get bone's texture
		Debug.Log ("display="+_bone.Display);
		//_bone.display.dispose();
		//_bone.display = _image;


		//TODO:实现换装
		//1获得所需要的材质
		//2找到骨架材质的设置方式
		//3注意回收


		
	}
	
	void Update () {
		WorldClock.Clock.AdvanceTime (Time.deltaTime);
		
//		if("walk"==armature.Animation.MovementID){
//			((armature.Display as UnityArmatureDisplay).Display as GameObject).transform.position += new Vector3(-0.02f, 0f,0f);
//		}

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

						//changeClothes();
						//armature.Animation.GotoAndPlay ("walk", -1, -1, 0);
					}else if( hit.transform.name=="forward"){
//						if("fall"!=armature.Animation.MovementID){
//							armature.Animation.GotoAndPlay ("fall", -1, -1, 0);
//						}
					}else if( hit.transform.name=="sword"){
//						armature.Animation.GotoAndPlay ("jump", -1, -1, 0);
					}
				}
			}
			
			
			
		}
		
		if(Input.GetMouseButtonUp(0)){
//			Debug.Log("get mouseButton up");
//			armature.Animation.GotoAndPlay ("stand", -1, -1, 0);
			
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
