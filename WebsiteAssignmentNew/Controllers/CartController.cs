using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteAssignmentNew.Models;


namespace WebsiteAssignmentNew.Controllers
{
    public class CartController : Controller
    {
        DAO dao = new DAO();
        static List<Track> selectedTracks = new List<Track>();
        static List<Shop> selectedMerchandise = new List<Shop>();
        static List<ProductModel> selectedItems = new List<ProductModel>();

        decimal totalPrice = 0.0m;
        decimal totalPriceTrack = 0.0m;
        decimal totalPriceMerc = 0.0m;
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(FormCollection form)
        {

            List<Track> list = dao.ShowAllTracks();
            bool found = false, found1 = false;

            for (int i = 0; i < selectedItems.Count && found == false; i++)
            {
                //if already in the selected items
                if (selectedItems[i].ProductID == form["TrackCode"])
                {
                    selectedItems[i].Quantity = selectedItems[i].Quantity + int.Parse(form["quantity"]);
                    selectedItems[i].TotalPrice = selectedItems[i].TotalPrice * selectedItems[i].Quantity;
                    found = true;
                    found1 = true;

                }

            }

            for (int i = 0; i < list.Count && found1 == false; i++)
            {

                if (list[i].TrackCode == form["TrackCode"])
                {
                    list[i].Quantity = int.Parse(form["quantity"]);
                    selectedTracks.Add(list[i]);
                    found1 = true;

                }
            }


            return RedirectToAction("ViewCart");
        }

        public ActionResult AddMerchandise(FormCollection form)
        {

            List<Shop> list2 = dao.ShowAllMerchandise();
            bool found = false, found1 = false;

            for (int i = 0; i < selectedItems.Count && found == false; i++)
            {
                //if already in the selected items
                if (selectedItems[i].ProductID == form["ItemCode"])
                {
                    selectedItems[i].Quantity = selectedItems[i].Quantity + int.Parse(form["quantity"]);
                    selectedItems[i].TotalPrice = selectedItems[i].TotalPrice * selectedItems[i].Quantity;
                    found = true;
                    found1 = true;

                }

            }

            for (int i = 0; i < list2.Count && found1 == false; i++)
            {

                if (list2[i].ItemCode == form["ItemCode"])
                {
                    list2[i].Quantity = int.Parse(form["quantity"]);
                    selectedMerchandise.Add(list2[i]);
                    found1 = true;

                }
            }


            return RedirectToAction("ViewCart");
        }


        [HttpPost]
        public ActionResult RemoveItem(FormCollection form)
        {
            bool found = false;
            ProductModel item;

            for (int i = 0; i < selectedItems.Count && found == false; i++)
            {

                if (selectedItems[i].ProductID == form["id"] || selectedItems[i].ProductID == form["code"])
                {
                    item = selectedItems[i];
                    if (item.Quantity > 1)
                    {
                        item.Quantity = item.Quantity - 1;
                        item.TotalPrice = item.TotalPrice - item.Price;
                    }
                    else
                    {
                        selectedItems.Remove(item);
                    }
                    found = true;
                }
            }
            foreach (var item1 in selectedItems)
            {
                totalPrice = totalPrice + item1.TotalPrice;
            }
            ViewBag.TransactionPrice = totalPrice;

            return RedirectToAction("ViewCart");
        }

        public ActionResult ClearAll()
        {
            selectedTracks.Clear();
            selectedMerchandise.Clear();
            selectedItems.Clear();
            return RedirectToAction("ViewCart");
        }

        public ActionResult ViewCart()
        {


            foreach (Track track in selectedTracks)
            {

                ProductModel item = new ProductModel();

                totalPriceTrack = totalPriceTrack + track.Quantity * track.PriceVinyl;
                item.ProductID = track.TrackCode;
                //item.Title = track.Title;
                item.Quantity = track.Quantity;
                item.Price = track.PriceVinyl;
                item.TotalPrice = totalPriceTrack;
                if (!selectedItems.Contains(item))
                    selectedItems.Add(item);

            }


            foreach (Shop merc in selectedMerchandise)
            {

                ProductModel item = new ProductModel();

                totalPriceMerc = totalPriceMerc + merc.Quantity * merc.Price;
                item.ProductID = merc.ItemCode;
                //item.Title = track.Title;
                item.Quantity = merc.Quantity;
                item.Price = merc.Price;
                item.TotalPrice = totalPriceMerc;
                if (!selectedItems.Contains(item))
                    selectedItems.Add(item);

            }

            foreach (var item in selectedItems)
            {
                totalPrice = totalPrice + item.TotalPrice;
            }
            ViewBag.TransactionPrice = totalPrice;
            //To remove from the selected books to avoid adding them in selected items again and again
            selectedTracks.Clear();
            //selectedCourses.Clear();
            return View(selectedItems);
        }

        //public ActionResult CheckOut()
        //{
        //    int count = 0;
        //    if (selectedItems.Count > 0)
        //    {
        //        foreach (ProductModel item in selectedItems)
        //        {
        //            totalPrice = totalPrice + item.TotalPrice;

        //        }
        //    }

        //    dao.AddTransaction(/*Session.SessionID + count,*/ DateTime.Now, totalPrice, Session["UserName"].ToString());

        //    if (selectedItems.Count > 0)
        //    {
        //        foreach (ProductModel item in selectedItems)
        //        {

        //            dao.AddTransactionItem(/*Session.SessionID + count,*/ item);
        //        }
        //    }
        //    count++;
        //    Session.Clear();
        //    //Session.Abandon();

        //    return View();
        //}

        public ActionResult ShippingDetails()
        {
            //int count = 0;
            //if (selectedItems.Count > 0)
            //{
            //    foreach (ProductModel item in selectedItems)
            //    {
            //        totalPrice = totalPrice + item.TotalPrice;

            //    }
            //}

            //dao.AddTransaction(/*Session.SessionID + count,*/ DateTime.Now, totalPrice, Session["UserName"].ToString());

            //if (selectedItems.Count > 0)
            //{
            //    foreach (ProductModel item in selectedItems)
            //    {

            //        dao.AddTransactionItem(/*Session.SessionID + count,*/ item);
            //    }
            //}
            //count++;
            //Session.Clear();
            ////Session.Abandon();
            return View();
        }

        //public ActionResult ShippingDetails()
        //{
        //    return View();
        //}
        public ActionResult AddShippingDetails(ShippingModel shipping)
        {

            dao.InsertAddress(shipping);
            int count = 0;
            if (selectedItems.Count > 0)
            {
                foreach (ProductModel item in selectedItems)
                {
                    totalPrice = totalPrice + item.TotalPrice;

                }
            }
            string a = null;
            string b = null;
            int addressID = (dao.getAddressID(a,b));
            dao.AddTransaction(/*Session.SessionID + count,*/ DateTime.Now, totalPrice, Session["UserName"].ToString(), addressID);

            if (selectedItems.Count > 0)
            {
                foreach (ProductModel item in selectedItems)
                {

                    dao.AddTransactionItem(/*Session.SessionID + count,*/ item);
                }
            }
            count++;
            Session.Clear();
            //Session.Abandon();

            return Content(count.ToString());
            //return View();
        }

    }
}