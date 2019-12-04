using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GenericCameraFollow : MonoBehaviour
{
    public Transform camera;

    public float positionLerpValue;
    public float rotationLerpValue;

    void Start()
    {
        camera.position = transform.position;
        camera.rotation = transform.rotation;
    }

    void LateUpdate()
    {
        camera.position = Vector3.Lerp(camera.position, transform.position, positionLerpValue);
        camera.rotation = Quaternion.Lerp(camera.rotation, transform.rotation, rotationLerpValue);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GenericCameraFollow))]
public class CameraFollowDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GenericCameraFollow gcf = (GenericCameraFollow)target;

        if(GUILayout.Button("Set Target Transform"))
        {
            Transform c = gcf.camera;

            c.position = Selection.activeGameObject.transform.position;
            c.rotation = Selection.activeGameObject.transform.rotation;
        }
    }
}
#endif