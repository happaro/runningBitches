using UnityEngine;
using System.Collections;

public class SkinChanger : MonoBehaviour 
{
	public Material[] mats;
	public Mesh[] meshs;

	public SkinnedMeshRenderer mesh;

	public int ChangeMaterial(int toMat)
	{
		if (toMat < 0)
		{
			mesh.sharedMaterials = new Material[]{mats[mats.Length - 1], mesh.sharedMaterials[1]};
			return mats.Length - 1;
		}
		else if (toMat > mats.Length - 1)
		{
			mesh.sharedMaterials = new Material[]{mats[0], mesh.sharedMaterials[1]};
			return 0;
		}
		else
		{
			mesh.sharedMaterials = new Material[]{mats[toMat], mesh.sharedMaterials[1]};
			return toMat;
		}
	}

	public int ChangeModel(int toModel)
	{
		if (toModel < 0)
		{
			mesh.sharedMesh = meshs[meshs.Length - 1];
			return meshs.Length - 1;
		}
		else if (toModel > meshs.Length - 1)
		{
			mesh.sharedMesh = meshs[0];
			return 0;
		}
		else
		{
			mesh.sharedMesh = meshs[toModel];
			return toModel;
		}
	}
}
