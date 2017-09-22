﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteAssignmentNew.Models;

namespace WebsiteAssignmentNew.Controllers
{
    public class ShopController : Controller
       
    {
    DAO dao = new DAO();
    // GET: Shop
    public ActionResult Index()
        {
             List<Shop> shoplist = dao.ShowAllMerchandise();
        //    List<Shop> shoplist = new List<Shop>()
        //    {

            ///shop items initially hardcoded in here
        //        new Shop{Item="Eyes Hat", Description="hat 1", ItemCode="BC1001", Price=5.99m, ItemImage="hat1.png"},
        //        new Shop{Item="Logo Hat", Description="hat 2", ItemCode="BC1001", Price=5.99m, ItemImage="hat2.png"},
        //        new Shop{Item="Logo Text Hat", Description="hat 3", ItemCode="BC1001", Price=5.99m, ItemImage="hat3.png"},
        //        new Shop{Item="Logo Text White Hat ", Description="hat 3", ItemCode="BC1001", Price=5.99m, ItemImage="hat4.png"},
        //        new Shop{Item="Eyes Sticker Sheet", Description="stickerSheet", ItemCode="BC1001", Price=3.99m, ItemImage="Sticker1.png"},
        //        new Shop{Item="Logo Text Sticker Sheet", Description="stickerSheet", ItemCode="BC1001", Price=3.99m, ItemImage="Sticker2.png"},
        //        new Shop{Item="Mens Logo T-Shirt", Description="tshirt", ItemCode="BC1001", Price=15.99m, ItemImage="tshirt1.png"},
        //        new Shop{Item="Womens Red Logo T-Shirt", Description="tshirt", ItemCode="BC1001", Price=15.99m, ItemImage="tshirt2.png"},
        //        new Shop{Item="Mens Logo Text T-Shirt", Description="tshirt", ItemCode="BC1001", Price=15.99m, ItemImage="tshirt3.png"},
        //        new Shop{Item="Mens Logo Text Red T-Shirt", Description="tshirt", ItemCode="BC1001", Price=15.99m, ItemImage="tshirt4.png"},
        //        new Shop{Item="Logo Text Hoodie", Description="hoodie", ItemCode="BC1001", Price=15.99m, ItemImage="hoodie11.png"},
        //        new Shop{Item="Logo Eyes Hoodie", Description="hoodie", ItemCode="BC1001", Price=15.99m, ItemImage="hoodie22.png"},


        //};
            List<int> quantityList = new List<int>() { 1, 2, 3, 4, 5 };
        ViewBag.Quantity = quantityList;
            return View(shoplist);
        }
    }
}