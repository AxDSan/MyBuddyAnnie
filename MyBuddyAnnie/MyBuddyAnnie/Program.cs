using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace MyBuddyAnnie
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        public static Spell.Targeted Q;
        public static Spell.Skillshot W;
        public static Spell.Active E;
        public static Spell.Skillshot R;
        public static Menu MyBuddyAnnie, ComboMenu, HarassMenu, FarmMenu, FleeMenu;

        static double CalcMyPi(int i)
        {
            return 1 + i / (2.0 * i + 1) * CalcMyPi(i + 1);
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            TargetManager.init();
            Bootstrap.Init(null);

            double RCastDelay_Float = 0.25;
            double QCastDelay_Float = 0.60;
            double QCastSpeed_Float = 0.50;
            int QCastDelay_Int = (int)QCastDelay_Float;
            int RCastDelay_Int = (int)RCastDelay_Float;
            int QCastSpeed_Int = (int)QCastSpeed_Float;
            double MaxValue_Float = float.MaxValue;
            int MaxValue_Int = (int)MaxValue_Float;


            int PI = 2 * (int)CalcMyPi(1);

            Q = new Spell.Targeted(SpellSlot.Q, 625);
            W = new Spell.Skillshot(SpellSlot.W, 625, SkillShotType.Cone, QCastDelay_Int, MaxValue_Int, QCastSpeed_Int * PI / 180);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Skillshot(SpellSlot.R, 600, SkillShotType.Circular, RCastDelay_Int, MaxValue_Int, 200);

            MyBuddyAnnie = MainMenu.AddMenu("MyBuddyAnnie", "mybuddyannie");
            MyBuddyAnnie.AddGroupLabel("MyBuddyAnnie");
            MyBuddyAnnie.AddSeparator();
            MyBuddyAnnie.AddLabel("Forked from the 'TeemoBuddy' Repository and Implemented it on for Annie");
            MyBuddyAnnie.AddLabel("Don't hate on me, I'm learning this now xD");

            ComboMenu = MyBuddyAnnie.AddSubMenu("Combo", "Combo");
            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.AddSeparator();
            ComboMenu.Add("useQCombo", new CheckBox("Use Q"));
            ComboMenu.Add("useWCombo", new CheckBox("Use W"));
            ComboMenu.Add("useRCombo", new CheckBox("Use R"));

            HarassMenu = MyBuddyAnnie.AddSubMenu("Harass", "Harass");
            HarassMenu.AddGroupLabel("Harass Settings");
            HarassMenu.AddSeparator();
            HarassMenu.Add("useQHarass", new CheckBox("Use Q"));
            HarassMenu.Add("useWHarass", new CheckBox("Use W"));

            FarmMenu = MyBuddyAnnie.AddSubMenu("Farm", "Farm");
            FarmMenu.AddGroupLabel("Farming Settings");
            FarmMenu.AddSeparator();
            FarmMenu.Add("useQFarmLH", new CheckBox("Use Q for LastHit"));
            FarmMenu.Add("useQFarmWC", new CheckBox("Use Q for WaveClear"));

            FleeMenu = MyBuddyAnnie.AddSubMenu("Flee", "Flee");
            FleeMenu.AddGroupLabel("Flee Settings");
            FleeMenu.AddSeparator();
            FleeMenu.Add("useRFlee", new CheckBox("Use R"));
            FleeMenu.Add("useWFlee", new CheckBox("Use W"));

            Game.OnTick += Game_OnTick;
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                StatesManager.Combo();
            }
            else if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                StatesManager.Harass();
            }
            else if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                StatesManager.WaveClear();
            }
            else if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
                StatesManager.LastHit();
            }
            else if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                StatesManager.Flee();
            }
        }
    }
}