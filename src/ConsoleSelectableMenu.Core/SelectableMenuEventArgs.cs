using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleSelectableMenu
{
    public class SelectableMenuEventArgs : EventArgs
    {
        #region Constructor
        public SelectableMenuEventArgs(ConsoleKeyInfo keyInfo)
        {
            KeyInfo = keyInfo;
        }
        #endregion

        public ConsoleKeyInfo KeyInfo { get; set; }
    }
}
