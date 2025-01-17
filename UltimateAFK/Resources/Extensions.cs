using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Attachments;
using PluginAPI.Core;
using System.Collections.Generic;
using System.Linq;

namespace UltimateAFK.Resources
{
    public static class Extensions
    {
        /// <summary>
        /// Adds several items at the same time to a player.
        /// </summary>
        public static void SendItems(this Player player, List<ItemType> types)
        {
            foreach (var item in types)
            {
                player.AddItem(item);
            }
        }

        /// <summary>
        /// Gives the ammunition you want a player to have.
        /// </summary>
        public static void SendAmmo(this Player ply, Dictionary<ItemType, ushort> ammo)
        {
            foreach (var ammoItem in ammo)
            {
                ply.SetAmmo(ammoItem.Key, ammoItem.Value);
            }
        }


        public static List<ItemType> GetItems(this Player ply)
        {
            var items = ply.ReferenceHub.inventory.UserInventory.Items;
            var returnitems = new List<ItemType>();

            foreach (var i in items.Values)
            {
                if (i.ItemTypeId is ItemType.Ammo9x19 or ItemType.Ammo12gauge or ItemType.Ammo44cal or ItemType.Ammo556x45 or ItemType.Ammo762x39) continue;

                returnitems.Add(i.ItemTypeId);
            }

            return returnitems;
        }

        public static Dictionary<ItemType, ushort> GetAmmo(Player player)
        {
            var result = new Dictionary<ItemType, ushort>();

            foreach (var ammo in player.ReferenceHub.inventory.UserInventory.ReserveAmmo)
            {
                result.Add(ammo.Key, ammo.Value);
            }

            return result;
        }

        /// <summary>
        /// Applies player attachments.
        /// </summary>
        /// <param name="ply"></param>
        public static void ApplyAttachments(this Player ply)
        {
            var item = ply.Items.Where(i => i is Firearm);

            foreach (var fire in item)
            {
                if (fire is Firearm fireArm)
                {
                    if (AttachmentsServerHandler.PlayerPreferences.TryGetValue(ply.ReferenceHub, out var value) && value.TryGetValue(fireArm.ItemTypeId, out var value2))
                        fireArm.ApplyAttachmentsCode(value2, reValidate: true);
                    var firearmStatusFlags = FirearmStatusFlags.MagazineInserted;
                    if (fireArm.HasAdvantageFlag(AttachmentDescriptiveAdvantages.Flashlight))
                        firearmStatusFlags |= FirearmStatusFlags.FlashlightEnabled;

                    fireArm.Status = new FirearmStatus(fireArm.AmmoManagerModule.MaxAmmo, firearmStatusFlags, fireArm.GetCurrentAttachmentsCode());
                }
            }
        }

        public static void ReloadAllWeapons(this Player ply)
        {
            var item = ply.Items.Where(i => i is Firearm);

            foreach (var weapon in item)
            {
                if (weapon is Firearm firearm)
                {
                    firearm.Status = new FirearmStatus(firearm.AmmoManagerModule.MaxAmmo, firearm.Status.Flags,
                        firearm.GetCurrentAttachmentsCode());
                }
            }
        }
    }
}