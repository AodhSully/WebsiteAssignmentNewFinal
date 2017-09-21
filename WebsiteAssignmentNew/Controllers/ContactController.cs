using System;
using WebsiteAssignmentNew.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteAssignmentNew.Controllers
{
    public class ContactController : Controller
    {

        DataSet ds;
        DataTable dt;
        // GET: Contact
        public ActionResult Index()
        {
            if (System.IO.File.Exists(Server.MapPath(path: "~/App_Data/feedback.xml")))
            {
                ds = new DataSet();
                ds.ReadXml(Server.MapPath(path: "~/App_Data/feedback.xml"));
                dt = ds.Tables["user_comments"];
                if (!dt.Columns.Contains("name"))
                    dt.Columns.Add("name");
            }
            else
            {

                ds = new DataSet("feedback");
                dt = new DataTable("user_comments");
                DataColumn name_col = new DataColumn("name");
                DataColumn email_col = new DataColumn("email");
                DataColumn phone_col = new DataColumn("phone");
                DataColumn address_col = new DataColumn("address");
                DataColumn website_col = new DataColumn("website");
                DataColumn artist_col = new DataColumn("artist");
                DataColumn artistname_col = new DataColumn("artistname");
                DataColumn comments_col = new DataColumn("comments");

                dt.Columns.Add(name_col);
                dt.Columns.Add(email_col);
                dt.Columns.Add(phone_col);
                dt.Columns.Add(address_col);
                dt.Columns.Add(website_col);
                dt.Columns.Add(artist_col);
                dt.Columns.Add(artistname_col);
                dt.Columns.Add(comments_col);
                ds.Tables.Add(dt);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Feedback(ContactModel contact)
        {
            if (ModelState.IsValid)
            {
                Response.Write(contact.Name);
                DataRow row = dt.NewRow();
                if (contact.Name == "" || contact.Name == null)
                {
                    row["name"] = "name not entered";
                }
                else
                {
                    row["name"] = contact.Name;
                }
                row["email"] = contact.Email;
                row["phone"] = contact.Phone;
                row["address"] = contact.Address;
                row["website"] = contact.Website;
                row["artist"] = contact.Email;
                row["artistname"] = contact.Email;
                row["comments"] = contact.Comments;


                dt.Rows.Add(row);
                ds.AcceptChanges();
                ds.WriteXml(Server.MapPath(path: "~/App_Data/feedback.xml"));
                return View("Comments");
            }

            else return View("Index", contact);
        }

        public ActionResult ShowFeedback()
        {
            List<ContactModel> contactList = new List<ContactModel>();
            if (System.IO.File.Exists(Server.MapPath(path: "~/App_Data/feedback.xml")))
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(Server.MapPath(path: "~/App_Data/feedback.xml"));
                DataTable table = dataSet.Tables[0];//dataSet.Tables["user_comments"]
                foreach (DataRow row in table.Rows)
                {
                    ContactModel contact = new ContactModel();
                    if (row["name"] != null)
                        contact.Name = row["name"].ToString();
                    contact.Email = row["email"].ToString();

                    contact.Phone = row["phone"].ToString();
                    contact.Address = row["address"].ToString();
                    contact.Website = row["website"].ToString();
                    contact.Email = row["artist"].ToString();
                    contact.Email = row["artistname"].ToString();


                    contact.Comments = row["comments"].ToString();
                    contactList.Add(contact);
                }
                ViewBag.Message = "";
            }
            else ViewBag.Message = "User feedback is not recorded";

            return View(contactList);
        }
    }
}