using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheels : MonoBehaviour
{
    public class WheelColliders
    {
        public WheelCollider FR;
        public WheelCollider FL;
        public WheelCollider RR;
        public WheelCollider RL;
    }

    public class WheelMeshes
    {
        MeshRenderer FR;
        MeshRenderer FL;
        MeshRenderer RR;
        MeshRenderer RL;
    }

    public WheelColliders colliders;

}
