namespace Model.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CommodityDetail : DbContext
    {
        // Your context has been configured to use a 'CommoditySaleDetail' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Model.Models.CommoditySaleDetail' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CommoditySaleDetail' 
        // connection string in the application configuration file.
        public CommodityDetail()
            : base("name=CommoditySaleDetail")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<CommoditySaleDeatil> MyEntities { get; set; }
        
    }

    public class CommoditySaleDeatil
    {
        public string CommodityId { get; set; }
        public string CommodityName { get; set; }
        public int CommodityPrice { get; set; }
        public int CommodityAmount { get; set; }
        public string CommodityImage { get; set; }
        public int? CommoditySellAmount { get; set; }

        public CommoditySaleDeatil()
        {
        }

        public CommoditySaleDeatil(string commodityId, string commodityName, int commodityPrice, int commodityAmount, string commodityImage, int? commoditySellAmount)
        {
            CommodityId = commodityId;
            CommodityName = commodityName;
            CommodityPrice = commodityPrice;
            CommodityAmount = commodityAmount;
            CommodityImage = commodityImage;
            CommoditySellAmount = commoditySellAmount;
        }
    }
}