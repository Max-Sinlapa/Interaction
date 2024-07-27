using System;
using System.Collections;
using System.Collections.Generic;
using MaxDev.Interaction;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaxDev
{
   public class StaticValue : MonoBehaviour
   {
      #region StaticValue
      
      [Header("Player")]
      private static bool playerCanControl;
      private static Vector3 playerNextSpawnPoint = Vector3.zero;

      [Header("Scene")]
      private static bool whildChangeScene = true;
      private static string CurrentSceneName;
      public static List<string> SceneCurrentLoad;

      [Header("dialogue")] 
      private static bool OnDialogue = false;
         
      [Header("saveScriptObject")]
      private static bool OnLoadData = false;
      private static bool OnSaveData = false;

      #endregion

      void OnEnable()
      {
         CurrentSceneName = SceneManager.GetActiveScene().name;
      }

      #region Player

      public static void Set_playerCanControl(bool op)
      {
         if (OnDialogue)
         {
            //Debug.Log("Can not set playerControl When Dialogue");
            return;
         }
         playerCanControl = op;
      }
      
      public static bool Get_playerCanControl()
      {
         return playerCanControl;
      }

      public static void Set_playerNextSpawnPoint(Vector3 newSpawnPoint)
      {
         playerNextSpawnPoint = newSpawnPoint;
      }
      
      public static Vector3 Get_playerNextSpawnPoint()
      {
         return playerNextSpawnPoint;
      }

      #endregion
      #region Scene

      public static void Set_whildChangeScene(bool op)
      {
         whildChangeScene = op;
         //print("whildChangeScene : " + whildChangeScene);
      }

      public static bool Get_whildChangeSceneStatus()
      {
         return whildChangeScene;
      }
      
      public static void Set_CurrentSceneName(string sceneName)
      {
         CurrentSceneName = sceneName;
      }
      
      public static string Get_CurrentSceneName()
      {
         return CurrentSceneName;
      }

      #endregion
      #region Dialogue

      public static void Set_OnDialogue(bool op)
      {
         OnDialogue = op;
         ///print("OnDialogue : " + OnDialogue);
      }

      public static bool Get_OnDialogue()
      {
         return OnDialogue;
      }

      #endregion
      #region saveScriptObject

      public static void Set_OnLoadData(bool op)
      {
         OnLoadData = op;
         //print("OnLoadData From Save File : " + OnLoadData);
      }

      public static bool Get_OnLoadData()
      {
         return OnLoadData;
      }
      
      public static void Set_OnSaveData(bool op)
      {
         OnSaveData = op;
         //print("OnSaveData [data currently save] : " + OnSaveData);
      }
      
      public static bool Get_OnSaveData()
      {
         return OnSaveData;
      }

      #endregion

   }
}
