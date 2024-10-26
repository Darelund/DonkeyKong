using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class GameObjectFactory
    {
        public GameObject CreateGameObjectFromType(string objectType, List<string> objectData)
        {
            // Create objects based on their type
            switch (objectType)
            {
                case "EnemyController":
                    Debug.WriteLine("An enemy is created(Factory)");
                    return CreateEnemyController(objectData);
                case "PlayerController":
                    return CreatePlayerController(objectData);
                case "PickUp":
                //    return CreatePickUp(objectData);
                default:
                    Debug.WriteLine("Unknown object type: " + objectType);
                    return null;
            }
        }
        private EnemyController CreateEnemyController(List<string> data)
        {
            // Parse the data and initialize the object
            // Assume data contains all the needed parameters in the expected order

            // 1. Parse general properties (like sprite, position, color, etc.)
            string sprite = data[0];  // Texture name or path
            string[] positionParts = data[1].Split(',');
            int xPos = int.Parse(positionParts[0].Trim());
            int yPos = int.Parse(positionParts[1].Trim());
            Vector2 position = new Vector2(xPos, yPos);

            string colorName = data[2].Trim();
            Color color = colorName switch
            {
                "white" => Color.White,
                "red" => Color.Red,
                "blue" => Color.Blue,
                "green" => Color.Green,
                _ => Color.White // Default color if not found
            };
            
            float rotation = float.Parse(data[3].Trim());  // Rotation
            float size = float.Parse(data[4].Trim());      // Size/scale
            float layerDepth = float.Parse(data[5].Trim());  // Layer depth

            string[] originParts = data[6].Split(',');
            xPos = int.Parse(originParts[0].Trim());
            yPos = int.Parse(originParts[1].Trim());
            Vector2 origin = new Vector2(xPos, yPos);
            // 2. Parse animation clips (loop through the animation data in the input list)
            var animationClips = new Dictionary<string, AnimationClip>();

            // Example: WalkAnimation:5,86,18,18|23,86,18,18|41,86,18,18;4f
            for (int i = 7; i < data.Count; i++)
            {
                string animationData = data[i];
                if (!string.IsNullOrWhiteSpace(animationData))
                {
                    // Split each animation data (format: "Name:rect1|rect2|...;speed")
                    string[] animationParts = animationData.Split(':');
                    string animationName = animationParts[0].Trim();

                    string[] rectsAndSpeed = animationParts[1].Split(';');
                    string[] rectStrings = rectsAndSpeed[0].Split('|'); // Each rectangle info

                    // Create an array of rectangles for the animation
                    Rectangle[] frames = rectStrings.Select(rectStr =>
                    {
                        string[] rectComponents = rectStr.Split(',');
                        int rectX = int.Parse(rectComponents[0].Trim());
                        int rectY = int.Parse(rectComponents[1].Trim());
                        int rectWidth = int.Parse(rectComponents[2].Trim());
                        int rectHeight = int.Parse(rectComponents[3].Trim());

                        return new Rectangle(rectX, rectY, rectWidth, rectHeight);
                    }).ToArray();

                    // Parse the speed of the animation
                    float animationSpeed = float.Parse(rectsAndSpeed[1].Trim());

                    // Add the parsed animation to the dictionary
                    animationClips[animationName] = new AnimationClip(frames, animationSpeed);
                }
            }
            // 3. Create and return the EnemyController object with the parsed data
            return new EnemyController(
                ResourceManager.GetTexture(sprite),  // Texture loading handled elsewhere
                position,
                color,
                rotation,
                size,
                layerDepth,
                origin,
                animationClips  // Add the parsed animation clips
            );
        }
        private PlayerController CreatePlayerController(List<string> data)
        {
            // 1. Parse general properties (like sprite, position, color, etc.)
            string sprite = data[0];  // Texture name or path
            string[] positionParts = data[1].Split(',');
            int xPos = int.Parse(positionParts[0].Trim());
            int yPos = int.Parse(positionParts[1].Trim());
            Vector2 position = new Vector2(xPos, yPos);

            float speed = float.Parse(data[2]);
            string colorName = data[3].Trim();
            Color color = colorName switch
            {
                "white" => Color.White,
                "red" => Color.Red,
                "blue" => Color.Blue,
                "green" => Color.Green,
                _ => Color.White // Default color if not found
            };

            float rotation = float.Parse(data[4].Trim());
            float size = float.Parse(data[5].Trim());
            float layerDepth = float.Parse(data[6].Trim());

            string[] originParts = data[7].Split(',');
            xPos = int.Parse(originParts[0].Trim());
            yPos = int.Parse(originParts[1].Trim());
            Vector2 origin = new Vector2(xPos, yPos);

            // 2. Parse animation clips
            var animationClips = new Dictionary<string, AnimationClip>();
            for (int i = 8; i < data.Count; i++)
            {
                string animationData = data[i];
                if (!string.IsNullOrWhiteSpace(animationData))
                {
                    string[] animationParts = animationData.Split(':');
                    string animationName = animationParts[0].Trim();

                    string[] rectsAndSpeed = animationParts[1].Split(';'); // Outside the bounds of the array
                    string[] rectStrings = rectsAndSpeed[0].Split('|');

                    Rectangle[] frames = rectStrings.Select(rectStr =>
                    {
                        string[] rectComponents = rectStr.Split(',');
                        int rectX = int.Parse(rectComponents[0].Trim());
                        int rectY = int.Parse(rectComponents[1].Trim());
                        int rectWidth = int.Parse(rectComponents[2].Trim());
                        int rectHeight = int.Parse(rectComponents[3].Trim());

                        return new Rectangle(rectX, rectY, rectWidth, rectHeight);
                    }).ToArray();

                    float animationSpeed = float.Parse(rectsAndSpeed[1].Trim());

                    animationClips[animationName] = new AnimationClip(frames, animationSpeed);
                }
            }

            // 3. Create and return the PlayerController object with the parsed data
            return new PlayerController(
                ResourceManager.GetTexture(sprite),  // Texture loading
                position,
                100,  // Assuming speed is always 100 for player, can be parsed too
                color,
                rotation,
                size,
                layerDepth,
                origin,
                animationClips  // Pass the parsed animations
            );
        }


        private Pickup CreatePickUp(List<string> data)
        {
            // 1. Parse general properties (like sprite, position, color, etc.)
            string sprite = data[0];  // Texture name or path
            string[] positionParts = data[1].Split(',');
            int xPos = int.Parse(positionParts[0].Trim());
            int yPos = int.Parse(positionParts[1].Trim());
            Vector2 position = new Vector2(xPos, yPos);

            float speed = float.Parse(data[2]);
            string colorName = data[3].Trim();
            Color color = colorName switch
            {
                "white" => Color.White,
                "red" => Color.Red,
                "blue" => Color.Blue,
                "green" => Color.Green,
                _ => Color.White // Default color if not found
            };

            float rotation = float.Parse(data[4].Trim());
            float size = float.Parse(data[5].Trim());
            float layerDepth = float.Parse(data[6].Trim());

            string[] originParts = data[7].Split(',');
            xPos = int.Parse(originParts[0].Trim());
            yPos = int.Parse(originParts[1].Trim());
            Vector2 origin = new Vector2(xPos, yPos);

            // 2. Parse animation clips
            var animationClips = new Dictionary<string, AnimationClip>();
            for (int i = 8; i < data.Count; i++)
            {
                string animationData = data[i];
                if (!string.IsNullOrWhiteSpace(animationData))
                {
                    string[] animationParts = animationData.Split(':');
                    string animationName = animationParts[0].Trim();

                    string[] rectsAndSpeed = animationParts[1].Split(';'); // Outside the bounds of the array
                    string[] rectStrings = rectsAndSpeed[0].Split('|');

                    Rectangle[] frames = rectStrings.Select(rectStr =>
                    {
                        string[] rectComponents = rectStr.Split(',');
                        int rectX = int.Parse(rectComponents[0].Trim());
                        int rectY = int.Parse(rectComponents[1].Trim());
                        int rectWidth = int.Parse(rectComponents[2].Trim());
                        int rectHeight = int.Parse(rectComponents[3].Trim());

                        return new Rectangle(rectX, rectY, rectWidth, rectHeight);
                    }).ToArray();

                    float animationSpeed = float.Parse(rectsAndSpeed[1].Trim());

                    animationClips[animationName] = new AnimationClip(frames, animationSpeed);
                }
            }

            // 3. Create and return the PlayerController object with the parsed data
            return new Pickup(
                ResourceManager.GetTexture(sprite),  // Texture loading
                position,
                100,  // Assuming speed is always 100 for player, can be parsed too
                color,
                rotation,
                size,
                layerDepth,
                origin,
                animationClips  // Pass the parsed animations
            );
        }
    }
}
