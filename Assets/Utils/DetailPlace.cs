using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    public class DetailPlace
    {
        public Vector3 Position;
        public Vector3 Rotation;

        public DetailPlace(Vector3 pos, Vector3 rot)
        {
            Position = pos;
            Rotation = rot;
        }
    }
    
}
