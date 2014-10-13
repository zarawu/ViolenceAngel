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

	void Awake ()
	{
		//Application.targetFrameRate = 30;
		
	}

	// Use this for initialization
	void Start () {
		//Factory:解析数据格式和准备图像资源，并且通过它创建骨骼容器Armature。//
		//Armature: 我们可以把它想像为一个容器，
		//它对应在Flash Pro中编辑并通过骨骼面板导出的一个MoiveClip。
		//通过Armature来对各骨骼进行管理，播放动画等。//

		TextAsset jsonReader = (TextAsset)Resources.Load("skeleton_1.json", typeof(TextAsset));
		TextReader reader = new StringReader (jsonReader.text);
		Dictionary<String, System.Object> skeletonRawData = Json.Deserialize (reader) as Dictionary<String, System.Object>;
		SkeletonData skeletonData = ObjectDataParser.ParseSkeletonData (skeletonRawData);
		
		//read and parse texture atlas(地图集; 〈比喻〉身负重担的人) josn into TextureAtlas//
		Texture _textures = Resources.Load<Texture>("texture_1");
		jsonReader = (TextAsset)Resources.Load("texture_1.json", typeof(TextAsset));
		reader = new StringReader (jsonReader.text);
		Dictionary<String, System.Object> atlasRawData = Json.Deserialize (reader) as Dictionary<String, System.Object>;
		AtlasData atlasData = AtlasDataParser.ParseAtlasData (atlasRawData);
		TextureAtlas textureAtlas = new TextureAtlas (_textures, atlasData);
		
		//use the above data to make factory
		UnityFactory factory = new UnityFactory ();
		//print ("skeletonData.Name="+skeletonData.Name);
		factory.AddSkeletonData (skeletonData, skeletonData.Name);
		factory.AddTextureAtlas (textureAtlas);

		Armature armature = factory.BuildArmature ("Dragon",null,"DragonBones_tutorial_Start");
		armature.AdvanceTime (1f);
		((armature.Display as UnityArmatureDisplay).Display as GameObject).transform.position = new Vector3(0f, 1f,0f);
		WorldClock.Clock.Add (armature);
		armature.Animation.GotoAndPlay("walk", -1, -1, 0);



	}
	
	// Update is called once per frame
	void Update () {
		WorldClock.Clock.AdvanceTime (Time.deltaTime);
	}
}
