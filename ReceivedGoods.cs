//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Footprint
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReceivedGoods
    {
        public int ReceivedGoodsId { get; set; }
        public int InvoiceId { get; set; }
        public int ShoesId { get; set; }
        public int ShoeSize { get; set; }
        public int QuantityProduct { get; set; }

        public string UnitOfMeasurement
        {
            get
            {
                return "шт.";
            }
            set { }
        }

        public double TotalCostInInvoice
        {
            get
            {
                return QuantityProduct * Shoes.Price;
            }
            set { }
        }

        private static int numberCounter = 1;
        private static int currentInvoiceId;

        public int Number
        {
            get
            {
                if (InvoiceId != currentInvoiceId)
                {
                    currentInvoiceId = InvoiceId;
                    numberCounter = 1;
                }
                return numberCounter++;
            }
            set { }
        }
        public virtual Invoice Invoice { get; set; }
        public virtual Shoes Shoes { get; set; }
        public virtual ShoeSize ShoeSize1 { get; set; }
    }
}
