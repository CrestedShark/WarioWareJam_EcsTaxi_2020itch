﻿using UnityEngine;

public abstract class BaseMinigame : MonoBehaviour
{
   public virtual bool Active
   {
      get => _active;
      set => SetActive(value);
   }

   public virtual float MinigameTimer
   {
      get => Toolbox.Instance.MiniManager.timer.currentTime;
      set
      {
         Toolbox.Instance.MiniManager.timer.Reset();
         Toolbox.Instance.MiniManager.timer.timeLimit = value;
         _timerSet = true;
      }
   }

   private bool _timerSet = false;
   private bool _active = false;

   private void SetActive(bool value)
   {
      _active = value;
      if (value && _timerSet)
      {
         Toolbox.Instance.MiniManager.timer.Activate();
      }
      else if (!_timerSet)
      {
         Debug.LogError("Minigame time limit not set by BaseMinigame " + this);
      }
   }

   public abstract void InitMinigame();
}
