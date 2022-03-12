using System;
using System.Collections;
using Modding;
using UnityEngine;
using NoRecoilModNS;

namespace NoRecoilModNS
{
	public class Mod : ModEntryPoint
	{
		public string objectName = "EsModControllerObject";
		public override void OnLoad()
		{
			GameObject gameObject = GameObject.Find(objectName);
			if(!gameObject)
			{
				UnityEngine.Object.DontDestroyOnLoad(gameObject = new GameObject(objectName));
			}
			gameObject.AddComponent<NoRecoilModCore>();
		}
	}
}
