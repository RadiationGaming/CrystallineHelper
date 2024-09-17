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
    [CustomEntity("vitellary/roomname")]
    [Tracked]
    public class RoomNameController : Entity
    {
        public string Name;
        private string bgColor;
        private string textColor;
        private float timer;

        public RoomNameController(EntityData data, Vector2 offset) : base(data.Position + offset)
        {
            Name = data.Attr("roomName");
            bgColor = data.Attr("backgroundColor", "000000FF");
            textColor = data.Attr("textColor", "FFFFFFFF");
            timer = data.Float("disappearTimer", -1f);
        }
        public override void Awake(Scene scene)
        {
            base.Awake(scene);
            var display = RoomNameDisplay.GetDisplay(scene);
            display.SetName(Name);
            display.SetColor(Calc.HexToColorWithAlpha(textColor), Calc.HexToColorWithAlpha(bgColor));
            display.SetTimer(Math.Max(timer, 0f));
        }
    }
}
