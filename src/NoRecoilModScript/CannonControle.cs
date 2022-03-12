using System;
using System.Collections;
using Modding;
using UnityEngine;
using NoRecoilModNS;
using Modding.Blocks;

namespace NoRecoilModNS
{
    class CannonControle:MonoBehaviour
    {
            public CanonBlock canonblock;
            private Rigidbody rigidbody;
            private bool isFirstFrame = true;
            private float maxAngularVelocity = 10000f;
            private float knockBackSpeedDefault = 100f;
            private float randomDelayDefault = 0.2f;
            private float intervalDefault = 0.05f;
            private float massDefault = 1.5f;
            private float randomDelay = 0f;
            private float interval = 0.05f;
            private float mass = 0.3f;

            public void Awake()
            {
                if(StatMaster.isMP)
                {
                    if(StatMaster.isHosting || StatMaster.isLocalSim)
                    {
                        AwakeBlock();
                    }
                }
                else
                {
                    AwakeBlock();
                }
            }
            public void FixedUpdate()
            {
                if(StatMaster.levelSimulating)//シミュレートしているとき
                {
                    if(isFirstFrame)//最初のフレームはスキップしたほうがいいらしい(少なくともBesiegeのコードはそうなってた...はず...)
                    {
                        isFirstFrame = false;
                        if(StatMaster.isMP)
                        {
                            if(StatMaster.isHosting || StatMaster.isLocalSim)//鯖に入ってるand(ホストorローカルシミュ)  クライアントのグローバルは物理処理しないから回避する必要がある
                            {
                                OnSimulateStartBlock();
                            }
                        }
                        else
                        {
                            OnSimulateStartBlock();//鯖入ってなければ問答無用で実行でOK
                        }
                    }
                }
            }
            //なぜAwakeとStartSimで分けたのかEEXはわからないが、ふねmodがそうだったのでそうする
            private void AwakeBlock()
            {
                rigidbody = base.GetComponent<Rigidbody>();
                rigidbody.maxAngularVelocity = maxAngularVelocity;
            }
            private void OnSimulateStartBlock()
            {
                canonblock = base.GetComponent<CanonBlock>();
                canonblock.knockbackSpeed = knockBackSpeedDefault / canonblock.StrengthSlider.Value;
                canonblock.randomDelay = randomDelay;
                canonblock.interval = interval;
                rigidbody.mass = mass;
            }
    }
}
