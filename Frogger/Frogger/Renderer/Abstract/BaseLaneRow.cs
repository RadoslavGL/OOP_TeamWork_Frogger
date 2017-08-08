﻿using Frogger.Renderer.Abstract;
using Frogger.Renderer.Contracts;
using Frogger.Renderer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frogger.Renderer
{
    public abstract class BaseLaneRow : BaseRow, ILaneRow
    {
        private int vehicleX;
        //ако има време може и vehicleLength

        public BaseLaneRow(RowID initialRowID) : base(initialRowID)
        {
            //ем, те като се създават обектите не е необходимо да ги инициализирам със стойности
            //защото и без друго веднага ще бъдат overwrite-нати в калкулатора
        }

        public int VehicleX
        {
            get
            {
                return this.vehicleX;
            }
            set //set-ва се от калкулатора    
            {
                this.vehicleX = value;
            }
        }
        
    }
}
