﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.DotNET;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsCore.Libraries.TModLoader;
using ModLibsMaps.Services.Maps;


namespace ModLibsNPCDialogue.Services.Dialogue {
	/// <summary>
	/// Provides a service for adding or removing town NPC chats (based on weight values).
	/// </summary>
	public partial class DialogueEditor : ILoadable {
		internal static void UpdateAlertIconsOnMap() {
			var dialogEdit = TmlLibraries.SafelyGetInstance<DialogueEditor>();
			IDictionary<int, DynamicDialogueHandler> dynHandlers = dialogEdit.DynamicHandlers;
			if( dynHandlers.Count == 0 && !dialogEdit.MarkerCleanupNeeded ) {
				return;
			}

			dialogEdit.MarkerCleanupNeeded = false;

			var uniques = new HashSet<int>();
			IDictionary<int, int> townNpcWhos = Main.npc
				.SafeWhere( n => {
					if( n?.active != true ) { return false; }
					if( !n.townNPC && n.type != NPCID.OldMan ) { return false; }
					if( uniques.Contains( n.type ) ) { return false; }

					uniques.Add( n.type );

					return true;
				} )
				.ToDictionary( n => n.type, n => n.whoAmI );

			foreach( (int townNpcType, int townNpcWho) in townNpcWhos ) {
				string alertId = "ModLibsDialogueAlert_" + townNpcType;

				if( dynHandlers.ContainsKey(townNpcType) && (dynHandlers[townNpcType].IsShowingAlert?.Invoke() ?? false) ) {
					NPC npc = Main.npc[townNpcWho];

					MapMarkersAPI.SetFullScreenMapMarker(
						id: alertId,
						tileX: ((int)npc.Center.X / 16),
						tileY: ((int)npc.Center.Y / 16) - 3,
						icon: Main.chatTexture,
						scale: 1.35f
					);
				} else {
					MapMarkersAPI.RemoveFullScreenMapMarker( alertId );
				}
			}
		}



		////////////////

		private IDictionary<int, IList<(float Weight, string Chat)>> AddedChats = new Dictionary<int, IList<(float, string)>>();
		private IDictionary<int, IList<string>> RemovedChatFlatPatterns = new Dictionary<int, IList<string>>();

		private IDictionary<int, DynamicDialogueHandler> DynamicHandlers = new ConcurrentDictionary<int, DynamicDialogueHandler>();

		private bool MarkerCleanupNeeded = false;



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() { }
	}
}
