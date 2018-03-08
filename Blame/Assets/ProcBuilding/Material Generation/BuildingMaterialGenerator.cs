using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class BuildingMaterialGenerator : MonoBehaviour {

	[HideInInspector]
    public Material wallMaterial;
	[HideInInspector]
	public Texture2D wallTexture;
	[HideInInspector]
	public Texture2D windowTexture;
    public int textureWidth = 100, textureHeight = 100;
    public Color baseColor, lightColor = Color.white;
	[Range(1, 20)]
    public int lightSpacing = 7;
	[Range(0f, 1f)]
    public float lightFrequency = 0.25f;

	void Awake()
	{
		Generate();
	}

    public void Generate()
    {
		if (wallMaterial == null)
		{
			Shader defaultShader = Shader.Find("Standard");
			wallMaterial = new Material(defaultShader);
		}
		wallTexture = WallTexture.Create(textureWidth, textureHeight, baseColor, Color.magenta);
		windowTexture = WindowTexture.Create(textureWidth, textureHeight, lightSpacing, lightFrequency, lightColor);
        wallMaterial.EnableKeyword("_Emission");
        wallMaterial.mainTexture = wallTexture;
        wallMaterial.SetTexture("_EmissionMap", windowTexture);
		wallMaterial.SetColor("_EmissionColor", Color.white);
		wallMaterial.name = "Generated Material";
		GetComponent<MeshRenderer>().sharedMaterial = wallMaterial;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BuildingMaterialGenerator))]
public class BuildingMaterialGeneratorEditor : Editor {

	private BuildingMaterialGenerator generator {get {return target as BuildingMaterialGenerator;}}

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		DrawDefaultInspector();
		if (EditorGUI.EndChangeCheck())
		{
			generator.Generate();
		}

		if (GUILayout.Button("Save to Material"))
		{
			generator.Generate();
			string path = EditorUtility.SaveFilePanelInProject("Save Building Material", "New Material", "mat", "Save Building Material");
			
			if (path != "")
			{
				Material newMaterial = AssetDatabase.LoadAssetAtPath(path, typeof(Material)) as Material;
				//If there is no material that exists at the given path, make a new one and save it
				//Otherwise, overwrite the values of the existing material so we don't break references
				if (newMaterial == null)
				{
					newMaterial = new Material(generator.wallMaterial);
					AssetDatabase.CreateAsset(newMaterial, path);
				}
				string directory = Path.GetDirectoryName(path);
				string materialName = Path.GetFileNameWithoutExtension(path);
				string wallTextureName = materialName + " texture.png";
				string windowTextureName = materialName + " emissive.png";
				string wallTexturePath = Path.Combine(directory, wallTextureName);
				string windowTexturePath = Path.Combine(directory, windowTextureName);
				newMaterial.name = materialName;

				//We need to save the textures the material uses along with the material itself
				byte[] wallData = generator.wallTexture.EncodeToPNG();
				File.WriteAllBytes(wallTexturePath, wallData);
				byte[] windowData = generator.windowTexture.EncodeToPNG();
				File.WriteAllBytes(windowTexturePath, windowData);

				AssetDatabase.Refresh();

				//Assign material textures using the saved instances
				Texture2D savedWallTexture = AssetDatabase.LoadAssetAtPath(wallTexturePath, typeof(Texture2D)) as Texture2D;
				newMaterial.mainTexture = savedWallTexture;
				Texture2D savedWindowTexture = AssetDatabase.LoadAssetAtPath(windowTexturePath, typeof(Texture2D)) as Texture2D;
        		newMaterial.EnableKeyword("_EMISSION");
				newMaterial.SetTexture("_EmissionMap", savedWindowTexture);
				newMaterial.SetColor("_EmissionColor", Color.white);
			}
		}
	}
}
#endif
