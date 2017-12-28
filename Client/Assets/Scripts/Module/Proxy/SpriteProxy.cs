using UnityEngine;
using System.Collections;
using Message;
using System;
using System.Collections.Generic;

namespace RedStone
{
    public class SpriteProxy : ProxyBase
    {
        Dictionary<string, Sprite> m_allSprites = new Dictionary<string, Sprite>();

        public SpriteProxy()
        {
        }

        public override void OnInit()
        {
            var pathHolder = (Resources.Load("Atlas/SpritePath") as GameObject).GetComponent<TexturePathsHolder>();
            foreach (var path in pathHolder.paths)
            {
                var spriteHolder = (Resources.Load(path) as GameObject).GetComponent<SpriteHolder>();
                foreach (var sprite in spriteHolder.allSprites)
                {
                    m_allSprites.Add(sprite.name, sprite);
                }
            }
        }

        public Sprite GetSprite(string spriteName)
        {
            Sprite sprite = null;
            m_allSprites.TryGetValue(spriteName, out sprite);
            return sprite;
        }

    }
}