using Celeste;
using Celeste.Mod;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Celeste.Mod.Code.Entities
{
    [Tracked]
    public class RoomNameDisplay : Entity
    {
        private float drawLerp;
        private float textLerp;
        private string text;
        private string nextText;

        public Color bgColor;
        public Color textColor;
        private float colorLerp;
        private Color bgColorStart;
        private Color textColorStart;

        private float outTimer;
        public RoomNameDisplay() {
            Tag = Tags.HUD | Tags.Global | Tags.PauseUpdate | Tags.TransitionUpdate;
            Depth = -100;
            drawLerp = 0f;
            textLerp = 0f;
            colorLerp = 1f;
            outTimer = 0f;

            bgColor = Color.Black;
            textColor = Color.White;

            var trans = new TransitionListener();
            trans.OnInEnd = () =>
            {
                if (Scene.Tracker.GetEntity<RoomNameController>() == null)
                {
                    SetName("");
                }
            };
            Add(trans);
        }

        public override void Update()
        {
            base.Update();
            float speed = 1.5f;
            if (nextText != "")
            {
                if (drawLerp > 0f || !(Scene as Level).Transitioning) {
                    drawLerp = Calc.Approach(drawLerp, 1f, Engine.DeltaTime * speed);
                }
            }
            else
            {
                drawLerp = Calc.Approach(drawLerp, 0f, Engine.DeltaTime * speed);
                if (drawLerp == 0f)
                {
                    RemoveSelf();
                }
            }

            if (text == nextText)
            {
                textLerp = Calc.Approach(textLerp, 1f, Engine.DeltaTime * speed);
            }
            else
            {
                textLerp = Calc.Approach(textLerp, 0f, Engine.DeltaTime * speed);
                if (textLerp == 0f)
                {
                    text = nextText;
                }
            }

            if (colorLerp < 1f)
            {
                colorLerp = Calc.Approach(colorLerp, 1f, Engine.DeltaTime * speed);
            }

            if (outTimer > 0f)
            {
                outTimer = Calc.Approach(outTimer, 0f, Engine.DeltaTime);
                if (outTimer == 0f)
                {
                    SetName("");
                }
            }
        }

        public override void Render()
        {
            base.Render();
            var y = Calc.LerpClamp(1080f, 1032f, Ease.CubeOut(drawLerp));
            Color bgC = bgColor;
            if (colorLerp < 1f)
            {
                bgC = Color.Lerp(bgColorStart, bgColor, colorLerp);
            }
            Draw.Rect(-2f, y, 1920f + 4f, 48f + 2f, bgC);
            if (text != "")
            {
                Color textC = textColor;
                if (colorLerp < 1f) {
                    textC = Color.Lerp(textColorStart, textColor, colorLerp);
                }
                var texty = Calc.LerpClamp(1080f, 1032f, Ease.CubeOut(Calc.Min(textLerp, drawLerp)));
                ActiveFont.Draw(text, new Vector2(960, texty - 6f), new Vector2(0.5f, 0f), new Vector2(1f, 1f), textC);
            }
        }

        public void SetName(string name)
        {
            if (name != "" && Dialog.Has(name))
            {
                nextText = Dialog.Clean(name);
            }
            else
            {
                nextText = name;
            }
        }

        public void SetColor(Color text, Color bg)
        {
            if (colorLerp < 1f)
            {
                bgColorStart = Color.Lerp(bgColorStart, bgColor, colorLerp);
                textColorStart = Color.Lerp(textColorStart, textColor, colorLerp);
            }
            else
            {
                bgColorStart = bgColor;
                textColorStart = textColor;
            }
            colorLerp = 0f;
            if (drawLerp == 0f)
            {
                colorLerp = 1f;
            }
            bgColor = bg;
            textColor = text;
        }

        public void SetTimer(float timer)
        {
            outTimer = timer;
        }

        public void SetInstant()
        {
            if (nextText == "")
            {
                RemoveSelf();
            }
            else
            {
                text = nextText;
                drawLerp = 1f;
                colorLerp = 1f;
                textLerp = 1f;
            }
        }

        public static RoomNameDisplay GetDisplay(Scene scene)
        {
            RoomNameDisplay display = scene.Tracker.GetEntity<RoomNameDisplay>();
            if (display == null)
            {
                display = new RoomNameDisplay();
                scene.Add(display);
            }
            return display;
        }
    }
}
