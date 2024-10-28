using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public enum ItemType
    {
        Wearable,
        Consumable,
        Weapon
    }
    public enum ItemState
    {
        pickupState,
        usingState
    }
    public class Item : AnimatedGameObject
    {
        public ItemType Type { get; }
        public ItemState state { get; private set; } = ItemState.pickupState;
        private float _lifeTimeDuration = 5f;
        private float _remainingTimeLeft = 0;

        private int _minScore;
        private int _maxScore;

        public event Action<Item> OnExpired;

        public Item(Texture2D texture, Vector2 position, float speed, Color color, float rotation, float size, float layerDepth, Vector2 origin, Dictionary<string, AnimationClip> animationClips, ItemType type, int minScore, int maxScore) : base(texture, position, speed, color, rotation, size, layerDepth, origin, animationClips)
        {
            Type = type;
            _minScore = minScore;
            _maxScore = maxScore;

            _remainingTimeLeft = _lifeTimeDuration;
        }
        public override void OnCollision(GameObject gameObject)
        {
            //Maybe add it back to a pool and respawn it
            if(gameObject is PlayerController)
            {
                Random random = new Random();
                var randomScore = random.Next(_minScore, _maxScore);

                PlayerController playerInventory = (PlayerController)gameObject;
                playerInventory.Inventory.Items.Add(this);
                state = ItemState.usingState;
                ScoreManager.UpdateScore(randomScore);
                int resetRotation = 0;
                Rotation = resetRotation;
                AudioManager.PlaySoundEffect("CoinPickupSound");
            }
            GameManager.GameObjects.Remove(this);
        }
        public override void Update(GameTime gameTime)
        {
            if(state == ItemState.pickupState)
            Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
            {
                _remainingTimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_remainingTimeLeft <= 0)
                {
                    Debug.WriteLine("Item expired");
                    OnExpired?.Invoke(this);
                }
                ItemEffect();
            }
            base.Update(gameTime);
        }
        //I should break this up into different classes so I have Weapon, Consumable and Wearable classes that derive/inherit from/this class but yeah Will do if I have time(It feels like a lot of work)
        private void ItemEffect()
        {
            switch (Type)
            {
                case ItemType.Wearable:
                    break;
                case ItemType.Consumable:
                    Rotation = -1.57f;
                    break;
                case ItemType.Weapon:
                    break;
            }
        }
    }
}
