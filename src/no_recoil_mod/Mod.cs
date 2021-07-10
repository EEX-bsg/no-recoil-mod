using System;
using System.Collections;
using Modding;
using UnityEngine;

namespace no_recoil_mod
{
	public class Mod : ModEntryPoint
	{
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
            UnityEngine.Object.DontDestroyOnLoad(SingleInstance<NotRecoilModule>.Instance);

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
                //âΩÇ∆Ç»Ç≠1ïbë“ã@(èàóùèáóp)
                yield return new WaitForSeconds(1f);
                //GuidÇ…ÇÕÇÕé©ï™ÇÃMod.xmlì‡ÇÃIDÇì¸ÇÍÇÈÇ±Ç∆
                Log("Version " + Mods.GetVersion(new Guid("ed3ed2d0-a918-45e2-828e-6cc08df2784a")));
            }
            public void Awake()
            {
                StartCoroutine(CheckVersion());
            }
        }
    }
}
