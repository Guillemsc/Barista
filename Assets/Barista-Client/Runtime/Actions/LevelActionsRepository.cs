using System;
using System.Collections.Generic;

namespace Barista.Client.Actions
{
    public class LevelActionsRepository
    {
        private readonly Dictionary<Type, IAction> actions = new Dictionary<Type, IAction>();

        public void ClearActions()
        {
            actions.Clear();
        }

        public void AddAction<T>(T action) where T : IAction
        {
            Type actionType = typeof(T);

            actions.Add(actionType, action);
        }

        public void SwapAction<T>(T action) where T : IAction
        {
            Type actionType = typeof(T);

            bool found = actions.ContainsKey(actionType);

            if(found)
            {
                actions[actionType] = action;
            }
            else
            {
                AddAction(action);
            }
        }

        public void RemoveAction<T>() where T : IAction
        {
            Type actionType = typeof(T);

            actions.Remove(actionType);
        }

        public bool TryGetAction<T>(out T action) where T : class
        {
            Type actionType = typeof(T);

            bool found = actions.TryGetValue(actionType, out IAction actionObject);

            if(!found)
            {
                action = null;
                return false;
            }

            action = (T)actionObject;
            return true;
        }
    }
}