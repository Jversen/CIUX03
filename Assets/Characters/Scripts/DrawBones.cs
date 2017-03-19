using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBones : MonoBehaviour
{

    public bool showMesh = true;
    public Color boneColor = Color.green;

    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Mesh mesh;

    void Start()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        mesh = skinnedMeshRenderer.sharedMesh;
    }

    void Update()
    {
        if (skinnedMeshRenderer == null)
        {
            Debug.LogWarning("No SkinnedMeshRender found");
            Destroy(this);
        }
    }

    void LateUpdate()
    {
        // Show or hide mesh
        if (showMesh)
        {
            skinnedMeshRenderer.sharedMesh = mesh;
        }
        else
        {
            skinnedMeshRenderer.sharedMesh = null;
        }

        Transform[] bones = skinnedMeshRenderer.bones;
        foreach (Transform bone in bones)
        {
            if (bone.parent != null)
            {
                Debug.DrawLine(bone.position, bone.parent.position, boneColor);
            }
        }
    }

}
