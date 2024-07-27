using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager IGUID objects inside of the scene.
/// </summary>
public class SceneGUIDManager : MonoBehaviour
{
    public static SceneGUIDManager instance;
    
    public List<GUIDConnection> guidConnections;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    
    /// <summary>
    /// Assign the guid of the connection to the guid of the object.
    /// Generate one if none exists inside of the connection.
    /// </summary>
    public void OnValidate()
    {
        foreach (GUIDConnection connection in guidConnections)
        {
            if (connection.gameObject)
            {
                IGuid guidScript = connection.gameObject.GetComponent<IGuid>();

                if (guidScript == null)
                {
                    Debug.LogWarning("Attahed gameObject in the SceneGUIDManager does not have a monobehaviour script that implements IGuid interface");
                }
                else
                {
                    // Assign new guid to the IGuid script
                    guidScript.Guid = connection.Guid;
                }
            }
        }
    }

    [Serializable]
    public class GUIDConnection
    {
        public IGuid guidObject;

        [Tooltip("Game Object that has IGuid script attached, should be scene objects only")]
        public GameObject gameObject;

        public GUIDConnection()
        {
            Guid = System.Guid.NewGuid();
            guid = Guid.ToString();
        }

        // The Guid of the guidObject
        [SerializeField]
        private readonly string guid;

        public Guid Guid { get; private set; }
    }
}