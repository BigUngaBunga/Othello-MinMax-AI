using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OthelloMinMaxAI
{
    class Particle
    {
        Vector2 pos, originalPos;
        Texture2D tex;
        //float direction;
        float Size, Angle, AngularVelocity, time;
        Color color;

        Vector2 startSpeed, acceleration, speed;

        public Particle(Vector2 pos, Texture2D tex, float Size, float AngularVelocity, Color color, float launchSpeed, float launchAngle)
        {
            this.pos = pos;
            originalPos = pos;
            this.tex = tex;


            this.Size = Size;
            Angle = 0;
            this.AngularVelocity = AngularVelocity;
            this.color = color;
            //this.direction = direction;
            startSpeed.X = launchSpeed * (float)Math.Sin(launchAngle * (float)Math.PI / 180);
            startSpeed.Y = launchSpeed * (float)Math.Cos(launchAngle * (float)Math.PI / 180);
            acceleration = new Vector2(0, 10);
        }

        public void Update(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds * 5;

            speed.X = startSpeed.X + acceleration.X * time;
            speed.Y = startSpeed.Y + acceleration.Y * time;

            pos.X = originalPos.X + time * (startSpeed.X + speed.X) / 2;
            pos.Y = originalPos.Y + time * (startSpeed.Y + speed.Y) / 2;

            //pos.X = originalPos.X+ direction;
            //pos.Y = (float)Math.Pow(pos.X, 2) * 0.1f + pos.X * 0.000000001f+C;

            Angle += AngularVelocity;
        }

        public void Draw(SpriteBatch sb)
        {
            Rectangle source = new Rectangle(0, 0, tex.Width, tex.Height);
            Vector2 origin = new Vector2(tex.Width / 2, tex.Height);

            sb.Draw(tex, pos, source, color, Angle, origin, Size, SpriteEffects.None, 0f);
        }
    }
}
