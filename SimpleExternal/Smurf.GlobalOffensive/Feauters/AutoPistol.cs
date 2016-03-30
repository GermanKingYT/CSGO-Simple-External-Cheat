﻿using System;
using System.Threading;

namespace Smurf.GlobalOffensive.Feauters
{
    public class AutoPistol
    {
        private bool _autoPistol;
        private WinAPI.VirtualKeyShort _autoPistolKey;
        private int _delay;
        private long _lastShot;

        public void Update()
        {
            if (!Smurf.Objects.ShouldUpdate())
                return;
            if (Smurf.LocalPlayerWeapon.WeaponGroup != "Pistol")
                return;

            ReadSettigns();

            if (!_autoPistol)
                return;

            //TODO Fix so we only shoot if we are active in the csgo window.
            if (Smurf.KeyUtils.KeyIsDown(_autoPistolKey))
            {
                if (!(new TimeSpan(DateTime.Now.Ticks - _lastShot).TotalMilliseconds >= _delay))
                    return;

                _lastShot = DateTime.Now.Ticks;

                Shoot();
            }
        }

        private void ReadSettigns()
        {
            try
            {
                _autoPistol = Smurf.Settings.GetBool(Smurf.LocalPlayerWeapon.WeaponName, "Auto Pistol");
                _delay = Smurf.Settings.GetInt(Smurf.LocalPlayerWeapon.WeaponName, "Auto Pistol Delay");
                _autoPistolKey = (WinAPI.VirtualKeyShort) Convert.ToInt32(Smurf.Settings.GetString(Smurf.LocalPlayerWeapon.WeaponName, "Auto Pistol Key"), 16);
            }
            catch (Exception e)
            {
#if DEBUG
Console.WriteLine(e.Message);
#endif

            }
        }

        public void Shoot()
        {
            Thread.Sleep(8);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF.LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(8);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF.LEFTUP, 0, 0, 0, 0);
        }
    }
}