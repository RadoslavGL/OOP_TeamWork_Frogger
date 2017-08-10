﻿using Frogger.Objects.Models;
using Frogger.Renderer.Abstract;
using Frogger.Renderer.Contracts;
using Frogger.Renderer.Enums;
using Frogger.Renderer.RowCollection;
using Frogger.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frogger.Renderer.Models
{
    public class LaneRow : BaseRow, ILaneRow
    {
        //няма да е VehichleX, защото нямам само едно Vehicle => трябва да ги държа или в колекция
        //или по-интелигентно във всеки LaneRow да има инстанция Vehicle, но по една, за това я правя readonly

        private readonly Vehicle vehicleOnTheRow;

        public LaneRow(RowID initialRowID) : base(initialRowID)
        {
            this.vehicleOnTheRow = new Vehicle();
            //default-ен конструктор, ползвам го при инициализацията на модела
            //във всеки LaneRow има по една количка, където да и се пазят персонално стойностите
        }

        public IVehicle VehicleOnTheRow
        {
            get
            {
                return this.vehicleOnTheRow;
            }
        }

        public override string ToString()
        {
            //Каквото и да става да не се ползва VehichleLength за дължина на колата.
            //VehicleLength е множител на пълнежа на количките => когато е например 3, колата е дълга 10
            if (base.HasFrog)
            {
                //ако на реда има жаба
                if (Swamp.Instance.X >= this.VehicleOnTheRow.X + this.VehicleOnTheRow.ToString().Split('*')[0].Length)
                //това е така, тука сме се разбрали максималния Х на жабата да е <=94, защото иначе ще излиза съобщение за умряла жаба и това изобщо няма да се вика и няма да се вика този ред
                {
                    //празно, кола, празно, жаба
                    return string.Format("{0}{1}{2}{3}*{0}{4}{2}{5}*{0}{6}{2}{7}",
                        new string(' ', this.VehicleOnTheRow.X),            //0, tova e taka
                        this.VehicleOnTheRow.ToString().Split('*')[0],      //1, tova e taka
                        new string(' ', Swamp.Instance.X - this.VehicleOnTheRow.X - this.VehicleOnTheRow.ToString().Split('*')[0].Length),    //2, tova e taka
                        Swamp.Instance.ToString().Split('*')[0],            //3, tova e taka
                        this.VehicleOnTheRow.ToString().Split('*')[1],      //4, tova e taka
                        Swamp.Instance.ToString().Split('*')[1],            //5, tova e taka
                        this.VehicleOnTheRow.ToString().Split('*')[2],      //6, tova e taka
                        Swamp.Instance.ToString().Split('*')[2]);           //7, tova e taka
                }
                else if (this.VehicleOnTheRow.X >= Swamp.Instance.X + Swamp.Instance.ToString().Split('*')[0].Length)
                {
                    //празно, жаба, празно, кола
                    if (this.VehicleOnTheRow.X <= GlobalConstants.ScreenWidth - this.VehicleOnTheRow.ToString().Split('*')[0].Length)
                    //ако Х на колата е по-малко или равно 
                    //на размера на екрана минус размера на колата, който се задава динамично от генератор
                    {
                        return string.Format("{0}{1}{2}{3}*{0}{4}{2}{5}*{0}{6}{2}{7}",
                            new string(' ', Swamp.Instance.X),              //0
                            Swamp.Instance.ToString().Split('*')[0],        //1
                            new string(' ', this.VehicleOnTheRow.X - Swamp.Instance.X - Swamp.Instance.ToString().Split('*')[0].Length),        //2
                            this.VehicleOnTheRow.ToString().Split('*')[0],  //3
                            Swamp.Instance.ToString().Split('*')[1],        //4
                            this.VehicleOnTheRow.ToString().Split('*')[1],  //5
                            Swamp.Instance.ToString().Split('*')[2],        //6
                            this.VehicleOnTheRow.ToString().Split('*')[2]); //7
                    }
                    else
                    {
                        //ако Х на колата е между размера на екрана минус дължината
                        //на колата (динамична) и размера на екрана => трябва да се отреже малко от края й,
                        //което става по следния начин:
                        //string pesho = "asdfghjkl";
                        //Console.WriteLine(pesho.Length); //9
                        //Console.WriteLine(pesho.Remove(5)); //"asdfg"
                        return string.Format("{0}{1}*{0}{2}*{0}{3}",
                            new string(' ', this.VehicleOnTheRow.X),
                            this.VehicleOnTheRow.ToString().Split('*')[0].Remove(GlobalConstants.ScreenWidth - this.VehicleOnTheRow.X - 1),
                            this.VehicleOnTheRow.ToString().Split('*')[1].Remove(GlobalConstants.ScreenWidth - this.VehicleOnTheRow.X - 1),
                            this.VehicleOnTheRow.ToString().Split('*')[2].Remove(GlobalConstants.ScreenWidth - this.VehicleOnTheRow.X - 1));
                    }
                }
                else
                {
                    if (Swamp.Instance.X + 10 < GlobalConstants.ScreenWidth &&
                        this.VehicleOnTheRow.X + 13 < GlobalConstants.ScreenWidth &
                        Math.Min(this.VehicleOnTheRow.X, Swamp.Instance.X) + 13 < GlobalConstants.ScreenWidth)
                    {
                        return string.Format("{0}FrogX = {1}*{2}VehicleX = {3}*{4} => Collision", //n==13
                        new string(' ', Swamp.Instance.X),
                        Swamp.Instance.X,
                        new string(' ', this.VehicleOnTheRow.X),
                        this.VehicleOnTheRow.X,
                        new string(' ', Math.Min(this.VehicleOnTheRow.X, Swamp.Instance.X)));
                    }
                    else
                    {
                        return string.Format("{0}FrogX = {1}*{2}VehicleX = {3}*{4} => Collision", //n==13
                        new string(' ', 86),
                        Swamp.Instance.X,
                        new string(' ', 86),
                        this.VehicleOnTheRow.X,
                        new string(' ', 86));
                    }
                }
            }
            else
            {
                //ако на реда няма жаба
                if (this.VehicleOnTheRow.X < GlobalConstants.ScreenWidth - this.VehicleOnTheRow.ToString().Split('*')[0].Length)
                {
                    //ако Х на колата е по-малко или равно 
                    //на размера на екрана минус размера на колата, който се задава динамично от генератор
                    return string.Format("{0}{1}*{0}{2}*{0}{3}",
                        new string(' ', this.VehicleOnTheRow.X),
                        this.VehicleOnTheRow.ToString().Split('*')[0],
                        this.VehicleOnTheRow.ToString().Split('*')[1],
                        this.VehicleOnTheRow.ToString().Split('*')[2]);
                }
                else
                {   
                    //ако Х на колата е между размера на екрана минус дължината на колата (динамична)
                    //и размера на екрана => трябва да се отреже малко от края й, което става по следния начин:
                    //string pesho = "asdfghjkl";
                    //Console.WriteLine(pesho.Length); //9
                    //Console.WriteLine(pesho.Remove(5)); //"asdfg"
                    return string.Format("{0}{1}*{0}{2}*{0}{3}",
                        new string(' ', this.VehicleOnTheRow.X),
                        this.VehicleOnTheRow.ToString().Split('*')[0].Remove(GlobalConstants.ScreenWidth - this.VehicleOnTheRow.X - 1),
                        this.VehicleOnTheRow.ToString().Split('*')[1].Remove(GlobalConstants.ScreenWidth - this.VehicleOnTheRow.X - 1),
                        this.VehicleOnTheRow.ToString().Split('*')[2].Remove(GlobalConstants.ScreenWidth - this.VehicleOnTheRow.X - 1));
                }
            } //работи
        }
    }
}
