﻿using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsCore.Libraries.TModLoader;


namespace ModLibsNPCDialogue.Services.Dialogue {
	/// <summary>
	/// Allows editing the current NPC dialogue message.
	/// </summary>
	public class DynamicDialogueHandler {
		/// <summary>
		/// Accepts the current chat message and returns an (optionally) edited version.
		/// </summary>
		public Func<string, string> GetDialogue { get; }

		/// <summary>
		/// Indicates if the current NPC is showing an alert icon.
		/// </summary>
		public Func<bool> IsShowingAlert { get; }


		/// <summary></summary>
		/// <param name="getDialogue"></param>
		/// <param name="isShowingAlert"></param>
		public DynamicDialogueHandler( Func<string, string> getDialogue, Func<bool> isShowingAlert ) {
			this.GetDialogue = getDialogue;
			this.IsShowingAlert = isShowingAlert;
		}
	}




	/// <summary>
	/// Provides a service for adding or removing town NPC chats (based on weight values).
	/// </summary>
	public partial class DialogueEditor : ILoadable {
		/// <summary>
		/// Retrieves the current dynamic dialogue message handler.
		/// </summary>
		/// <param name="npcType"></param>
		/// <returns></returns>
		public static DynamicDialogueHandler GetDynamicDialogueHandler( int npcType ) {
			DialogueEditor de = TmlLibraries.SafelyGetInstance<DialogueEditor>();
			return de.DynamicHandlers.GetOrDefault( npcType );
		}

		/// <summary>
		/// Sets the current dynamic dialogue message handler for a given NPC.
		/// </summary>
		/// <param name="npcType"></param>
		/// <param name="handler"></param>
		public static void SetDynamicDialogueHandler( int npcType, DynamicDialogueHandler handler ) {
			DialogueEditor de = TmlLibraries.SafelyGetInstance<DialogueEditor>();
			de.DynamicHandlers[ npcType ] = handler;
		}

		/// <summary>
		/// Removes any dynamic dialogue message handler for a given NPC.
		/// </summary>
		/// <param name="npcType"></param>
		/// <returns></returns>
		public static bool RemoveDynamicDialogueHandler( int npcType ) {
			DialogueEditor de = TmlLibraries.SafelyGetInstance<DialogueEditor>();

			de.MarkerCleanupNeeded = true;

			return de.DynamicHandlers.Remove( npcType );
		}
	}
}
