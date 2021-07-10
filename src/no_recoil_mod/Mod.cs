using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using Modding.Blocks;
using UnityEngine;

namespace no_recoil_mod
{
	public class Mod : ModEntryPoint
	{
        GameObject mod;
        public static void Log(string message)
        {
            ModConsole.Log("NoRecoil Log:" + message);
        }
        public static void Warning(string message)
        {
            ModConsole.Log("NoRecoil Warning:" + message);
        }
        public static void Error(string message)
        {
            ModConsole.Log("NoRecoil Error:" + message);
        }
        public override void OnLoad()
		{
            // Called when the mod is loaded.
            mod = new GameObject("NoRecoilMod");
            UnityEngine.Object.DontDestroyOnLoad(SingleInstance<NotRecoilModule>.Instance);
            SingleInstance<BlockSelector>.Instance.transform.parent = mod.transform;
            UnityEngine.Object.DontDestroyOnLoad(mod);
        }
        public class NotRecoilModule : SingleInstance<NotRecoilModule>
        {
            public override string Name
            {
                get
                {
                    return "ETCMmodule";

                }
            }
            private IEnumerator CheckVersion()
            {
                //何となく1秒待機(処理順用)
                yield return new WaitForSeconds(1f);
                //Guidにはは自分のMod.xml内のIDを入れること
                Log("Version " + Mods.GetVersion(new Guid("ed3ed2d0-a918-45e2-828e-6cc08df2784a")));
            }
            public void Awake()
            {
                StartCoroutine(CheckVersion());
            }
        }
        public class BlockSelector : SingleInstance<BlockSelector>
        {
            public override string Name
            {
                get
                {
                    return "BlockSelector";
                }
            }
            public Dictionary<int, Type> BlockDict = new Dictionary<int, Type>
            {
			    // スタートブロック
			    {11, typeof(CannonControle) },
            };
            public void Awake()
            {
                Events.OnBlockInit += new Action<Block>(AddScript);
            }
            public void AddScript (Block block)
            {
                BlockBehaviour internalObject = block.BuildingBlock.InternalObject;

                if (BlockDict.ContainsKey(internalObject.BlockID))
                {
                    Type type = BlockDict[internalObject.BlockID];
                    try
                    {
                        if(internalObject.GetComponent(type) == null)
                        {
                            internalObject.gameObject.AddComponent(type);

                        }
                    }
                    catch
                    {
                        Error("AddScript Error");
                    }
                    return;
                }
            }
        }
        public class CannonControle : MonoBehaviour
        {
            public CanonBlock canonblock;
            private Rigidbody rigidbody;
            public BlockBehaviour BB { get; internal set; }
            private bool isFirstFrame = true;
            private float knockBackSpeedDefault = 100f;
            private float randomDelayDefault = 0.2f;
            private float intervalDefault = 0.05f;
            private float massDefault = 1.5f;
            private float randomDelay = 0f;
            private float interval = 0.05f;
            private float mass = 0.3f;

            private void Awake()
            {
                BB = GetComponent<BlockBehaviour>();
                if(!StatMaster.isClient || StatMaster.isLocalSim)
                {
                    rigidbody = GetComponent<Rigidbody>();
                }
            }
            private void Update()
            {
                if (BB.isSimulating)
                {
                    if (isFirstFrame)
                    {
                        isFirstFrame = false;
                        OnSimulateStart();
                    }
                }
            }
            public void OnSimulateStart()
            {
                if (StatMaster.isClient)
                {
                    return;
                }
                canonblock = GetComponent<CanonBlock>();
                canonblock.knockbackSpeed = 0.0f;
                canonblock.randomDelay = randomDelay;
                canonblock.interval = interval;
                if(!StatMaster.isClient || StatMaster.isLocalSim)
                {
                    rigidbody.mass = mass;
                }
            }
        }
    }
}
