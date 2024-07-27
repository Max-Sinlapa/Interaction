using System.Collections;
using System.Collections.Generic;
using MaxDev.Interaction;
using UnityEngine;

public class ChangeObjectSequence : MonoBehaviour
{
    [Header("Object-SaveData")]
    public m_ObjectData gameData;
        
    [Header("Object-NewData")]
    public List<ObjectInteracData> NewSequenceObjectData;
    private IObjectInteracService ObjectServiec = new ObjectInteracService();

    public void ChangeObjectSequenceData(int targetSequenceData)
    {
        if (gameData == null)
        {
            Debug.LogError("No 'Object Data' In " + this.name + " Plese Add 'Object' in " + this.name);
            return;
        }
        ObjectServiec.ChangeObjectSequence(NewSequenceObjectData[targetSequenceData], gameData);
    }
    
    public void ChangeObject_SequenceData_AllInThisList()
    {
        if (gameData == null)
        {
            Debug.LogError("No 'Object Data' In " + this.name + " Plese Add 'Object' in " + this.name);
            return;
        }

        foreach (var sceneData in NewSequenceObjectData)
        {
            ObjectServiec.ChangeObjectSequence(sceneData, gameData);
        }
    }
}
