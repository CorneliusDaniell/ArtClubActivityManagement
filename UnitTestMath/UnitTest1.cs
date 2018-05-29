using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArtClubActivityManagement;
using System.Web.Mvc;
using System.Net;
using System.Web;
using ArtClubActivityManagement.Controllers;
using UnitTestMath;


namespace UnitTestMath
{

    [TestClass]
    public class EvenimentControllerTest
    {
        [TestMethod]
        public void Details()
        {
            // Arrange
            MembriiController controller = new MembriiController();

            string id = "22";

            // Act
              ViewResult result = controller.Details(id) as ViewResult;
            var reuslt = controller.Details(id);

            // Assert
            Assert.IsNotNull("Details", id);
        }
    }


    [TestClass]
    public class EvenimentControllerTest
    {

        /// <summary>
        ///  app.net mvc 5  crud
        /// </summary>
        [TestMethod]
        public void AddMembriiTest()
        {

            //if (this.User.IsInRole("Membru"))
            //{
            Membrii membru = new Membrii();
            //Membrii membriu = db.Membrii.Find(id);
            //var result = db.Membrii.Where(a => a.ID_Username == memb.ID_Username && a.Parola == memb.Parola).ToList();
            //membru.Create(eveniment);
            membru.ID_Username = "ion";
            membru.Nume = "ionnn";
            membru.Prenume = "adrian";
            membru.Email = "asdas@yahoo.com";
            membru.Parola = "a2312";
            membru.ID_NumeFunctie = "Membru";
            //}

        }

        [TestMethod]
        public void AddMembriiGresitTest()
        {
            ArtClubEntities21 db = new ArtClubEntities21();
            //if (this.User.IsInRole("Membru"))
            //{
            Membrii membru = new Membrii();
            //Membrii membriu = db.Membrii.Find(id);
            //var result = db.Membrii.Where(a => a.ID_Username == memb.ID_Username && a.Parola == memb.Parola).ToList();
            //membru.Create(eveniment);
            membru.ID_Username = "ion";
            membru.Nume = "ionnn";
            membru.Prenume = "adrian";
            membru.Email = "asdas@yahoo.com";
            membru.Parola = "a2312";
            membru.ID_NumeFunctie = "Memb45ru";
            db.Membriis.Add(membru);
            //}

        }


        [TestMethod]
        public void Details()
        {
            // Arrange
            MembriiController controller = new MembriiController();

            string id = "22";

            // Act
            ViewResult result = controller.Details(id) as ViewResult;

            // Assert
            Assert.IsNotNull("Details", result.ViewName);
        }

        [TestMethod]
        public void Create()// membru existent creaza membru =>false
        {
            //membru,nonmembru creeaza membru (nu se poate)
            Membrii membru2 = new Membrii();

            membru2.ID_Username = "4";
            //membru2.Nume = "ionnn";
            //membru2.Prenume = "adrian";
            //membru2.Email = "asdas@yahoo.com";
            membru2.Parola = "4";
            //membru2.ID_NumeFunctie = "Membru";//asta exista
            AccountController accountcontroler = new AccountController();

            ViewResult result2 = accountcontroler.Login(membru2) as ViewResult;



            MembriiController controller = new MembriiController();

            Membrii membru = new Membrii();
            membru.ID_Username = "25323";
            membru.Nume = "ionnn";
            membru.Prenume = "adrian";
            membru.Email = "asdas@yahoo.com";
            membru.Parola = "a2312";
            membru.ID_NumeFunctie = "Membru";

            // Act
            ViewResult result = controller.Create(membru) as ViewResult;

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Create1()//admin creaza membru existent
        {
            //membru,nonmembru creeaza membru (nu se poate)
            Membrii membru2 = new Membrii();

            membru2.ID_Username = "5";
            membru2.Parola = "123";

            AccountController accountcontroler = new AccountController();

            ViewResult result2 = accountcontroler.Login(membru2) as ViewResult;

            MembriiController controller = new MembriiController();

            Membrii membru = new Membrii();
            membru.ID_Username = "4";
            membru.Nume = "4";
            membru.Prenume = "4";
            membru.Email = "4";
            membru.Parola = "4";
            membru.ID_NumeFunctie = "Membru";

            // Act
            ViewResult result = controller.Create(membru) as ViewResult;

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Create2()//admin creaza membru neexistent
        {
            //membru,nonmembru creeaza membru (nu se poate)
            Membrii membru2 = new Membrii();

            membru2.ID_Username = "5";
            membru2.Parola = "123";

            AccountController accountcontroler = new AccountController();

            ViewResult result2 = accountcontroler.Login(membru2) as ViewResult;

            MembriiController controller = new MembriiController();

            Membrii membru = new Membrii();
            membru.ID_Username = "Dida";
            membru.Nume = "4DIdd";
            membru.Prenume = "4";
            membru.Email = "4";
            membru.Parola = "777";
            membru.ID_NumeFunctie = "Membru";

            // Act
            ViewResult result = controller.Create(membru) as ViewResult;

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            MembriiController controller = new MembriiController();
            string id = "5";
            // Act
            ViewResult result = controller.Edit(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete()
        {

            //  Session["ID_Username"] = "Membru";
            // Arrange
            MembriiController controller = new MembriiController();
            string id = "2";

            // Act
            ViewResult result = controller.Delete(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


    }

}

