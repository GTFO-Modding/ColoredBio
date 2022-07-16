using BepInEx.IL2CPP.Utils;
using Enemies;
using Il2CppInterop.Runtime.Attributes;
using Il2CppInterop.Runtime.InteropTypes.Fields;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ColoredBio
{
    internal sealed class BioColorHandler : MonoBehaviour
    {
        public Il2CppReferenceField<EnemyAgent> Owner;

        private EnemyAgent _Owner;
        private BioState _LastState = BioState.None;
        private Coroutine _ColorRoutine = null;

        void Start()
        {
            _Owner = Owner.Get();
        }

        void FixedUpdate()
        {
            if (!_Owner.Alive)
                return;

            var newState = GetState();
            if (newState != _LastState)
            {
                if (_LastState == BioState.None)
                {
                    _Owner.ScannerColor = BioColorInfo.GetColorByState(newState);
                }
                else
                {
                    var from = BioColorInfo.GetColorByState(_LastState);
                    var to = BioColorInfo.GetColorByState(newState);

                    if (_ColorRoutine != null) StopCoroutine(_ColorRoutine);
                    _ColorRoutine = this.StartCoroutine(UpdateColor(from, to));
                }
                _LastState = newState;
            }
        }

        [HideFromIl2Cpp]
        IEnumerator UpdateColor(Color from, Color to)
        {
            var time = 0.0f;
            var duration = 0.25f;
            var inv = 1.0f / duration;
            while (time <= 0.25f)
            {
                var p = time * inv;
                _Owner.ScannerColor = Color.Lerp(from, to, p);

                time += Time.deltaTime;
                yield return null;
            }

            _Owner.ScannerColor = to;
            _ColorRoutine = null;
        }

        [HideFromIl2Cpp]
        BioState GetState()
        {
            var loco = _Owner.Locomotion;
            switch (loco.CurrentStateEnum)
            {
                case ES_StateEnum.ClimbLadder:
                    return _LastState;

                case ES_StateEnum.StuckInGlue:
                    return BioState.Hibernate;

                case ES_StateEnum.Hibernate:
                    if (loco.Hibernate.m_heartbeatActive)
                    {
                        return BioState.Heartbeat;
                    }
                    return _Owner.IsHibernationDetecting ? BioState.Detect : BioState.Hibernate;

                case ES_StateEnum.ScoutDetection:
                    return BioState.ScoutTentacle;

                case ES_StateEnum.PathMove:
                    if (_Owner.AI.m_scoutPath == null)
                    {
                        return BioState.Aggressive;
                    }

                    if (loco.ScoutScream.m_state == ES_ScoutScream.ScoutScreamState.Done)
                    {
                        return BioState.Aggressive;
                    }
                    return  BioState.Scout;

                default:
                    return BioState.Aggressive;
            }
        }
    }
}
