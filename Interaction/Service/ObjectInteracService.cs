using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaxDev.Interaction
{
    public interface IObjectInteracService
    {
        ObjectInteracData GetObjectInteracData(ObjectInteracData thisObjectInteracData, m_ObjectData objectInteracDataScriptObject);
    
        bool SaveObjectInteracData(ObjectInteracData newObjectInteracData, m_ObjectData objectInteracDataScriptObject);
    
        bool ChangeObjectSequence(ObjectInteracData newSceneSequenceData, m_ObjectData sceneListDataScriptObject);
    }

    public class ObjectInteracService : IObjectInteracService
    {
        public ObjectInteracData GetObjectInteracData(ObjectInteracData thisObjectInteracData, m_ObjectData objectInteracDataScriptObject)
        {
            ObjectInteracData ObjectInteracData = objectInteracDataScriptObject.objectData.Find(i => i.ObjectID == thisObjectInteracData.ObjectID);
            if (ObjectInteracData != null)
            {
                return ObjectInteracData;
            }
            else
            {
                ObjectInteracData thisNewObjectData = new ObjectInteracData();
                thisNewObjectData = thisObjectInteracData;
            
                objectInteracDataScriptObject.objectData.Add(thisNewObjectData);
                return thisNewObjectData;
            }
        }

        public bool SaveObjectInteracData(ObjectInteracData newObjectInteracData, m_ObjectData objectInteracDataScriptObject)
        {
            ObjectInteracData ObjectInteracData = objectInteracDataScriptObject.objectData.Find(i => i.ObjectID == newObjectInteracData.ObjectID);
            if (ObjectInteracData != null)
            {
                ObjectInteracData = newObjectInteracData;
                return true;
            }
            else
            {
                objectInteracDataScriptObject.objectData.Add(newObjectInteracData);
                return false;
            }
        }

        public bool ChangeObjectSequence(ObjectInteracData newObjectSequenceData, m_ObjectData objectInteracDataScriptObject)
        {
            ObjectInteracData ObjectInteracData = objectInteracDataScriptObject.objectData.Find(i => i.ObjectID == newObjectSequenceData.ObjectID);
            if (ObjectInteracData != null)
            {
                Debug.Log("Change ObjectData : " + ObjectInteracData.ObjectName + " -Object Sequence = " + ObjectInteracData.ObjectSequence + " >>To>> " + newObjectSequenceData.ObjectSequence);
                ObjectInteracData.ObjectSequence = newObjectSequenceData.ObjectSequence;
                return true;
            }
            else
            {
                ObjectInteracData NewObjectData = new ObjectInteracData();
                NewObjectData = newObjectSequenceData;
                objectInteracDataScriptObject.objectData.Add(NewObjectData);
                
                return false;
            }
        }
    }
}