using Celeste;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vitmod {
    [CustomEntity("vitellary/flaginsidetrigger")]
    [Tracked(false)]
    public class FlagInsideTrigger : Trigger {
        public string flag;
        public FlagInsideTrigger(EntityData data, Vector2 offset) : base(data, offset) {
            flag = data.Attr("flag");
        }

        public override void OnEnter(Player player) {
            base.OnEnter(player);
            SceneAs<Level>().Session.SetFlag(flag, true);
        }

        public override void OnLeave(Player player) {
            base.OnLeave(player);
            SceneAs<Level>().Session.SetFlag(flag, false);
        }
    }
}
