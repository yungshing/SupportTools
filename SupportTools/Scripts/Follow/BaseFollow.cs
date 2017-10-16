using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupportTools.Follow
{
   public abstract class BaseFollow
    {
        public abstract void Dispose();

        public abstract void OnLoad();

        public abstract void LoadConfig();

        public abstract void SaveConfig();
    }
}
