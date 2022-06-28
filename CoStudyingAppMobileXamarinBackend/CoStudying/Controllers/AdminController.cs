
using CoStudying.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CoStudying.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")] // tune to your needs
    public class AdminController : ApiController
    {
        DatabaseContext context = DBContextMethods.GetDatabaseContext();

        //TEST
        //test 2
        //test 3
        //test4
        //TEST5

        //[Authorize]
        //[HttpPost]
        //public CreateProductResponseModel CreateNewProduct(Product product)
        //{
        //    try
        //    {
        //        Product p = new Product()
        //        {
        //            CreatedOn = DateTime.Now,
        //            isActive = true,
        //            isPopular = product.isPopular,
        //            PhotoPath1 = product.PhotoPath1,
        //            PhotoPath2 = product.PhotoPath2,
        //            ProductCategoryId = product.ProductCategoryId
        //        };
        //        context.Products.Add(p);
        //        context.SaveChanges();
        //        int lastProductId = context.Products.OrderByDescending(x => x.Id).First().Id;
        //        return new CreateProductResponseModel() { isSuccess = true, productId = lastProductId };
        //    }
        //    catch (Exception ex)
        //    {

        //        return new CreateProductResponseModel() { isSuccess = false, productId = 0 };
        //    }
        //}

        //[Authorize]
        //[HttpPost]
        //public HttpResponseMessage EditProduct (Product product)
        //{
        //    try
        //    {
        //        Product p = context.Products.Where(x => x.Id == product.Id).FirstOrDefault();
        //        p.ProductCategoryId = product.ProductCategoryId;
        //        p.isPopular = product.isPopular;
        //        p.isActive = product.isActive;
        //        context.SaveChanges();
        //        return new HttpResponseMessage(HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {

        //        return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //    }
        //}


        ////http://printsanaccess.online/Images/ProductImages/gofre_2_mini.jpg

        //[Authorize]
        //[HttpPost]
        //[HttpGet]
        //public HttpResponseMessage PostProductImages(int productId)
        //{
        //    Product product = context.Products.Where(x => x.Id == productId).FirstOrDefault();
        //    if (product == null)
        //        return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //    var httpRequest = HttpContext.Current.Request;
        //    int filesCount = httpRequest.Files.Count;
        //    if (filesCount > 0)
        //    {
        //        Random rnd = new Random();
        //        int i = 1;

        //        for (int k = 0; k < filesCount; k++)
        //        {

        //            string specialKey = String.Empty;
        //            string characters = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890ab";
        //            for (int j = 0; j < 6; j++)
        //            {
        //                int index = rnd.Next(0, 62);
        //                specialKey += characters[index];
        //            }

        //            //var path = HttpContext.Current.Server.MapPath("~/Resumes/" + "application_" + application.Id + "_" + specialKey);
        //            //Directory.CreateDirectory(path);
        //            //fullPath = "http://" + fullPath;
        //            string fullPath = HttpContext.Current.Server.MapPath("~/Images/ProductImages" + "product_" + product.Id + "_" + specialKey + "_" + httpRequest.Files[k].FileName);
        //            var postedFile = httpRequest.Files[k];
        //            postedFile.SaveAs(fullPath);
        //            string dbPath = "http://printsanaccess.online/Images/ProductImages" + "product_" + product.Id + "_" + specialKey + "_" + httpRequest.Files[k].FileName;
        //            if ( k == 0)
        //            {
        //                product.PhotoPath1 = dbPath;
        //            }
        //            else
        //            {
        //                product.PhotoPath2 = dbPath;
        //            }
        //        }
        //        context.SaveChanges();
        //        return new HttpResponseMessage(HttpStatusCode.OK);
        //    }
        //    return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //}



        //[Authorize]
        //[HttpPost]
        //public HttpResponseMessage CreateNewProductCategory(ProductCategory cat)
        //{
        //    ProductCategory c = new ProductCategory()
        //    {
        //        isActive = true,
        //        CategoryName = cat.CategoryName,
        //        CreatedOn = DateTime.Now
        //    };
        //    context.ProductCategories.Add(c);
        //    context.SaveChanges();
        //    return new HttpResponseMessage(HttpStatusCode.OK);
        //}

        //[Authorize]
        //[HttpGet]
        //public HttpResponseMessage RemoveProduct(int productId)
        //{
        //    try
        //    {
        //        Product pr = context.Products.Where(x => x.Id == productId).FirstOrDefault();
        //        pr.isActive = false;
        //        context.SaveChanges();
        //        return new HttpResponseMessage(HttpStatusCode.OK);
        //    }
        //    catch (Exception)
        //    {

        //        return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //    }
        //}

        //[Authorize]
        //[HttpGet]
        //public List<JobApplication> GetJobApplications()
        //{
        //    List<JobApplication> jobApplications = context.JobApplications.Include("OpenPosition").AsNoTracking().OrderByDescending(x => x.Id).ToList();
        //    return jobApplications;
        //}

        //[Authorize]
        //[HttpPost]
        //public HttpResponseMessage CreateOpenPosition(OpenPosition position)
        //{
        //    OpenPosition openPosition = new OpenPosition()
        //    {
        //        isActive = true,
        //        CreatedOn = DateTime.Now,
        //        Description = position.Description,
        //        JobTitle = position.JobTitle,
        //        PublishedDate = DateTime.Now,

        //    };
        //    context.OpenPositions.Add(openPosition);
        //    context.SaveChanges();
        //    return new HttpResponseMessage(HttpStatusCode.OK);
        //}

        //[Authorize]
        //[HttpGet]
        //public List<ContactFormResponseModel> GetContactForms()
        //{
        //    List<ContactForm> contactForms = new List<ContactForm>();
        //    contactForms = context.ContactForms.OrderByDescending(x => x.CreatedOn).ToList();
        //    List<ContactFormResponseModel> responseModels = new List<ContactFormResponseModel>();
        //    if (contactForms == null)
        //        return responseModels;
        //    foreach(ContactForm form in contactForms)
        //    {
        //        responseModels.Add(new ContactFormResponseModel() { ContactForm = form });
        //    }
        //    return responseModels;
        //}


        //[Authorize]
        //[HttpPost]
        //public HttpResponseMessage CreateNewOpenPosition(OpenPosition position)
        //{
        //    OpenPosition pos = new OpenPosition()
        //    {
        //        CreatedOn = DateTime.Now,
        //        PublishedDate = DateTime.Now,
        //        isActive = true,
        //        //Description = "Proje yöneticisi - projeden sorumlu tecrübeli eleman aranıyor.",
        //        Description = position.Description,
        //        JobTitle = position.JobTitle
        //    };
        //    context.OpenPositions.Add(pos);
        //    context.SaveChanges();
        //    return new HttpResponseMessage(HttpStatusCode.OK);
        //}

        //[Authorize]
        //[HttpGet]
        //public HttpResponseMessage RemoveOpenPosition(int openPositionId)
        //{
        //    try
        //    {
        //        OpenPosition pos = context.OpenPositions.Where(x => x.Id == openPositionId).FirstOrDefault();
        //        pos.isActive = false;
        //        context.SaveChanges();
        //        return new HttpResponseMessage(HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {

        //        return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //    }
        //}

        //[Authorize]
        //[HttpGet]
        //public List<ProductCategory> GetProductCategories()
        //{
        //    return context.ProductCategories.OrderByDescending(x => x.Id).ToList();
        //}

    }
}
