using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaxDev.Interaction
{
    [CreateAssetMenu(menuName = "GameData/ObjectData")]
    public class m_ObjectData : ScriptableObject
    {
        public List<ObjectInteracData> objectData;
        public void ClearData()
        {
            objectData.Clear();
        }
    }
}