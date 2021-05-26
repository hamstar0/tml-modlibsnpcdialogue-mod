using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace ModLibsNPCDialogue {
	/// @private
	partial class ModLibsNPCDialogueMod : Mod {
		public static ModLibsNPCDialogueMod Instance { get; private set; }



		////////////////

		public override void Load() {
			ModLibsNPCDialogueMod.Instance = this;
		}

		////

		public override void Unload() {
			try {
				LogLibraries.Alert( "Unloading mod..." );
			} catch { }

			ModLibsNPCDialogueMod.Instance = null;
		}
	}
}
