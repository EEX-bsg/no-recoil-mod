using System;
using System.Collections;
using Modding;
using UnityEngine;
using NoRecoilModNS;
using Modding.Blocks;

namespace NoRecoilModNS
{
    class NoRecoilModCore:MonoBehaviour
    {
        public void Awake() {
            StartCoroutine(CheckVersion());
            Events.OnBlockInit += AddScript;
        }

        private void AddScript(Block block) {
            BlockBehaviour internalObject = block.BuildingBlock.InternalObject;
            if(internalObject.BlockID == 11)//BlockID 11:Cannon
            {
                try
                {
                    internalObject.gameObject.AddComponent<CannonControle>();
                }
                catch
                {
                    Debug.LogError("NoRecoilModCore: Failed to apply cannon script");
                }
            }
        }
        private IEnumerator CheckVersion()
        {
            //何となく1秒待機(処理順用)
            yield return new WaitForSeconds(1f);
            //Guidにはは自分のMod.xml内のIDを入れること
            Debug.Log("NoRecoilMod Version " + Mods.GetVersion(new Guid("ed3ed2d0-a918-45e2-828e-6cc08df2784a")));
        }
    }
}
