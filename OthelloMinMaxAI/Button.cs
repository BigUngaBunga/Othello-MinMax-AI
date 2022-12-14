using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OthelloMinMaxAI
{
    class Button
    {
        public Rectangle hitbox;
        Texture2D tex;
        public bool pressable;
        public bool buttonPressed;
        public Color color;
        int currentFrame;
        float timer;
        float timeTillUnpress;

        public Button(Texture2D tex, Point location, Point dimensions, float timeTillUnpress)
        {
            this.tex = tex;
            this.timeTillUnpress = timeTillUnpress;
            hitbox = new Rectangle(location, dimensions);
            color = Color.White;
        }

        public void Update(GameTime gameTime)
        {
            if (!pressable)
            {
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer <= 0)
                {
                    if (!buttonPressed)
                        Unpressed();
                }
            }
        }

        public void ButtonPressed()
        {
            buttonPressed = true;
            pressable = false;
            currentFrame = 0;
            timer = timeTillUnpress;
        }

        public void Unpressed()
        {
            pressable = true;
            currentFrame = 0;
        }

        public void Draw(SpriteBatch sb)
        {
            Rectangle source = new Rectangle(tex.Width * currentFrame / 2, 0, tex.Width / 2, tex.Height);
            sb.Draw(tex, hitbox, source, color);
        }

        public void ChangeColour(Color newColour, Color pickedColour, bool useColour)
        {
            color = useColour ? newColour : pickedColour;
        }
    }
}
