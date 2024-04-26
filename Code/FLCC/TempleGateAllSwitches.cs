using Celeste;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace vitmod {
    [Tracked()]
    [CustomEntity("vitellary/templegateall")]
    public class TempleGateAllSwitches : TempleGate {
        public TempleGateAllSwitches(EntityData data, Vector2 offset) : base(data.Position + offset, 48, Types.NearestSwitch, data.Attr("sprite", "default"), data.Level.Name) {
            ClaimedByASwitch = true;
        }

        public static void Load() {
            On.Celeste.DashSwitch.Awake += DashSwitch_Awake;
        }

        public static void Unload() {
            On.Celeste.DashSwitch.Awake -= DashSwitch_Awake;
        }

        private static void DashSwitch_Awake(On.Celeste.DashSwitch.orig_Awake orig, DashSwitch self, Scene scene) {
            orig(self, scene);
            DashCollision orig_OnDashCollide = self.OnDashCollide;
            self.OnDashCollide = (Player player, Vector2 direction) => {
                DashCollisionResults result = orig_OnDashCollide(player, direction);
                bool finalswitch = true;
                if (self.pressed) {
                    foreach (Solid solid in self.SceneAs<Level>().Tracker.GetEntities<Solid>()) {
                        if (solid is DashSwitch dashSwitch) {
                            if (!dashSwitch.pressed) {
                                finalswitch = false;
                                break;
                            }
                        }
                    }
                } else {
                    finalswitch = false;
                }
                if (finalswitch) {
                    foreach (TempleGateAllSwitches gate in self.SceneAs<Level>().Tracker.GetEntities<TempleGateAllSwitches>()) {
                        gate.Open();
                    }
                }
                return result;
            };
        }
    }
}
