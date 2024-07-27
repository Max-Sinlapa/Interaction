using System;
using System.Collections;
using System.Collections.Generic;
using MaxDev;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace MaxDev.Interaction
{
    public class SequenceCollector : MonoBehaviour , I_Interactable
    {
        [Header("Debugger")] [SerializeField] private bool DebugThis;
        
        public m_ObjectData gameData;
        [Header("Interaction-Setup")] 
        public UnityEvent OnObjectTriggerEnter;
        public List<UnityEvent> ObjectInteracSequence;
        public List<UnityEvent> ObjectWhildInCurrentSequence;

        [Header("ObjectData")] 
        [SerializeField]private ObjectInteracData thisObjectData;
        private IObjectInteracService _objectInteracService = new ObjectInteracService();
        
        [Header("Object-Color")] 
        public List<Color> newColor;
        private SpriteRenderer thisSpriteRenderer;
        
        public static event SequenceCollector.EventHandler afterSequenceLoadFinish;
        public delegate void EventHandler();
        

        #region UnityEvent

        private void Awake()
        {                       
            
            if (GetComponent<SpriteRenderer>())
            {
                thisSpriteRenderer = GetComponent<SpriteRenderer>();
            }
            if (gameData == null)
            {
                LogError("Plese Add gameObjectData In Object : " + this.name);
            }

            SetupObject();
            StartCoroutine(LoadDataInGameData());
        }
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Log("Player Triger Object" + this.gameObject);
                OnObjectTriggerEnter.Invoke();
            }
        }

        #endregion
        

        void SetupObject()
        {
            thisObjectData = new ObjectInteracData();
            thisObjectData.ObjectName = gameObject.name;
            thisObjectData.ObjectID = SetupObjectID();
            thisObjectData.ObjectSequence = 0;
        }

        public void InteracToObject()
        {
            InvokeInteracSequence();
        }

        public void SequenceChose(int sequenceOrder)
        {
            if (sequenceOrder < 0)
            {
                Log("Object : " + gameObject.name + " - No InteractionSequence Change");
            }
            else
            {
                thisObjectData.ObjectSequence = sequenceOrder;
                StartCoroutine(UpdateDataToGameData());
                InvokeWhildInCurrentSequence();
                Log("Object : " + gameObject.name + "- Current Sequence Is = " + sequenceOrder);
            }
        }

        public void InvokeInteracChoseSequence(int sequenceOrder)
        {
            if (sequenceOrder < ObjectInteracSequence.Count)
            {
                Log("Object : " + this.name + " -is Invoke InteracEvent Sequence = " + sequenceOrder);
                ObjectInteracSequence[sequenceOrder].Invoke();
                StartCoroutine(UpdateDataToGameData());
                InvokeWhildInCurrentSequence();
            }
            else
            {
                LogError("Object : " + this.name + " -is Not Have InteracEvent Sequence = " + sequenceOrder);
            }
        }
        
        public void InvokeCurrentInteracSequence()
        {
            if (thisObjectData.ObjectSequence < ObjectInteracSequence.Count)
            {
                Log("Object : " + this.name + " -is Invoke InteracEvent Sequence = " + thisObjectData.ObjectSequence);
                ObjectInteracSequence[thisObjectData.ObjectSequence].Invoke();
                StartCoroutine(UpdateDataToGameData());
                InvokeWhildInCurrentSequence();

            }

        }

        void InvokeWhildInCurrentSequence()
        {
            
            if(thisObjectData.ObjectSequence < ObjectWhildInCurrentSequence.Count)
            {
                Log("Object : " + this.name + " -is Invoke WhildEvent Sequence = " + thisObjectData.ObjectSequence);
                ObjectWhildInCurrentSequence[thisObjectData.ObjectSequence].Invoke();
            }
            else
            {
                LogWarning("Object : " + this.name + " -No Whild Event In Sequence = " + thisObjectData.ObjectSequence);
            }
        }

        void InvokeInteracSequence()
        {
            if(thisObjectData.ObjectSequence < ObjectInteracSequence.Count)
            {
                Log("Object : " + this.name + " -is Invoke InteracEvent Sequence = " + thisObjectData.ObjectSequence);
                ObjectInteracSequence[thisObjectData.ObjectSequence].Invoke();
            }
            else
            {
                LogWarning("Object : " + this.name + " -No Interac Event In Sequence = " + thisObjectData.ObjectSequence);
            }
        }

        IEnumerator LoadDataInGameData()
        {
            yield return new WaitUntil(() => !StaticValue.Get_OnSaveData());
            StaticValue.Set_OnLoadData(true);

            thisObjectData = _objectInteracService.GetObjectInteracData(thisObjectData, gameData);
            InvokeWhildInCurrentSequence();
            StaticValue.Set_OnLoadData(false);
            Log(gameObject.name + "Load Object Data-------------");

        }

        IEnumerator UpdateDataToGameData()
        {
            yield return new WaitUntil(() => !StaticValue.Get_OnLoadData());
            StaticValue.Set_OnSaveData(true);
            
            _objectInteracService.SaveObjectInteracData(thisObjectData, gameData);
            StaticValue.Set_OnSaveData(false);
            Log(gameObject.name + "Save Object Data To Save++++++");
            if (afterSequenceLoadFinish != null)
                afterSequenceLoadFinish.Invoke();
        }

        public void ChangeColor(int colorElement)
        {
            if (thisSpriteRenderer != null)
            {
                if (colorElement + 1 <= newColor.Count)
                {
                    Log("Object : " + this.name + " is Change Color From " + thisSpriteRenderer.color + " >>To>> " + newColor[colorElement]);
                    thisSpriteRenderer.color = newColor[colorElement];
                }
            }
            else
            {
                if (GetComponent<SpriteRenderer>())
                {
                    thisSpriteRenderer = GetComponent<SpriteRenderer>();
                    if (colorElement + 1 <= newColor.Count)
                    {
                        Log("Object : " + this.name + " is Change Color From " + thisSpriteRenderer.color + " >>To>> " + newColor[colorElement]);
                        thisSpriteRenderer.color = newColor[colorElement];
                    }
                }
                else
                {
                    LogError("Object : " + this.name + "Not Have Component 'SpriteRenderer'");
                }
            }
        }

        public void DestroyObject(GameObject targetObjectDestroy)
        {
            DestroyObject(targetObjectDestroy);
        }

        
        public string SetupObjectID()
        {
            string thisObjectName;
            thisObjectName = this.ToString();

            ///Check Object Is Not PlayerCharactor
            if (this.gameObject.tag != "Player")
            {
                if (this.transform.parent != null && this.transform.root != this.transform.parent)
                    thisObjectName = this.transform.parent.name + "_" + thisObjectName;

                if (this.transform.root != null)
                    thisObjectName = this.transform.parent.root.name + "_" + thisObjectName;
                
                thisObjectName = SceneManager.GetActiveScene().name + "_" + thisObjectName;
            }
            
            return thisObjectName;
        }
        
        
        #region Debug

        public void Log(string log)
        {
            if (DebugThis)
                Debug.Log(log);
        }

        public void LogWarning(string log)
        {
            if (DebugThis)
                Debug.LogWarning(log);
        }

        public void LogError(string log)
        {
            if (DebugThis)
                Debug.LogError(log);
        }

        #endregion
    }
}