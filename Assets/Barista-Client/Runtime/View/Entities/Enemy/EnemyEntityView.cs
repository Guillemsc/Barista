﻿using Juce.CoreUnity.Architecture;
using UnityEngine;

namespace Barista.Client.View.Entities.Enemy
{
    public class EnemyEntityView : MonoBehaviour, IEntityView<string>, IMovableEntityView
    {
        public string TypeId { get; private set; }
        public int InstanceId { get; private set; }

        public Transform Transform => gameObject.transform;

        public void Init(string typeId, int instanceId)
        {
            TypeId = typeId;
            InstanceId = instanceId;
        }

        public void CleanUp()
        {

        }
    }
}
