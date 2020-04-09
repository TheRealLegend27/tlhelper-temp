using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TLHelper.Hotkeys
{
    class HotkeyRegistry
    {
        // TODO: IMPLEMENT
        public static void LoadSettings() {
            throw new NotImplementedException();
        }

        public delegate void ActionHandle();

        private static Dictionary<int, ActionHandle> actionHandles = new Dictionary<int, ActionHandle>();
        private static Dictionary<(MouseButtons, bool, bool, bool),ActionHandle> mouseHandles = new Dictionary<(MouseButtons, bool, bool, bool), ActionHandle>();

        public static void RegisterKey(Hotkey key, ActionHandle handle)
        {
            if (key.IsMouse())
            {
                MouseButtons mb = key.kKey == Keys.LButton ? MouseButtons.Left :
                                    key.kKey == Keys.RButton ? MouseButtons.Right :
                                    key.kKey == Keys.MButton ? MouseButtons.Middle :
                                    key.kKey == Keys.XButton1 ? MouseButtons.XButton1 :
                                    key.kKey == Keys.XButton2 ? MouseButtons.XButton2 : MouseButtons.None;
                mouseHandles.Add((mb, key.ctrl, key.shift, key.alt), handle);
            }
            else
            {
                int id = HardwareListener.AddAction(key.GetAddonKeys(), key.GetKey());
                actionHandles.Add(id, handle);
            }
        }

        public static void ProcessAction(int id)
        {
            actionHandles[id]();
        }

        public static void ProcessMouse(MouseButtons button, bool ctrlDown, bool shiftDown, bool altDown)
        {
            if (mouseHandles.ContainsKey((button, ctrlDown, shiftDown, altDown)))
            {
                mouseHandles[(button, ctrlDown, shiftDown, altDown)]();
            }
        }
    }
}
