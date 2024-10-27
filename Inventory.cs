using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using static DonkeyKong.Inventory;

namespace DonkeyKong
{
    public class Inventory
    {
        public List<Item> Items { get; set; }
        private Dictionary<int, Slot> Slots;
        private PlayerController _player;
        public Inventory(PlayerController player)
        {
            Items = new List<Item>();
            Slots = new Dictionary<int, Slot>()
            {
                { 1, new Slot(ItemType.Wearable, this) },
                { 2, new Slot(ItemType.Consumable, this) },
                { 3, new Slot(ItemType.Weapon, this) },
            };
            _player = player;
        }

        public void Update(GameTime gameTime)
        {
            //To keep hidden/active items at the right position
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].currentDirection = _player.currentDirection;
                Items[i].Position = GetSlotPosition(Items[i].Type);
            }
            //Display active items
            for (var i = 1; i <= Slots.Keys.Count; i++)
            {
                if (Slots.TryGetValue(i, out Slot slot))
                {
                    if (slot.ItemSlot != null)
                    {
                        slot.ItemSlot.Update(gameTime);
                    }
                }
            }
            if (InputManager.WearButton())
            {
                int slotOne = 1;
                foreach (var item in Items)
                {
                    if (item.Type == ItemType.Wearable && Slots[slotOne].ItemSlot == null)
                    {
                        Slots[slotOne].ItemSlot = item;
                       // Slots[1].ItemSlot.Position = Slots[1].Position;
                        item.OnExpired += Slots[slotOne].ClearSlot;
                        Debug.WriteLine($"Trying to wear: {item.Type}");
                    }
                }
            }
            if (InputManager.ConsumeButton())
            {
               int slotTwo = 2;
                foreach (var item in Items)
                {
                    if (item.Type == ItemType.Consumable && Slots[slotTwo].ItemSlot == null)
                    {
                        Slots[slotTwo].ItemSlot = item;
                        item.OnExpired += Slots[slotTwo].ClearSlot;
                    Debug.WriteLine($"Type: {item.Type}");
                    }
                }
            }
            if (InputManager.UseButton())
            {
               int slotThree = 3;
                foreach (var item in Items)
                {
                    if (item.Type == ItemType.Weapon && Slots[slotThree].ItemSlot == null)
                    {
                        Slots[slotThree].ItemSlot = item;
                        item.OnExpired += Slots[slotThree].ClearSlot;
                    Debug.WriteLine($"Type: {item.Type}");
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 1; i <= Slots.Keys.Count; i++)
            {
                if (Slots.TryGetValue(i, out Slot slot))
                {
                    if (slot.ItemSlot != null)
                    {
                        slot.ItemSlot.Draw(spriteBatch);
                        Debug.WriteLine("Draw it");
                    }
                }
            }
        }
        private Vector2 GetSlotPosition(ItemType type)
        {
            int consumFírstOffset = 2;
            Vector2 consumableSecondOffset = new Vector2 (25, 10);
            return type switch
            {
                ItemType.Wearable => new Vector2((int)_player.Position.X, (int)_player.Position.Y - (int)_player.Origin.Y * (int)_player.Size),
                ItemType.Consumable => new Vector2(((int)_player.Position.X + consumableSecondOffset.X), (int)_player.Position.Y - (int)_player.Origin.Y * (int)_player.Size / consumFírstOffset + consumableSecondOffset.Y),
                ItemType.Weapon => Vector2.Zero,
                _ => new Vector2((int)_player.Position.X, (int)_player.Position.Y)
            };
        }
        public class Slot
        {
            public Item ItemSlot;
            public ItemType Type { get; }
           // public Vector2 Position { get; set; }
            private Inventory _inventory;

            public Slot(ItemType type, Inventory inventory)
            {
                Type = type;
                _inventory = inventory;
            }
            public void ClearSlot(Item item)
            {
                ItemSlot = null;
                _inventory.Items.Remove(item);
                Debug.WriteLine(_inventory.Items.Count);
            }
        }
    }

}
