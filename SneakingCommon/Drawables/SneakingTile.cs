using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas_Window_Template.Drawables;
using Canvas_Window_Template.Interfaces;

namespace SneakingCommon.Drawables
{
    public class SneakingTile:Tile
    {
        
        public float[] ShadeColor { get; set; }


        public SneakingTile(IPoint tileStart, IPoint tileEnd):base(tileStart,tileEnd)
        {
        }
        public SneakingTile(double[] start, int[] end):base(start,end)
        {

        }
        public SneakingTile(double[] start, int _tileSize, int orientation = 3)
            : base(start, _tileSize, orientation)
        {
            
        }    
    }
}
