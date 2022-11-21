using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OthelloMinMaxAI
{

    class ButtonManager
    {
        List<Button> buttons;

        public ButtonManager()
        {
            buttons = new List<Button>();
        }

        public void Add(Button button)
        {
            buttons.Add(button);
        }

        public void Update(GameTime gametime)
        {
            if (KeyMouseReader.LeftClick())
            {
                foreach (Button b in buttons)
                {
                    if (b.hitbox.Contains(KeyMouseReader.mouseState.Position))
                    {
                        b.ButtonPressed();
                        break;
                    }
                }
            }

            foreach (Button b in buttons)
            {
                b.Update(gametime);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (Button b in buttons)
            {
                b.Draw(sb);
            }
        }
    }
}
