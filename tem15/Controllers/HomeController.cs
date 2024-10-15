using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using tem15.Models;

namespace tem15.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        QlgiaiBongDaContext db = new QlgiaiBongDaContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            var lstCauThu = db.Cauthus.Take(7).ToList();

            ViewBag.DanhSachTranDau = db.Trandaus.AsNoTracking().Where(x => x.Clbkhach == "101" || x.Clbnha == "101").ToList();


            return View(lstCauThu);




        }
        [HttpGet]
        [Route("TaoMoiTranDau")]
        public IActionResult TaoMoiTranDau()
        {


            ViewBag.Clbnha = new SelectList(db.Caulacbos.ToList(), "CauLacBoId", "TenClb");
                ViewBag.Clbkhach= new SelectList(db.Caulacbos.ToList(), "CauLacBoId", "TenClb");
            ViewBag.SanVanDongId = new SelectList(db.Sanvandongs.ToList(), "SanVanDongId", "TenSan");


            return View();
        }



        [HttpPost]
        [Route("TaoMoiTranDau")]
        public IActionResult TaoMoiTranDau(Trandau trandau)
        {
            db.Trandaus.Add(trandau);
            db.SaveChanges();

            ViewBag.Clbnha = new SelectList(db.Caulacbos.ToList(), "CauLacBoId", "TenClb");
            ViewBag.Clbkhach = new SelectList(db.Caulacbos.ToList(), "CauLacBoId", "TenClb");
            ViewBag.SanVanDongId = new SelectList(db.Sanvandongs.ToList(), "SanVanDongId", "TenSan");


            return View();
        }










        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
