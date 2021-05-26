﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Services.AnimatedColor;
using ModLibsNPCDialogue.Services.Dialogue;


namespace HamstarHelpers {
	/// @private
	partial class ModHelpersNPC : GlobalNPC {
		public override void PostDraw( NPC npc, SpriteBatch sb, Color drawColor ) {
			if( !npc.townNPC ) {
				return;
			}

			DynamicDialogueHandler handler = DialogueEditor.GetDynamicDialogueHandler( npc.type );
			if( handler?.IsShowingAlert.Invoke() ?? false ) {
				this.DrawAlertFlag( npc, sb );
			}
		}


		////

		private void DrawAlertFlag( NPC npc, SpriteBatch sb ) {
			Vector2 scrPos = npc.Center - Main.screenPosition;
			scrPos.X -= 4;
			scrPos.Y -= ( npc.height / 2 ) + 56;

			Utils.DrawBorderStringFourWay(
				sb: sb,
				font: Main.fontMouseText,
				text: "!",
				x: scrPos.X,
				y: scrPos.Y,
				textColor: AnimatedColors.Alert.CurrentColor,
				borderColor: Color.Black,
				origin: default( Vector2 ),
				scale: 2f
			);
		}
	}
}
