using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using UnityEngine.Tilemaps;



public class MenuItemExport
{
	delegate void functor(GameObject gameObj);
	static functor f = (GameObject gameObj) => { Debug.Log(gameObj.name); };

	// エディターにメニューを追加する
	[MenuItem("えくすぽーと/えくすぽーとせっとでーた", priority = 1)]
	static public void MenuSample()
	{
		
		var path = EditorUtility.SaveFilePanel("保存", ".", "ObjectRocation.dat", "dat");
		if (path == "")
		{
			Debug.Log("セーブキャンセルされました");
			return;
		}
		var stream = File.Create(path);
		var bw = new BinaryWriter(stream);
		bw.Write(5);
		bw.Write(15);
		string str = "C++Programming";
		bw.Write(str.Length);
		bw.Write(str.ToCharArray());

		var str2 = "あ" + "い" + "う"+"え"+"お";
		str2 += @"か	き	く	け	こ";
		Debug.Log(str2);
		bw.Write(str2.Length);
		bw.Write(str2.ToCharArray());
		bw.Close();
		stream.Close();
		EditorUtility.DisplayDialog("出力されました", path + "に出力されました", "OK");
		Debug.Log("ExportObjetRocation.dat");

	}

	[MenuItem("えくすぽーと/はいちでーたしゅつりょく", priority = 0)]
	static public void MenuDummy()
	{
		
		var gameObj = Selection.activeGameObject;
		if (gameObj == null)
		{
			return;
		}

		var path = EditorUtility.SaveFilePanel("保存", ".", "TestObjectPostion.dat", "dat");
		if (path == "")
		{
			Debug.Log("セーブキャンセルされました");
			return;
		}

		var stream = File.Create(path);
		var bw = new BinaryWriter(stream);
		TraverseNode(gameObj);
		WriteVector3(bw, gameObj.transform.position);
		bw.Close();
	}


	private static void WriteVector3(BinaryWriter bw,in Vector3  vector3)
	{
		bw.Write(vector3.x);
		bw.Write(vector3.y);
		bw.Write(vector3.z);
	}

	private static void TraverseNode(GameObject gameObj)
	{
		
		if (gameObj.GetComponent<MeshFilter>() != null && gameObj.tag == "Player")
		{
			f(gameObj);
			//Debug.Log(gameObj.name);
		}
		var count = gameObj.transform.childCount;
		for (int i = 0; i < count; i++)
		{
			TraverseNode(gameObj.transform.GetChild(i).gameObject);
		}
		
	}
}
