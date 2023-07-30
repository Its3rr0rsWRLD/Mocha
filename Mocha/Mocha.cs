using System;
using BepInEx;
using Mocha.HarmonyPatches;
using Mocha.Utils;
using UnityEngine;
using System.Linq;
using Photon.Realtime;
using Photon.Pun;

namespace Mocha
{

	[BepInPlugin("org.thaterror404.gorillatag.Mocha", "Mocha", "1.0.0")]
    public class Mocha : BaseUnityPlugin
    {
        static Events events = new Events();

        void Start()
        {
            DontDestroyOnLoad(this);
            RoomUtils.RoomCode = RoomUtils.RandomString(6); // Generate a random room code in case we need it

            GameObject dataObject = new GameObject();
            DontDestroyOnLoad(dataObject);
            gameObject.AddComponent<MochaNetworkController>();
            gameObject.AddComponent<AssetHandler>();

            Events.GameInitialized += PostInitialized;

            MochaNetworkController.events = events;
            PostInitializedPatch.events = events;

            MochaPatches.ApplyHarmonyPatches();
        }

        void PostInitialized(object sender, EventArgs e)
		{
            // GameObject.DontDestroyOnLoad(this.gameObject);
            var go = new GameObject("CustomGamemodesManager");
            GameObject.DontDestroyOnLoad(go);
            var gmm = go.AddComponent<GamemodeManager>();
            this.gameObject.GetComponent<MochaNetworkController>().gameModeManager = gmm;
		}
    }
}
