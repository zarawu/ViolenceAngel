// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using Com.Viperstudio.Geom;
namespace DragonBones.Objects
{
	public class DisplayData
	{
		public const string ARMATURE = "armature";
		public const string IMAGE = "image";
		
		public string Name;
		public string Type;
		public DBTransform Transform;
		public Point Pivot;
		
		public DisplayData()
		{
			Transform = new DBTransform();
		}
		
		public void Dispose()
		{
			Transform = null;
			Pivot = null;
		}
	}
}

