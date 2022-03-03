using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawAnimator : MonoBehaviour

{

    public Animator anim;

    public HumanBodyBones Jaw;

    public Vector3 rotationVector;



    void LateUpdate()

    {

        anim.GetBoneTransform(Jaw).localRotation = Quaternion.Euler(rotationVector);

    }

}
