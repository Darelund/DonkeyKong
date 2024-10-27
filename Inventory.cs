using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
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
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].currentDirection = _player.currentDirection;
                Items[i].Position = GetSlotPosition(Items[i].Type);
                Items[i].Update(gameTime);
            }
            if (InputManager.WearButton())
            {
                foreach (var item in Items)
                {
                    if (item.Type == ItemType.Wearable && Slots[1].ItemSlot == null)
                    {
                        Slots[1].ItemSlot = item;
                       // Slots[1].ItemSlot.Position = Slots[1].Position;
                        item.OnExpired += Slots[1].ClearSlot;
                        Debug.WriteLine($"Trying to wear: {item.Type}");
                    }
                }
            }
            if (InputManager.ConsumeButton())
            {
                foreach (var item in Items)
                {
                    if (item.Type == ItemType.Consumable && Slots[2].ItemSlot == null)
                    {
                        Slots[2].ItemSlot = item;
                        item.OnExpired += Slots[2].ClearSlot;
                    }
                    Debug.WriteLine($"Type: {item.Type}");
                }
            }
            if (InputManager.UseButton())
            {
                foreach (var item in Items)
                {
                    if (item.Type == ItemType.Weapon && Slots[3].ItemSlot == null)
                    {
                        Slots[3].ItemSlot = item;
                        item.OnExpired += Slots[2].ClearSlot;
                    }
                    Debug.WriteLine($"Type: {item.Type}");
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
          
            return type switch
            {
                ItemType.Wearable => new Vector2((int)_player.Position.X, (int)_player.Position.Y - (int)_player.Origin.Y * (int)_player.Size),
                ItemType.Consumable => new Vector2(((int)_player.Position.X + 25), (int)_player.Position.Y - (int)_player.Origin.Y * (int)_player.Size / 2 + 10),
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
            }
        }
    }

}
