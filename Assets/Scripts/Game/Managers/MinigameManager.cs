﻿using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
   public enum MinigameState
   {
      None,
      Win,
      Lose,
   }

   public bool Initialized { get => _initialized; }

   //============MinigameVars=============
   public AsyncOperation minigameScene;

   public BaseMinigame minigameScript;
   public CanvasManager canvas;
   public MinigameState result;
   public MinigameTimer timer;
   public MinigameInformation minigameInfo;
   public Scene scene;

   //=====================================

   private List<string> minigamesPlayed = new List<string>();

   private bool _initialized = false;
   private Dictionary<string, MinigameInformation> minigameList = new Dictionary<string, MinigameInformation>();

   [SerializeField]
   private List<string> miniSceneName;

   [SerializeField]
   private List<Sprite> miniVerb;

   //==========================================================================
   public void Init()
   {
      InitList();
      timer.Init();
      timer.Reset();
      _initialized = true;
   }

   //==========================================================================
   private void InitList()
   {
      for (int ix = 0; ix < miniSceneName.Count; ix++)
      {
         minigameList.Add(miniSceneName[ix], new MinigameInformation(miniSceneName[ix], miniVerb[ix]));
      }
   }

   //==========================================================================
   public void LoadNextMinigame()
   {
      //Additively load the next mini-game scene
      var sceneParameters = new LoadSceneParameters(LoadSceneMode.Additive);
      int idx = 0;
      string key = "";
      if (minigamesPlayed.Count == miniSceneName.Count - 1)
      {
         minigamesPlayed.Clear();
      }
      do
      {
         do
         {
            idx = Random.Range(0, miniSceneName.Count);
         }
         while (idx == 4);
         key = miniSceneName[idx];
      } while (HasBeenPlayed(key));
      minigamesPlayed.Add(key);
      minigameInfo = minigameList[key];
      minigameScene = SceneManager.LoadSceneAsync(minigameInfo.sceneName, sceneParameters);
      minigameScene.completed += (operation) =>
      {
         scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
      };
      minigameScene.allowSceneActivation = false;
   }

   private bool HasBeenPlayed(string key)
   {
      bool val = false;
      foreach (string str in minigamesPlayed)
      {
         if (str.Equals(key))
         {
            val = true;
         }
      }
      return val;
   }

   //==========================================================================
   public void UnloadCurrentMinigame()
   {
      minigameScene = null;
      minigameScript.Active = false;
      minigameScript = null;
      result = MinigameState.None;
      timer.Reset();
      minigameInfo = null;
      SceneManager.UnloadSceneAsync(scene);
      Resources.UnloadUnusedAssets();
   }

   //==========================================================================
   public void InitScene()
   {
      // run awake/start initializations
      foreach (var root in scene.GetRootGameObjects())
      {
         root.BroadcastMessage("Awake", SendMessageOptions.DontRequireReceiver);
      }
      foreach (var root in scene.GetRootGameObjects())
      {
         root.BroadcastMessage("Start", SendMessageOptions.DontRequireReceiver);
      }
   }

   //==========================================================================
   public void InitMinigame()
   {
      minigameScript.InitMinigame();
   }

   //==========================================================================
   private void Start()
   {
      Toolbox.Instance.AttachMinigameManager(this);

      if (miniSceneName.Count != miniVerb.Count)
      {
         Debug.LogError("Imbalanced parameter count for " + this.ToString() + " name: " + miniSceneName.Count + " verb: " + miniVerb.Count);
      }

      foreach (string str in miniSceneName)
      {
         if (str.Equals(null) || str.Equals(""))
         {
            Debug.LogError("Null/Empty value for element " + miniSceneName.IndexOf(str) + " in " + miniSceneName.ToString());
         }
      }
      foreach (Sprite spr in miniVerb)
      {
         if (spr.Equals(null))
         {
            Debug.LogError("Null/Empty value for element " + miniVerb.IndexOf(spr) + " in " + miniVerb.ToString());
         }
      }
   }
}
