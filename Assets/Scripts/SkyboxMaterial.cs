using UnityEngine;
using System.Collections;
using System.IO;

public class SkyboxMaterial : MonoBehaviour {
	
	private string[] _skyboxTexturesNames = new string[6];
	private Texture[] _skyboxTextures = new Texture[6];

	private void Awake()
	{
		InitSkyboxTexturesNames();
	}
	private void InitSkyboxTexturesNames()
	{
		_skyboxTexturesNames[0] = "1_front.jpg";
		_skyboxTexturesNames[1] = "2_back.jpg";
		_skyboxTexturesNames[2] = "3_left.jpg";
		_skyboxTexturesNames[3] = "4_right.jpg";
		_skyboxTexturesNames[4] = "5_up.jpg";
		_skyboxTexturesNames[5] = "6_down.jpg";
	}

	void Start()
	{        
		StartCoroutine(LoadTextures());
	}
	
	IEnumerator LoadTextures()
	{
		for(int i = 0; i < _skyboxTexturesNames.Length; i++)
		{
			var path = "file://"+Application.streamingAssetsPath+"/"+_skyboxTexturesNames[i];               
			WWW www = new WWW (path);
			while(!www.isDone)
			{
				yield return www;
			}
			
			_skyboxTextures[i] = www.texture;
		}
		Material material = CreateSkyboxMaterial();
		SetSkybox(material);
	}

	public Material CreateSkyboxMaterial()
	{
		Material result = new Material(Shader.Find("RenderFX/Skybox"));

		result.SetTexture("_FrontTex", _skyboxTextures[0]);
		result.SetTexture("_BackTex", _skyboxTextures[1]);
		result.SetTexture("_LeftTex", _skyboxTextures[2]);
		result.SetTexture("_RightTex", _skyboxTextures[3]);
		result.SetTexture("_UpTex", _skyboxTextures[4]);
		result.SetTexture("_DownTex", _skyboxTextures[5]);
		return result;
	}

	void SetSkybox(Material material)
	{
		RenderSettings.skybox = material;
	}
}

public struct SkyboxManifest
{
	public Texture2D[] textures;
	
	public SkyboxManifest(Texture2D front, Texture2D back, Texture2D left, Texture2D right, Texture2D up, Texture2D down)
	{
		textures = new Texture2D[6]
		{
			front,
			back,
			left,
			right,
			up,
			down
		};
	}
}
