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
        //SOOOOOOOOOOO MUCH repeated/duplicate CODE
        //This class is a mess, and I know that there is so much I need to change. Like all of these objects are almost the same and I parse them in almost the same way. So I should be able to
        //Add much of this together, but I am to lazy so here it is
        public GameObject CreateGameObjectFromType(string objectType, List<string> objectData)
        {
            switch (objectType)
            {
                case "EnemyController":
                    Debug.WriteLine("An enemy is created(Factory)");
                    return CreateEnemyController(objectData);
                case "PlayerController":
                    return CreatePlayerController(objectData);
                case "PickUp":
                    return CreatePickUp(objectData);
                case "DonkeyKong":
                    Debug.WriteLine("Donkey Kong is created(Factory)");
                    return CreateDonkeyKong(objectData);
                default:
                    Debug.WriteLine("Unknown object type: " + objectType);
                    return null;
            }
        }
        private EnemyController CreateEnemyController(List<string> data)
        {

            string sprite = data[0];  // Texture
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
                _ => Color.White
            };
            
            float rotation = float.Parse(data[3].Trim());
            float size = float.Parse(data[4].Trim());
            float layerDepth = float.Parse(data[5].Trim());

            string[] originParts = data[6].Split(',');
            xPos = int.Parse(originParts[0].Trim());
            yPos = int.Parse(originParts[1].Trim());
            Vector2 origin = new Vector2(xPos, yPos);

            var animationClips = new Dictionary<string, AnimationClip>();

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
            return new EnemyController(
                ResourceManager.GetTexture(sprite),
                position,
                color,
                rotation,
                size,
                layerDepth,
                origin,
                animationClips
            );
        }
        private PlayerController CreatePlayerController(List<string> data)
        {
            string sprite = data[0];
            string[] positionParts = data[1].Split(',');
            float xPos = float.Parse(positionParts[0].Trim());
            float yPos = float.Parse(positionParts[1].Trim());
            Vector2 position = new Vector2(xPos, yPos);

            float speed = float.Parse(data[2]);
            string colorName = data[3].Trim();
            Color color = colorName switch
            {
                "white" => Color.White,
                "red" => Color.Red,
                "blue" => Color.Blue,
                "green" => Color.Green,
                _ => Color.White
            };

            float rotation = float.Parse(data[4].Trim());
            float size = float.Parse(data[5].Trim());
            float layerDepth = float.Parse(data[6].Trim());

            string[] originParts = data[7].Split(',');
            xPos = float.Parse(originParts[0].Trim());
            yPos = float.Parse(originParts[1].Trim());
            Vector2 origin = new Vector2(xPos, yPos);

            var animationClips = new Dictionary<string, AnimationClip>();
            for (int i = 8; i < data.Count; i++)
            {
                string animationData = data[i];
                if (!string.IsNullOrWhiteSpace(animationData))
                {
                    string[] animationParts = animationData.Split(':');
                    string animationName = animationParts[0].Trim();

                    string[] rectsAndSpeed = animationParts[1].Split(';');
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

            return new PlayerController(
                ResourceManager.GetTexture(sprite),
                position,
                speed,
                color,
                rotation,
                size,
                layerDepth,
                origin,
                animationClips
            );
        }


        private Item CreatePickUp(List<string> data)
        {
            string sprite = data[0];
            string[] positionParts = data[1].Split(',');
            float xPos = float.Parse(positionParts[0].Trim());
            float yPos = float.Parse(positionParts[1].Trim());
            Vector2 position = new Vector2(xPos, yPos);

            float speed = float.Parse(data[2]);
            string colorName = data[3].Trim();
            Color color = colorName switch
            {
                "white" => Color.White,
                "red" => Color.Red,
                "blue" => Color.Blue,
                "green" => Color.Green,
                _ => Color.White
            };

            float rotation = float.Parse(data[4].Trim());
            float size = float.Parse(data[5].Trim());
            float layerDepth = float.Parse(data[6].Trim());

            string[] originParts = data[7].Split('.');
            xPos = float.Parse(originParts[0].Trim());
            yPos = float.Parse(originParts[1].Trim());
            Vector2 origin = new Vector2(xPos, yPos);

            int numberOfAnimatedClips = 0;
            for (int i = 8; i < data.Count; i++)
            {
                if (data[i].Contains(':'))
                {
                        numberOfAnimatedClips++;
                    Debug.WriteLine(numberOfAnimatedClips);
                }
                else
                {
                    break;
                }
            }
            var animationClips = new Dictionary<string, AnimationClip>();
            for (int i = 8; i < 8 + numberOfAnimatedClips; i++)
            {
                string animationData = data[i];
                if (!string.IsNullOrWhiteSpace(animationData))
                {
                    string[] animationParts = animationData.Split(':');
                    string animationName = animationParts[0].Trim();

                    string[] rectsAndSpeed = animationParts[1].Split(';');
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
            string itemTypeName = data[9].Trim();

            ItemType type = itemTypeName switch
            {
                "Wearable" => ItemType.Wearable,
                "Weapon" => ItemType.Weapon,
                "Consumable" => ItemType.Consumable,
                _ => ItemType.Consumable
            };
            int minScore = int.Parse(data[10].Trim());
            int maxScore = int.Parse(data[11].Trim());

            return new Item(
                ResourceManager.GetTexture(sprite),
                position,
                speed,
                color,
                rotation,
                size,
                layerDepth,
                origin,
                animationClips,
                type,
                minScore,
                maxScore
            );
        }
        private DonkeyKong CreateDonkeyKong(List<string> data)
        {
            string sprite = data[0];
            string[] positionParts = data[1].Split(',');
            float xPos = float.Parse(positionParts[0].Trim());
            float yPos = float.Parse(positionParts[1].Trim());
            Vector2 position = new Vector2(xPos, yPos);

            float speed = float.Parse(data[2]);
            string colorName = data[3].Trim();
            Color color = colorName switch
            {
                "white" => Color.White,
                "red" => Color.Red,
                "blue" => Color.Blue,
                "green" => Color.Green,
                _ => Color.White
            };

            float rotation = float.Parse(data[4].Trim());
            float size = float.Parse(data[5].Trim());
            float layerDepth = float.Parse(data[6].Trim());

            string[] originParts = data[7].Split(',');
            xPos = float.Parse(originParts[0].Trim());
            yPos = float.Parse(originParts[1].Trim());
            Vector2 origin = new Vector2(xPos, yPos);

            int numberOfAnimatedClips = 0;
            for (int i = 8; i < data.Count; i++)
            {
                if (data[i].Contains(':'))
                {
                    numberOfAnimatedClips++;
                    Debug.WriteLine(numberOfAnimatedClips);
                }
                else
                {
                    break;
                }
            }
            var animationClips = new Dictionary<string, AnimationClip>();
            for (int i = 8; i < 8 + numberOfAnimatedClips; i++)
            {
                string animationData = data[i];
                if (!string.IsNullOrWhiteSpace(animationData))
                {
                    string[] animationParts = animationData.Split(':');
                    string animationName = animationParts[0].Trim();

                    string[] rectsAndSpeed = animationParts[1].Split(';');
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
            return new DonkeyKong(
                ResourceManager.GetTexture(sprite),
                position,
                speed,
                color,
                rotation,
                size,
                layerDepth,
                origin,
                animationClips
            );
        }
    }
}
