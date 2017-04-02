﻿using System.IO;
using UnityEngine;

public class RandomCharacter : MonoBehaviour
{
    enum BodyParts { Chest, Head, LeftUpperArm, RightUpperArm, LeftLowerArm, RightLowerArm, LeftHand, RightHand, LeftUpperLeg, RightUpperLeg, LeftLowerLeg, RightLowerLeg, LeftFoot, RightFoot };

    private Object chestPrefab;
    private Object headPrefab;
    private Object leftArmPrefab;
    private Object rightArmPrefab;
    private Object leftLegPrefab;
    private Object rightLegPrefab;

    // GameObjects
    private string charactersDir = "Prefabs/Characters/";
    private string characterPath;
    private string[] characterDirs;
    private GameObject chest;
    private GameObject head;
    private GameObject leftArm;
    private GameObject rightArm;
    private GameObject leftLeg;
    private GameObject rightLeg;

    void Start()
    {
        characterPath = Application.dataPath + "/Resources/" + charactersDir;
        characterDirs = Directory.GetDirectories(characterPath);

        chest = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.Chest), transform);
        head = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.Head), chest.transform);
        leftArm = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.LeftUpperArm), chest.transform);
        rightArm = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.RightUpperArm), chest.transform);
        leftLeg = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.LeftUpperLeg), chest.transform);
        rightLeg = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.RightUpperLeg), chest.transform);

        chest.transform.localPosition = Vector3.zero;

        if (chest != null)
        {
            if (head != null)
            {
                attach(head, chest, chest.transform.Find("Head Socket"));
            }

            if (leftArm != null)
            {
                attach(leftArm, chest, chest.transform.Find("Left Arm Socket"));
            }

            if (rightArm != null)
            {
                attach(rightArm, chest, chest.transform.Find("Right Arm Socket"));
            }

            if (leftLeg != null)
            {
                attach(leftLeg, chest, chest.transform.Find("Left Leg Socket"));
            }

            if (rightLeg != null)
            {
                attach(rightLeg, chest, chest.transform.Find("Right Leg Socket"));
            }
        }
        // Freeze chest position for testing
        //chest.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    }

    void Update()
    {

    }

    private Object LoadRandomBodyPart(BodyParts bodyPart)
    {
        string character = Path.GetFileName(characterDirs[Random.Range(0, characterDirs.Length)]);
        return Resources.Load(charactersDir + character + "/" + bodyPart);
    }

    private GameObject InstantiateBodyPart(Object bodyPart, Transform transform)
    {
        if (bodyPart == null)
        {
            return null;
        }
        GameObject bodyPartInst = Instantiate((GameObject)bodyPart);
        bodyPartInst.transform.parent = transform;
        return bodyPartInst;
    }

    private void attach(GameObject objectToAttach, GameObject attachToObject, Transform attachToTransform)
    {
        Joint joint = objectToAttach.GetComponent<Joint>();
        joint.connectedBody = attachToObject.GetComponent<Rigidbody>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = attachToTransform.localPosition;

        objectToAttach.transform.localPosition = attachToTransform.localPosition - joint.anchor / 2;
    }

}